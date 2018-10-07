using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableRendererOnStart : MonoBehaviour {

	// Use this for initialization
	void Awake () {
		Destroy (GetComponent<MeshRenderer> ());
		gameObject.SetActive (false);
	}

}