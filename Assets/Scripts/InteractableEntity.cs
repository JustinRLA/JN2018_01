using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableEntity : MonoBehaviour {

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //STATE VARIABLES
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    //Object Type
    public enum InteractableType { generic, partygoer, pickup, disposal }
    public InteractableType interactableType;

    public bool hasInteraction2 = false;
    public bool canBePickedUp = false;
    public bool canReceive = false;

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //GAMEPLAY VARIABLES
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //The likelihood that a player's interaction with an objet yields a positive, neutral, or negative outcome
    public int successRatio = 10;
    public int neutralRatio = 30;
    public int failRatio = 100;

    //Adds modifier to success rate when interacting with other characters based on their anger
    float successMod = 1;

    //How much mental health is gained/lost when player interacts with entity
    public int healthImpact = 5;

    public GameObject pickupPrefab;

    GameObject playerPawn;
    GameObject UIInteract1;
    GameObject UIInteract2;
    GameObject UIHand;

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //STANDARD FUNCTIONS
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Use this for initialization

    void Awake () {
        UIInteract1 = GameObject.Find ("UI_Interact1");
        UIInteract2 = GameObject.Find ("UI_Interact2");
        UIHand = GameObject.Find ("UI_Hand");
        playerPawn = GameObject.Find ("Player");
    }

    void Start () {

        UIInteract1.SetActive (false);
        UIInteract2.SetActive (false);
        UIHand.SetActive (false);

        //Any random character
        if (interactableType == InteractableType.partygoer) {
            hasInteraction2 = true;
            canBePickedUp = false;
            canReceive = true;
        }
        //Object that can be picked up
        else if (interactableType == InteractableType.pickup) {
            hasInteraction2 = false;
            canBePickedUp = true;
            canReceive = false;
        }
        //Object that you can safely get rid of object in (trash, table)
        else if (interactableType == InteractableType.disposal) {
            hasInteraction2 = false;
            canBePickedUp = false;
            canReceive = true;
        }
        //Generic object interaction parameters
        else {
            hasInteraction2 = true;
            canBePickedUp = false;
            canReceive = false;
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //GAMEPLAY FUNCTIONS
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void DisplayOptions (bool active) {
        if (active) {

            if (interactableType == InteractableType.partygoer) {
                UIInteract1.SetActive (true);
                UIInteract2.SetActive (true);
                if (playerPawn.GetComponent<GameState> ().isHoldingItem == true) {
                    UIHand.SetActive (true);
                }
            }
            //Object that can be picked up
            else if (interactableType == InteractableType.pickup) {

                UIHand.SetActive (true);
            }
            //Object that you can safely get rid of object in (trash, table)
            else if (interactableType == InteractableType.disposal) {

                if (playerPawn.GetComponent<GameState> ().isHoldingItem == true) {
                    UIHand.SetActive (true);
                }
            }

        } else {
            UIInteract1.SetActive (false);
            UIInteract2.SetActive (false);
            UIHand.SetActive (false);
        }
    }

    public void Interact (string input, GameObject player) {
        if (input == "Interact1") {
            Result (player);
        } else if (input == "Interact2") {
            Result (player);
        } else if (input == "Take") {
            print (this.gameObject.name + " gives you a " + pickupPrefab);
            player.GetComponent<GameState> ().UpdateInventory (true, pickupPrefab);
        } else if (input == "Give") {
            player.GetComponent<GameState> ().UpdateInventory (false, pickupPrefab);
            if (interactableType == InteractableType.partygoer) {
                Result (player);
            }
        }

    }

    void Result (GameObject player) {
        if (interactableType == InteractableType.partygoer) {
            successMod = GetComponent<Partygoer> ().anger;
        }

        int resultScore = Random.Range (1, 100);
        if (resultScore <= successRatio / successMod) {
            //Success
            print ("player succeeded with " + this.gameObject.name);

            //Update player's mental health
            player.GetComponent<GameState> ().UpdateMentalHealthScore (healthImpact);

            //Make partygoer less mad
            if (interactableType == InteractableType.partygoer) {
                GetComponent<Partygoer> ().SetMood (-healthImpact, player);
            }
        } else if (resultScore <= neutralRatio / successMod) {
            //Neutral
            print ("player was neutral with " + this.gameObject.name);

            //Play success interaction animation
            player.GetComponent<GameState> ().UpdateMentalHealthScore (healthImpact);
            //Make partygoer less mad
            if (interactableType == InteractableType.partygoer) {
                GetComponent<Partygoer> ().SetMood (-healthImpact, player);
            }
        } else {
            //Fail
            print ("player failed with " + this.gameObject.name);
            //Play success interaction animation
            player.GetComponent<GameState> ().UpdateMentalHealthScore (-healthImpact);
            //Make partygoer mad
            if (interactableType == InteractableType.partygoer) {
                GetComponent<Partygoer> ().SetMood (1.0f, player);
            }
        }
    }
}