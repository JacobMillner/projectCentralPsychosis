using UnityEngine;
using System.Collections;

public class CoinPickup : MonoBehaviour {

	public int pointsToAdd;

	void OnTriggerEnter2D (Collider2D other)
	{
		if (other.GetComponent<PlayerController> () != null) {
			ScoreManager.AddPoints(pointsToAdd); //add the points and destroy the coin
			Destroy (gameObject);
		}
	}
}
