using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterRandomizer : MonoBehaviour {

	public GameObject[] hats;
	public GameObject[] collars;

	public Texture[] topTex;
	public Texture[] midTex;
	public Texture[] lowTex;

	public Color[] outfitColors;
	public Color[] skinColors;
	public Color[] accessoryColors;

	public SkinnedMeshRenderer BodyMaterials;

	void Start () {

		Material[] dummyMaterials = BodyMaterials.materials;

		int hatDice = Random.Range (0, hats.Length);
		int topDice = Random.Range (0, topTex.Length);
		int midDice = Random.Range (0, midTex.Length);
		int bottomDice = Random.Range (0, lowTex.Length);

		Color colorDice = outfitColors[Random.Range (0, outfitColors.Length)];
		Color selectedBottomColors = outfitColors[Random.Range (0, outfitColors.Length)];
		Color selectedTopColors = outfitColors[Random.Range (0, outfitColors.Length)];
		Color selectedSkinColors = outfitColors[Random.Range (0, outfitColors.Length)];

		dummyMaterials[0].mainTexture = topTex[topDice];
		dummyMaterials[1].mainTexture = midTex[midDice];
		dummyMaterials[2].mainTexture = lowTex[bottomDice];

		dummyMaterials[0].SetColor ("_Color", colorDice);
		dummyMaterials[1].SetColor ("_Color", colorDice);
		dummyMaterials[2].SetColor ("_Color", colorDice);
		/* 
				for (int i = 0; i < dummyMaterials.Length; i++) {
					dummyMaterials[i].SetColor ("_Color", colorDice);
				}
		*/
		//	BodyMaterials.materials = dummyMaterials;

		hats[hatDice].SetActive (true);
		collars[topDice].SetActive (true);

		Material hostAccessory = hats[hatDice].GetComponent<SkinnedMeshRenderer> ().material;

		if (hatDice == 0 || hatDice == 1) {
			hostAccessory.SetColor ("_Color", colorDice); // Hair Material
		} else {
			hostAccessory.SetColor ("_Color", colorDice);
		}

		hats[hatDice].GetComponent<SkinnedMeshRenderer> ().material = hostAccessory;
		collars[topDice].GetComponent<SkinnedMeshRenderer> ().material = hostAccessory;
	}
}