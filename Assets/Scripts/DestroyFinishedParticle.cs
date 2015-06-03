using UnityEngine;
using System.Collections;

public class DestroyFinishedParticle : MonoBehaviour {

	private ParticleSystem thisParticleSystem;

	// Use this for initialization
	void Start () {
		thisParticleSystem = GetComponent<ParticleSystem> ();
	
	}
	
	// Update is called once per frame
	void Update () {

		//probably fix this, this is dumb
		if (thisParticleSystem.isPlaying) {
			return;
		}

		Destroy (gameObject);
	
	}
}
