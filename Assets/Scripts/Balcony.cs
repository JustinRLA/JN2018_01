using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Balcony : MonoBehaviour {

	public bool canEndGame = false;
	public Light balconyLight;

	public void OpenBalcony () {
		GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (0, 100f);
		GetComponent<BoxCollider> ().isTrigger = true;
		balconyLight.intensity = 2.75f;
		canEndGame = true;
	}

	private void OnTriggerStay (Collider other) {
		if (other.gameObject.tag == "intimatePlayer" && canEndGame == true) {
			SceneManager.LoadScene ("BalconyScene", LoadSceneMode.Single);
		}
	}

}