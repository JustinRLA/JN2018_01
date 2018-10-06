using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableEntity : MonoBehaviour {

    //Variables
    public bool hasInteraction2 = false;
    public bool canBePickedUp = false;

    //The likelihood that a player's interaction with an objet yields a positive, neutral, or negative outcome
    public int successRatio = 10;
    public int neutralRatio = 30;
    public int failRatio = 100;

    //How much mental health is gained/lost when player interacts with entity
    public int healthImpact = 5;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void DisplayOptions(bool active)
    {
        if(active)
        {
            //Turn on display
        }
        else
        {
            //Turn off display
        }
    }

    public void Interact (string input, GameObject player)
    {
        int resultScore = Random.Range(1, 100);
        if(resultScore <= successRatio)
        {
            //Success
            print("player succeeded");
            //Play success interaction animation
            player.GetComponent<GameState>().UpdateMentalHealthScore(healthImpact);
        }
        else if(resultScore <= neutralRatio)
        {
            //Neutral
            print("player was neutral");
        }
        else
        {
            //Fail
            print("player failed");
            //Play success interaction animation
            player.GetComponent<GameState>().UpdateMentalHealthScore(-healthImpact);
        }
    }

    void Result()
    {

    }
}
