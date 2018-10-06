using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameStatePostProcessAdjust : MonoBehaviour {

	public GameState game;
	public PostProcessVolume pp;

	void Start () {
		if (game == null) {
			Destroy (this);
		}
	}

	// Update is called once per frame
	void Update () {
		pp.weight = 1 - (game.mentalHealthScore / 100f);

	}
}