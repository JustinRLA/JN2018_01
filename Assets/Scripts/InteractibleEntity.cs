using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractibleEntity : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider col)
    {
       if(col.gameObject.name == "Player")
        {
            //Display interaction options
        }

    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name == "Player")
        {
            //Hide interaction options
        }
    }

    void Interact (string input)
    {
        /*
        if (Input.GetButtonUp(Interact1Btn))
        {
            //Trigger object Interact 1
        }
        else if (Input.GetButtonUp(Interact2))
        {
            //Trigger object Interact 2
        }
        else if (Input.GetButtonUp(TakeGive))
        {
            //Trigger object Interact 1
        }*/
    }
}
