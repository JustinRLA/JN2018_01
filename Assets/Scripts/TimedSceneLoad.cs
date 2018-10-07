using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TimedSceneLoad : MonoBehaviour {

	public void Start () {
		Invoke ("Load", 22f);
	}

	public void Load () {
		SceneManager.LoadScene ("MainMenu", LoadSceneMode.Single);
	}
}