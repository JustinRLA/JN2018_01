using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBlendShape : MonoBehaviour {

	public bool Randomize;

	void Start () {
		if (Randomize == true) {
			GetComponent<SkinnedMeshRenderer> ().SetBlendShapeWeight (0, Random.Range (0f, 100f));
		}
	}

}