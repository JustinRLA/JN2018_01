using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRoomColor : MonoBehaviour {

	string colorNameInShader = "_Color";

	public Color[] woodColors;
	public Color[] wallColors;
	public Color[] metalColors;
	public GameObject[] selectedRenderers;

	public bool autoSelectFloors;
	public bool autoSelectWalls;

	void Awake () {
		if (autoSelectFloors) {
			selectedRenderers = GameObject.FindGameObjectsWithTag ("floor");
		}
		if (autoSelectWalls) {
			selectedRenderers = GameObject.FindGameObjectsWithTag ("wall");
		}
	}

	void Start () {

		Color selectedWallColor = wallColors[Random.Range (0, wallColors.Length)];
		Color selectedWoodColor = woodColors[Random.Range (0, woodColors.Length)];
		Color selectedMetalColor = metalColors[Random.Range (0, metalColors.Length)];

		foreach (GameObject item in selectedRenderers) {
			if (item.GetComponent<MeshRenderer> ().materials.Length == 1) { // Only wood
				item.GetComponent<MeshRenderer> ().material.SetColor (colorNameInShader, selectedWoodColor);
			}
			if (item.GetComponent<MeshRenderer> ().materials.Length == 2) { // Plaster, wood
				Material[] newTwoMaterials = item.GetComponent<MeshRenderer> ().materials;
				newTwoMaterials[0].SetColor (colorNameInShader, selectedWallColor);
				newTwoMaterials[1].SetColor (colorNameInShader, selectedWoodColor);

			}
			if (item.GetComponent<MeshRenderer> ().materials.Length == 3) { // Plaster, wood, metal
				Material[] newThreeMaterials = item.GetComponent<MeshRenderer> ().materials;
				newThreeMaterials[0].SetColor (colorNameInShader, selectedWallColor);
				newThreeMaterials[1].SetColor (colorNameInShader, selectedWoodColor);
				newThreeMaterials[2].SetColor (colorNameInShader, selectedMetalColor);
			}
		}

	}

}