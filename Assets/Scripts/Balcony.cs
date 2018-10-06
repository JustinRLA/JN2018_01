using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Balcony : MonoBehaviour {

	public bool canEndGame = false;
	public Light balconyLight;

	public void OpenBalcony () {
		GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (0, Random.Range (0f, 100f));
		GetComponent<BoxCollider> ().isTrigger = true;
		balconyLight.intensity = 1f;
		canEndGame = true;
	}

	private void OnTriggerEnter (Collider other) {
		if (other.gameObject.tag == "intimatePlayer" && canEndGame == true) {
			SceneManager.LoadScene ("BalconyScene", LoadSceneMode.Single);
		}
	}

}