using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

	public GameObject currentCheckpoint;

	private PlayerController player;

	public GameObject deathParticle;
	public GameObject respawnParticle;
	public int pointPenaltyOnDeath;

	public float respawnTimer;

	// Use this for initialization
	void Start () {

		player = FindObjectOfType<PlayerController> ();
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void RespawnPlayer()
	{
		StartCoroutine ("RespawnPlayerCo");
	}

	public IEnumerator RespawnPlayerCo()
	{
		Instantiate (deathParticle, player.transform.position, player.transform.rotation);
		player.enabled = false;
		player.GetComponent<Renderer>().enabled = false;
		player.GetComponent<Rigidbody2D> ().velocity = Vector2.zero; //set the player speed to 0 so he doesnt keep moving after death
		ScoreManager.AddPoints (-pointPenaltyOnDeath); //subtract the points!!!
		Debug.Log ("Respawned");
		yield return new WaitForSeconds (respawnTimer);
		player.transform.position = currentCheckpoint.transform.position;
		Instantiate (respawnParticle, currentCheckpoint.transform.position, currentCheckpoint.transform.rotation);
		player.enabled = true;
		player.GetComponent<Renderer>().enabled = true;
	}
}
