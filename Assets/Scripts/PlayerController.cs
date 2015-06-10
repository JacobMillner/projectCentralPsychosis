using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

	public float moveSpeed;
	public float jumpHeight;
	public Transform groundCheck;
	public float groundCheckRadius;
	public LayerMask whatIsGround;
	private bool grounded;
	private bool doubleJumped;
	private Animator anim;
	Rigidbody2D rb2D;
	Renderer ren;
	public Transform cameraHolder;
	public float cameraSpeed;

	public enum CameraMovementType
	{
		Lerp,
		MoveTowards,
		AccelDecel,
		Accerleration
	}

	public CameraMovementType cameraMovementType;

	void CameraMovement ()
	{
		switch (cameraMovementType) {
		case CameraMovementType.Lerp:
			cameraHolder.transform.position = Vector3.Lerp (cameraHolder.transform.position, transform.position, Time.deltaTime * cameraSpeed);
			break;
		case CameraMovementType.MoveTowards:
			cameraHolder.transform.position = Vector3.MoveTowards (cameraHolder.transform.position, transform.position, Time.deltaTime * cameraSpeed);
			break;
		case CameraMovementType.AccelDecel:
			cameraHolder.transform.position = InterpilationLibrary.AccelDecelInterpolation(cameraHolder.position, transform.position, Time.deltaTime * cameraSpeed);
			break;
		case CameraMovementType.Accerleration:
			cameraHolder.transform.position = InterpilationLibrary.AccelerationInterpolation(cameraHolder.position, transform.position, Time.deltaTime * cameraSpeed, 1);
			break;

		}
	}

	// Use this for initialization
	void Start ()
	{
		anim = GetComponent<Animator> ();
		rb2D = GetComponent<Rigidbody2D> ();
		ren = GetComponent<Renderer> ();

		cameraHolder = Camera.main.transform.parent.transform;
	
	}

	void FixedUpdate ()
	{

		CameraMovement ();

		grounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
	}
	
	// Update is called once per frame
	void Update ()
	{

		if (grounded)
			doubleJumped = false;

		anim.SetBool ("Grounded", grounded);
		
		//jumpy jumpy
		if (Input.GetKeyDown (KeyCode.Space) && grounded) {
			
			jump ();
			
		}
		
		
		if (Input.GetKeyDown (KeyCode.Space) && !grounded && !doubleJumped) {
			
			jump ();
			
			doubleJumped = true;
		}
		
		//move right
		if (Input.GetKey (KeyCode.D)) {
			
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		}
		
		//move left
		if (Input.GetKey (KeyCode.A)) {
			
			GetComponent<Rigidbody2D> ().velocity = new Vector2 (-moveSpeed, GetComponent<Rigidbody2D> ().velocity.y);
		}

		anim.SetFloat ("Speed", Mathf.Abs (GetComponent<Rigidbody2D> ().velocity.x));

		if (GetComponent < Rigidbody2D> ().velocity.x > 0)
			transform.localScale = new Vector3 (1f, 1f, 1f);
		else if (GetComponent<Rigidbody2D> ().velocity.x < 0)
			transform.localScale = new Vector3 (-1f, 1f, 1f);
	}
	
	public void jump ()
	{
		
		GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, jumpHeight);
		
	}
	
}
