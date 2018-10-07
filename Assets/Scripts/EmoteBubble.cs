using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmoteBubble : MonoBehaviour {

	public MeshRenderer thisBubble;
	public MeshRenderer thisEmote;

	public string[] Emotions;
	public Material[] EmotionsIcons;
	public Material[] EmotionFaces;

	public SkinnedMeshRenderer faceMaterial;

	void Awake () {
		TurnBubbleOff ();
	}

	public void TurnBubbleOff () {
		thisBubble.enabled = false;
		thisEmote.enabled = false;
	}

	public void NormalFace () {
		faceMaterial.material = EmotionFaces[0];
	}

	public void Emote (string emotionToElicit) {
		CancelInvoke ("TurnBubbleOff");
		CancelInvoke ("NormalFace");
		GetComponent<AudioSource> ().Play ();

		thisEmote.material = EmotionsIcons[0];

		for (int e = 0; e < Emotions.Length; e++) {
			if (Emotions[e] == emotionToElicit) {
				thisEmote.material = EmotionsIcons[e];
				faceMaterial.material = EmotionFaces[e];
			}
		}

		thisBubble.enabled = true;
		thisEmote.enabled = true;
		Invoke ("TurnBubbleOff", Random.Range (3f, 4f));
		Invoke ("NormalFace", Random.Range (2f, 8f));
	}

	public void EmoteFaceOnly (string emotionToElicit) {
		CancelInvoke ("TurnBubbleOff");

		for (int e = 0; e < Emotions.Length; e++) {
			if (Emotions[e] == emotionToElicit) {

				faceMaterial.material = EmotionFaces[e];
			}
		}

		Invoke ("NormalFace", Random.Range (1f, 3f));
	}

}