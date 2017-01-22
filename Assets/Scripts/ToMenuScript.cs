using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToMenuScript : MonoBehaviour {
	
	// Door
	GameObject door;
	// Door script
	BeginDoorScript doorScript;
	// Is input enabled?
	bool inputEnabled = false;

	// Use this for initialization
	void Start () {
		door = GameObject.Find("Door");
		doorScript = door.GetComponent<BeginDoorScript>();	
		// Start the unlock coortutine
		StartCoroutine(EnableInputDelayed());
	}

	// Update is called once per frame
	void Update () {
		if (!(SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "Credits" || SceneManager.GetActiveScene().name == "Story1" || SceneManager.GetActiveScene().name == "Story2") && (Input.GetKeyDown (KeyCode.JoystickButton7) || Input.GetKeyDown (KeyCode.Escape)) && inputEnabled) {
			inputEnabled = false;
			StartCoroutine (LoadNext ());
		}
	}
		
	IEnumerator LoadNext()
	{
		yield return new WaitForSeconds (doorScript.SetOpen (false));
		SceneManager.LoadScene ("Menu");
	}

	IEnumerator EnableInputDelayed() {
		yield return new WaitForSeconds (doorScript.GetOpeningTime ());
		inputEnabled = true;
	}

}