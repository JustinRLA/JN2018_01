using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteBubbleCull : MonoBehaviour {

	void Start () {
		Camera camera = GetComponent<Camera> ();
		float[] distances = new float[32];
		distances[9] = 5;
		camera.layerCullDistances = distances;
	}

}