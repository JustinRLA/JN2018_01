using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteBubble : MonoBehaviour {

	public MeshRenderer thisBubble;
	public MeshRenderer thisEmote;

	public string[] Emotions;
	public Material[] EmotionsIcons;

	void Awake () {
		TurnBubbleOff ();
	}

	public void TurnBubbleOff () {
		thisBubble.enabled = false;
		thisEmote.enabled = false;
	}

	public void Emote (string emotionToElicit) {
		GetComponent<AudioSource> ().Play ();

		thisEmote.material = EmotionsIcons[0];

		for (int e = 0; e < Emotions.Length; e++) {
			if (Emotions[e] == emotionToElicit) {
				thisEmote.material = EmotionsIcons[e];
			}
		}

		thisBubble.enabled = true;
		thisEmote.enabled = true;
		Invoke ("TurnBubbleOff", Random.Range (3f, 4f));
	}
}