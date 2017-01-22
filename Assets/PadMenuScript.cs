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
	public GameObject newButton, continueButton, creditsButton, door;
	// The three pointers and the door
	public GameObject newPointer, continuePointer, creditsPointer;
	// Current option
	int current = 0;
	// Already selected?
	bool selected = false;

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
	}
		
	void Update() {
		if (Input.GetKeyDown (KeyCode.JoystickButton0) || Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.A)) {
			// Check for an input
			switch (current) {
			case 0:
                    // New game case
                    PlayerPrefs.SetInt("CurrentLevel", 1);
				this.gameObject.GetComponent<AudioSource> ().pitch = pitch;
				this.gameObject.GetComponent<AudioSource> ().Play ();
				selected = true;
				StartCoroutine (LoadNewGame ());
				break;
			case 1:
				// Continue case
				if (continueEnabled) {
					this.gameObject.GetComponent<AudioSource> ().pitch = pitch;
					this.gameObject.GetComponent<AudioSource> ().Play ();
					GameObject.Find ("New Game").GetComponent<Collider2D> ().enabled = false;
					StartCoroutine (LoadContinue ());
				} else {
					this.gameObject.GetComponent<AudioSource> ().pitch = disabledPitch;
					this.gameObject.GetComponent<AudioSource> ().Play ();
				}
				break;
			case 2:
				// Continue case
				this.gameObject.GetComponent<AudioSource> ().pitch = pitch;
				this.gameObject.GetComponent<AudioSource> ().Play ();
				selected = true;
				StartCoroutine (LoadCredits ());
				break;
			}
		}	
	}

	IEnumerator ControlLoop()
	{
		while (true) {
			yield return new WaitForSeconds (0.1f);

			if (!selected) {
				int input = (int)Input.GetAxisRaw ("Vertical");
				// Update current
				if (input == 1) {
					current--;	
				} else if (input == -1) {
					current++;
				}
				// Normalize it
				if (current > 2)
					current = 2;
				if (current < 0)
					current = 0;

				// Show the selected button
				switch (current) {
				case 0:
					newPointer.SetActive (true);
					continuePointer.SetActive (false);
					creditsPointer.SetActive (false);
					break;
				case 1:
					newPointer.SetActive (false);
					continuePointer.SetActive (true);
					creditsPointer.SetActive (false);
					break;
				case 2:
					newPointer.SetActive (false);
					continuePointer.SetActive (false);
					creditsPointer.SetActive (true);
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

}