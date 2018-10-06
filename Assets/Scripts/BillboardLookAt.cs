using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillboardLookAt : MonoBehaviour {

	Transform target;

	void Awake () {
		target = GameObject.Find ("Main Camera").transform;
	}

	void Update () {
		if (target != null) {
			transform.LookAt (target);
		}
	}
}