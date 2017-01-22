using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackScript : MonoBehaviour {
	
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
	
	void Update()
	{
		if ((Input.GetKeyDown (KeyCode.JoystickButton0) || Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.A)) && inputEnabled)  {
			inputEnabled = false;
			this.gameObject.GetComponent<AudioSource> ().Play ();
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