using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PadMenuScript : MonoBehaviour {

	// Current level
	int currentLevel = 0;
	// Is enabled?
	bool continueEnabled = false;
	// Door script
	BeginDoorScript doorScript;
	// Disabled pitch
	public float disabledPitch = 2f;
	// Original pitch
	float pitch;
	// The three buttons and the door
	public GameObject newButton, continueButton, creditsButton, exitButton, door;
	// The three pointers and the door
	public GameObject newPointer, continuePointer, creditsPointer, exitPointer;
	// Current option
	int current = 0;
	// Is the user input enabled?
	bool inputEnabled = false;

	// Use this for initialization
	void Start () {
		// Get all the stuff
		currentLevel = PlayerPrefs.GetInt("CurrentLevel");
		doorScript = door.GetComponent<BeginDoorScript>();

		// Pitch for the audio button
		pitch = this.gameObject.GetComponent<AudioSource> ().pitch;

		// Enable or disable the continue button
		if (currentLevel < 2) {
			continueButton.transform.GetChild (0).GetComponent<SpriteRenderer> ().color = new Color (0.5f, 0.5f, 0.5f, 1f);
			continueEnabled = false;
		} else {
			continueButton.transform.GetChild (0).GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
			continueEnabled = true;
		}

		// Start the control loop
		StartCoroutine(ControlLoop());
		// Start the unlock coortutine
		StartCoroutine(EnableInputDelayed());

		// Initialize the pointer
		newPointer.SetActive (true);
		continuePointer.SetActive (false);
		creditsPointer.SetActive (false);
		exitPointer.SetActive (false);
	}
		
	void Update() {
		if ((Input.GetKeyDown (KeyCode.JoystickButton0) || Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.A)) && inputEnabled) {
			// Check for an input
			switch (current) {
			case 0:
                    // New game case
				PlayerPrefs.SetInt("CurrentLevel", 1);
				this.gameObject.GetComponent<AudioSource> ().pitch = pitch;
				this.gameObject.GetComponent<AudioSource> ().Play ();
				inputEnabled = false;
				StartCoroutine (LoadNewGame ());
				break;
			case 1:
				// Continue case
				if (continueEnabled) {
					this.gameObject.GetComponent<AudioSource> ().pitch = pitch;
					this.gameObject.GetComponent<AudioSource> ().Play ();
					inputEnabled = false;
					StartCoroutine (LoadContinue ());
				} else {
					this.gameObject.GetComponent<AudioSource> ().pitch = disabledPitch;
					this.gameObject.GetComponent<AudioSource> ().Play ();
				}
				break;
			case 2:
				// Credits case
				this.gameObject.GetComponent<AudioSource> ().pitch = pitch;
				this.gameObject.GetComponent<AudioSource> ().Play ();
				inputEnabled = false;
				StartCoroutine (LoadCredits ());
				break;			
			case 3:
				// Exit case
				this.gameObject.GetComponent<AudioSource> ().pitch = pitch;
				this.gameObject.GetComponent<AudioSource> ().Play ();
				inputEnabled = false;
				StartCoroutine (ExitGame());
				break;
			}
		}	
	}

	IEnumerator ControlLoop()
	{
		while (true) {
			yield return new WaitForSeconds (0.1f);

			if (inputEnabled) {
				int input = (int)Input.GetAxisRaw ("Vertical");
				// Update current
				if (input == 1) {
					current--;	
				} else if (input == -1) {
					current++;
				}
				// Normalize it
				if (current > 3)
					current = 3;
				if (current < 0)
					current = 0;

				// Show the selected button
				switch (current) {
				case 0:
					newPointer.SetActive (true);
					continuePointer.SetActive (false);
					creditsPointer.SetActive (false);
					exitPointer.SetActive (false);
					break;
				case 1:
					newPointer.SetActive (false);
					continuePointer.SetActive (true);
					creditsPointer.SetActive (false);
					exitPointer.SetActive (false);
					break;
				case 2:
					newPointer.SetActive (false);
					continuePointer.SetActive (false);
					creditsPointer.SetActive (true);
					exitPointer.SetActive (false);
					break;
				case 3:
					newPointer.SetActive (false);
					continuePointer.SetActive (false);
					creditsPointer.SetActive (false);
					exitPointer.SetActive (true);
					break;
				}
			}
		}
	}

	IEnumerator LoadNewGame()
	{
		yield return new WaitForSeconds (doorScript.SetOpen (false));
		SceneManager.LoadScene ("Level1");
	}

	IEnumerator LoadContinue()
	{
		yield return new WaitForSeconds (doorScript.SetOpen (false));
		SceneManager.LoadScene ("Level" + currentLevel);
	}	

	IEnumerator LoadCredits()
	{
		yield return new WaitForSeconds (doorScript.SetOpen (false));
		SceneManager.LoadScene ("Credits");
	}

	IEnumerator ExitGame()
	{
		yield return new WaitForSeconds (doorScript.SetOpen (false));
		Application.Quit();
	}

	IEnumerator EnableInputDelayed() {
		yield return new WaitForSeconds (doorScript.GetOpeningTime ());
		inputEnabled = true;
	}
}