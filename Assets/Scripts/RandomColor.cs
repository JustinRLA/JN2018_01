using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomColor : MonoBehaviour {

	public string colorNameInShader = "_Color";
	public bool useRandomColor;
	public bool isSkinnedMesh;

	void Start () {

		if (useRandomColor) {

			if (!isSkinnedMesh) {
				Color randomCol = new Color (Random.Range (0.2f, 1f), Random.Range (0.2f, 1f), Random.Range (0.2f, 1f), 1f);
				GetComponent<MeshRenderer> ().material.SetColor (colorNameInShader, randomCol);
			} else {
				Color randomCol = new Color (Random.Range (0.2f, 1f), Random.Range (0.2f, 1f), Random.Range (0.2f, 1f), 1f);
				GetComponent<SkinnedMeshRenderer> ().material.SetColor (colorNameInShader, randomCol);
			}

		}

	}

}