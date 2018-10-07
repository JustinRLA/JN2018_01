using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRandomizer : MonoBehaviour {

	public bool isPlayer;
	public GameObject[] hats;
	public GameObject[] collars;
	public SkinnedMeshRenderer BodyMaterials;

	public Color[] accessoryColors;

	public bool enableDeletion = true;

	void Start () {

		if (!isPlayer) {
			int aDice = Random.Range (0, hats.Length + 1);
			int bDice = Random.Range (0, collars.Length + 1);

			if (aDice < hats.Length) {

				hats[aDice].GetComponent<SkinnedMeshRenderer> ().material.SetColor ("_Color", accessoryColors[Random.Range (0, accessoryColors.Length)]);

				for (int i = 0; i < hats.Length; i++) {
					if (i != aDice) {
						if (enableDeletion) {
							hats[i].SetActive (false);
						}

					}
				}
			} else {
				for (int i = 0; i < hats.Length; i++) {
					if (enableDeletion) {
						hats[i].SetActive (false);
					}

				}
			}
			if (bDice < collars.Length) {

				//	collars[bDice].GetComponent<SkinnedMeshRenderer> ().material.SetColor ("_Color", accessoryColors[Random.Range (0, accessoryColors.Length)]);

				for (int i = 0; i < collars.Length; i++) {
					if (i != aDice) {
						if (enableDeletion) {
							collars[i].SetActive (false);
						}

					}
				}
			} else {
				for (int i = 0; i < collars.Length; i++) {
					if (enableDeletion) {
						collars[i].SetActive (false);
					}

				}
			}
		}

	}
}