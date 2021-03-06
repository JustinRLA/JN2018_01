﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //INPUT VARIABLES
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public string horizontalBtn = "Horizontal";
    public string verticalBtn = "Vertical";
    public string Interact1Btn = "Interact1";
    public string Interact2Btn = "Interact2";
    public string TakeGiveBtn = "TakeGive";

    public string startBtn = "Submit";
    public string quitBtn = "Quit";

    float interactionCooldown = 0.5f;
    public bool playerOnCooldown = false;

    ///////////////////////!///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //GAMEPLAY VARIABLES
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //Basic counter for how the player's mental health/anxiety is going
    public float mentalHealthScore = 100.0f;
    //Is the player currently holding an item
    public bool isHoldingItem = false;
    //Is the player doing an action (use to prevent triggers while performing action)
    public bool isActing = false;
    //Rate at which player's mental health deteriorates
    public float healthDecay = 0.7f;
    //Is the player's anxiety decrease on cooldown
    public bool isNotGettingAnxious = false;
    //How long in seconds between interactions before anxiety kicks in
    public int idleAnxietyTime = 5;
    //The mental health score needed to make balcony accessible
    public float balconyAccessible = 25f;
    //The mental health score needed to trigger everyone being mad and making balcony the only remaining option
    public float goodEndTrigger = 25f;
    // Animator to monitor result
    public Animator Rig;

    public Balcony balconyScript;

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //OTHER ENTITY VARIABLES
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //For partygoers that are witnessing your actions
    public List<GameObject> audience = new List<GameObject> ();

    public Transform grabbingHand;
    public GameObject pickupItem;
    public Transform parentShell;
    public Collider intimatePlayer;

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //STANDARD FUNCTIONS
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    // Use this for initialization
    void Start () {
        //Set starting status variables
        parentShell = transform.parent;
    }

    // Update is called once per frame
    void FixedUpdate () {
        //Manual Quit
        if (Input.GetButtonUp (quitBtn)) {
            Lose ();
        }

        //Main Menu
        if (Input.GetButtonUp (startBtn)) {
            MainMenu ();
        }

        //Trigger Balcony Prompt
        if (mentalHealthScore <= goodEndTrigger) {
            PlayBalconyPrompt ();
        }

        if (mentalHealthScore <= balconyAccessible) {
            Debug.Log ("Balcony");
            balconyScript.OpenBalcony ();
        }

        //Gradual decrease of mental health over time
        if (mentalHealthScore > goodEndTrigger && !isNotGettingAnxious) {
            mentalHealthScore = mentalHealthScore - (healthDecay * Time.deltaTime);
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //GAMEPLAY FUNCTIONS
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    public void UpdateMentalHealthScore (int modifier) {

        //print ("player got " + modifier + " mental health");

        mentalHealthScore = mentalHealthScore + modifier;
        if (modifier > 0) {
            Invoke ("PlayPositiveAnimation", 1.5f);
        } else if (modifier < 0) {
            Invoke ("PlayNegativeAnimation", 1.5f);

            foreach (GameObject i in audience) {
                //print (i.name + "saw that");
                i.GetComponentInParent<Partygoer> ().SetMood (1.0f, this.gameObject);
                //Extra damage from each partygoer that witnessed
                mentalHealthScore = mentalHealthScore - i.GetComponentInParent<InteractableEntity> ().healthImpact;
            }
        } else {
            //Neutral result player animation - might be redundant
        }
    }

    public void PlayNegativeAnimation () {
        Rig.SetTrigger ("Failure");
    }

    public void PlayPositiveAnimation () {
        Rig.SetTrigger ("Success");
    }

    public void UpdateInventory (bool createOrDestroy, GameObject item) {
        if (createOrDestroy == true) {
            pickupItem = Instantiate (item, grabbingHand.position, grabbingHand.rotation);
            pickupItem.transform.SetParent (grabbingHand);
        } else {
            Destroy (pickupItem, 0.8f);
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //TRIGGERS
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    private void OnTriggerEnter (Collider col) {
        //print("Collided with " + col);
        if (col.gameObject.tag == "intimatePartygoer" && col.gameObject.GetComponentInParent<Partygoer> ().anger < 1.0 ||
            col.gameObject.tag == "interactable") {

            col.gameObject.GetComponentInParent<InteractableEntity> ().DisplayOptions (true);
        }
        //Partygoers that start looking at you
        else if (col.gameObject.tag == "partyGoer" && !audience.Contains (col.gameObject)) {
            //print (col.gameObject.name + " is watching you");
            audience.Add (col.gameObject);
        }
    }

    private void OnTriggerStay (Collider col) {
        //print("Collided with " + col);
        if (col.gameObject.tag == "intimatePartygoer" && col.gameObject.GetComponentInParent<Partygoer> ().anger < 1.0 ||
            col.gameObject.tag == "interactable") {

            if (Input.GetButtonUp (Interact1Btn) && !playerOnCooldown) {
                //Perform Interact 1 actions for collided object
                //print("Interact 1 with " + col.name);
                Rig.SetTrigger ("Interact1");
                StartCoroutine (ActionCooldown ());
                col.gameObject.GetComponent<InteractableEntity> ().Interact ("Interact1", this.gameObject);
                //parentShell.LookAt(new Vector3(col.transform.position.x, this.transform.position.y,col.transform.position.z));

            }

            if (Input.GetButtonUp (Interact2Btn) && !playerOnCooldown &&
                col.gameObject.GetComponent<InteractableEntity> ().hasInteraction2 == true) {
                //Perform Interact 2 actions for collided object
                //print("Interact 2");
                Rig.SetTrigger ("Interact2");
                col.gameObject.GetComponent<InteractableEntity> ().Interact ("Interact2", this.gameObject);
                StartCoroutine (ActionCooldown ());
            }

            if (Input.GetButtonUp (TakeGiveBtn) && !playerOnCooldown) {
                //Player can pick up the item
                if (col.gameObject.GetComponent<InteractableEntity> ().canBePickedUp == true && isHoldingItem == false) {
                    Rig.SetTrigger ("Pickup");
                    Rig.SetLayerWeight (1, 1f);
                    //Give player object
                    print ("player took " + pickupItem);
                    isHoldingItem = true;

                    //Interaction with object
                    col.gameObject.GetComponent<InteractableEntity> ().Interact ("Take", this.gameObject);
                    StartCoroutine (ActionCooldown ());
                }

                //Player can give item
                else if (col.gameObject.GetComponent<InteractableEntity> ().canReceive == true && isHoldingItem == true) {
                    Rig.SetTrigger ("Drop");
                    Rig.SetLayerWeight (1, 0f);
                    //Give player object
                    print ("player gave " + pickupItem);
                    isHoldingItem = false;

                    col.gameObject.GetComponent<InteractableEntity> ().Interact ("Give", this.gameObject);
                    StartCoroutine (ActionCooldown ());
                }
            }
        }
        /*
        else if (col.gameObject.tag == "intimatePartygoer" && col.gameObject.GetComponentInParent<Partygoer>().anger >= 1.0)
        {
            print(col.gameObject.name + "already hates you");
        }*/
    }

    private void OnTriggerExit (Collider col) {
        if (col.gameObject.tag == "intimatePartygoer" ||
            col.gameObject.tag == "interactable") {

            col.gameObject.GetComponentInParent<InteractableEntity> ().DisplayOptions (false);
        }
        //Partygoers that stop looking at you
        else if (col.gameObject.tag == "partyGoer" && audience.Contains (col.gameObject)) {
            //print (col.gameObject.name + " is no longer watching you");
            audience.Remove (col.gameObject);
        }
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //EVENT TRIGGERS
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //When player has run out of possible interactions and should go to the balcony or leave
    void PlayBalconyPrompt () {
        //print ("Player is ready for ending sequence");
        //Make all attendees hostile
        //Highlight balcony
    }

    //When player has entered the balcony (to trigger the cutscene)

    void PlayEndingSequence () {
        //Trigger by interaction with balcony area
        //print ("Player is on the balcony");
        //Play balcony cinematic
        //Go to end screen (Win)
        Win ();
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //MENU CONTROLS
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    void Lose () {
        //print ("you lose");
        //Set menu to Lose Screen
        ApplicationModel.menuState = 2;
        SceneManager.LoadScene (0);
    }

    void Win () {
        //print ("you win");
        //Set menu to Win Screen
        ApplicationModel.menuState = 1;
        SceneManager.LoadScene (0);
    }

    void MainMenu () {
        //Set menu to Main Menu Screen
        ApplicationModel.menuState = 0;
        SceneManager.LoadScene (0);
    }

    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    //COROUTINES
    //////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
    IEnumerator ActionCooldown () {
        playerOnCooldown = true;
        //print("player on cooldown");
        yield return new WaitForSeconds (interactionCooldown);
        playerOnCooldown = false;
    }

    IEnumerator AnxietyPauseCooldown () {
        isNotGettingAnxious = true;
        //print("player on cooldown");
        yield return new WaitForSeconds (idleAnxietyTime);
        isNotGettingAnxious = false;
    }
}