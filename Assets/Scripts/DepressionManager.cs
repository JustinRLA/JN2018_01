using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepressionManager : MonoBehaviour {

	float anxiety;
	public GameState manager;
	GameObject[] lightInScene;
	public GameObject[] FloorCarvers;
	float[] lightIntensities;
	int clock = -1;
	bool CarversUp;

	public AudioSource[] sounds;
	public AudioSource heartbeat;

	void Start () {
		lightInScene = GameObject.FindGameObjectsWithTag ("Light");
		lightIntensities = new float[lightInScene.Length];

		for (int i = 0; i < lightInScene.Length; i++) {
			lightIntensities[i] = lightInScene[i].GetComponent<Light> ().intensity;
		}

		foreach (GameObject carver in FloorCarvers) {
			Destroy (carver.GetComponent<MeshRenderer> ());
			carver.SetActive (false);
		}

	}

	void Update () {

		anxiety = (manager.mentalHealthScore / 100f);
		heartbeat.volume = 1f - anxiety;

		clock = -1;
		if (!CarversUp) {
			foreach (GameObject lit in lightInScene) {
				clock++;
				lit.GetComponent<Light> ().intensity = anxiety * lightIntensities[clock];
			}

		}

		foreach (AudioSource audio in sounds) {
			audio.volume = anxiety;
		}

		if (!CarversUp && anxiety < 0.25f) {

			foreach (GameObject carver in FloorCarvers) {
				carver.SetActive (true);

				foreach (GameObject lit in lightInScene) {
					clock++;
					lit.GetComponent<Light> ().intensity = 2f;
				}

			}
			CarversUp = true;
		}

	}
}