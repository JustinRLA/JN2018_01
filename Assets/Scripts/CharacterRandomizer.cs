using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRandomizer : MonoBehaviour {

	public bool isPlayer;
	public GameObject[] hats;
	public GameObject[] collars;
	public SkinnedMeshRenderer BodyMaterials;

	public Material[] pantsMaterial;
	public Material[] topsMaterial;
	public Material[] skinMaterial;

	void Start () {

		if (!isPlayer) {
			int aDice = Random.Range (0, hats.Length + 1);
			int bDice = Random.Range (0, collars.Length + 1);

			if (aDice <= hats.Length) {
				for (int i = 0; i < hats.Length; i++) {
					if (i != aDice) {
						hats[i].SetActive (false);
					}
				}
			}
			if (bDice <= collars.Length) {
				for (int i = 0; i < collars.Length; i++) {
					if (i != aDice) {
						collars[i].SetActive (false);
					}
				}
			}
		}

	}
}