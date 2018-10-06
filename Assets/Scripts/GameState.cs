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

    //State Variables
    public int mentalHealthScore;

    // Use this for initialization
    void Start () {
        //Set starting status variables
        mentalHealthScore = 100;
	}

	// Update is called once per frame
	void Update () {
        //Manual Quit
        if (Input.GetButtonUp(quitBtn))
        {
            Lose();
        }

        //Manual Win
        if (Input.GetButtonUp(startBtn))
        {
            Win();
        }

        //Manual Main Menu return
        if (Input.GetKeyUp("space"))
        {
            MainMenu();
        }

        //Trigger Balcony Prompt
        if (mentalHealthScore <= 0)
        {
            PlayBalconyPrompt();
        }

        if (Input.GetButtonUp(Interact1Btn))
        {
            print("Interact 1");
        }

        if (Input.GetButtonUp(Interact2Btn))
        {
            print("Interact 2");
        }

        if (Input.GetButtonUp(TakeGiveBtn))
        {
            print("Interact Take Give");
        }
    }

    //When player has run out of possible interactions and should go to the balcony or leave
    void PlayBalconyPrompt()
    {
        print("Player is ready for ending sequence");
        //Make all attendees hostile
        //Highlight balcony
    }

    //When player has entered the balcony (to trigger the cutscene)
    void PlayEndingSequence()
    {
        print("Player is ready for ending sequence");
        //Play balcony cinematic
        Win();
    }

	void Lose(){
		//print ("you lose");
		//Set menu to Lose Screen
		ApplicationModel.menuState = 2;
		SceneManager.LoadScene(0);
	}

	void Win(){
		//print ("you win");
		//Set menu to Win Screen
		ApplicationModel.menuState = 1;
		SceneManager.LoadScene(0);
	}

    void MainMenu()
    {
        //Set menu to Main Menu Screen
        ApplicationModel.menuState = 0;
        SceneManager.LoadScene(0);
    }
}
