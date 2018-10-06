using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour {

    //Input
    public string horizontalBtn = "Horizontal";
    public string verticalBtn = "Vertical";
    public string Interact1Btn = "Interact1";
    public string Interact2Btn = "Interact2";
    public string TakeGiveBtn = "TakeGive";

    public string startBtn = "Submit";
    public string quitBtn = "Quit";

    float interactionCooldown = 0f;

    //State Variables
    //Basic counter for how the player's mental health/anxiety is going
    public int mentalHealthScore = 100;
    //Is the player currently holding an item
    public bool isHoldingItem = false;
    //Is the player doing an action (use to prevent triggers while performing action)
    public bool isActing = false;
    //How long in seconds between interactions before anxiety kicks in
    public int idleAnxietyTime = 5;

    // Use this for initialization
    void Start () {
        //Set starting status variables
    }

    // Update is called once per frame
    void Update () {
        //Manual Quit
        if (Input.GetButtonUp (quitBtn)) {
            Lose ();
        }

        //Main Menu
        if (Input.GetButtonUp(startBtn))
        {
            MainMenu();
        }

        //Trigger Balcony Prompt
        if (mentalHealthScore <= 10) {
            PlayBalconyPrompt ();
        }
    }

    public void UpdateMentalHealthScore (int modifier) {
        mentalHealthScore = mentalHealthScore + modifier;
    }

    private void OnTriggerEnter (Collider col) {
        //print("Collided with " + col);
        if (col.gameObject.tag == "partyGoer" || col.gameObject.tag == "interactable") {
            //Display interaction context menu
        }
    }

    private void OnTriggerStay (Collider col) {
        //print("Collided with " + col);
        if (col.gameObject.tag == "partyGoer" || col.gameObject.tag == "interactable") {
            if (Input.GetButtonUp (Interact1Btn) && Time.time > interactionCooldown) {
                //Perform Interact 1 actions for collided object
                //print("Interact 1");
                col.gameObject.GetComponent<InteractableEntity> ().Interact ("Interact1", this.gameObject);
                interactionCooldown = Time.time + 0.5f;
            }

            if (Input.GetButtonUp(Interact2Btn) && Time.time > interactionCooldown && col.gameObject.GetComponent<InteractableEntity>().hasInteraction2 == true)
            {
                //Perform Interact 2 actions for collided object
                //print("Interact 2");
                col.gameObject.GetComponent<InteractableEntity> ().Interact ("Interact2", this.gameObject);
                interactionCooldown = Time.time + 0.5f;
            }

            if (Input.GetButtonUp (TakeGiveBtn) && Time.time > interactionCooldown) {
                //Take the object or give currently held object
                //print("Interact Take Give");
                col.gameObject.GetComponent<InteractableEntity> ().Interact ("TakeGive", this.gameObject);
                interactionCooldown = Time.time + 0.5f;
            }
        }
    }

    private void OnTriggerExit (Collider col) {
        if (col.gameObject.tag == "partygoer" || col.gameObject.tag == "interactable") {
            //Display interaction context menu
        }
    }

    //When player has run out of possible interactions and should go to the balcony or leave
    void PlayBalconyPrompt () {
        print ("Player is ready for ending sequence");
        //Make all attendees hostile
        //Highlight balcony
    }

    //When player has entered the balcony (to trigger the cutscene)

    void PlayEndingSequence()
    {
        //Trigger by interaction with balcony area
        print("Player is on the balcony");
        //Play balcony cinematic
        //Go to end screen (Win)
        Win();
    }

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
}