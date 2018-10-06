using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class GameStatePostProcessAdjust : MonoBehaviour {

	public GameState game;
	public PostProcessVolume ppvs;

	void Start () {
		if (game == null) {
			Destroy (this);
		}
	}

	void Update () {

		ppvs.weight = Mathf.Clamp (1f - game.mentalHealthScore / 100f, 0.01f, 1f);

	}
}