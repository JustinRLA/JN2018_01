using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSkinColor : MonoBehaviour {
	/* 
		string colorNameInShader = "_Color";

		public Color[] outfitColors;
		public Color[] skinColors;
		public GameObject[] recolouredCollars;

		void Start () {

			Color selectedBottomColors = outfitColors[Random.Range (0, outfitColors.Length)];
			Color selectedTopColors = outfitColors[Random.Range (0, outfitColors.Length)];
			Color selectedSkinColors = skinColors[Random.Range (0, skinColors.Length)];

			Material[] hostMaterials = GetComponent<SkinnedMeshRenderer> ().materials;

			hostMaterials[0].SetColor (colorNameInShader, selectedSkinColors);
			hostMaterials[1].SetColor (colorNameInShader, selectedTopColors);
			hostMaterials[2].SetColor (colorNameInShader, selectedBottomColors);

			foreach (GameObject item in recolouredCollars) {
				item.GetComponent<SkinnedMeshRenderer> ().material.SetColor (colorNameInShader, selectedTopColors);

			}

		}
	*/
}