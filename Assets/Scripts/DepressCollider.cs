using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepressCollider : MonoBehaviour {

	public float Damage = 5f;

	void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "Player") {
			other.gameObject.GetComponent<GameState> ().mentalHealthScore -= Damage;
			Destroy (gameObject);
		}
	}
}