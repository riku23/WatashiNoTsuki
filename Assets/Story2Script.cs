using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Story2Script : MonoBehaviour {

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
		if (Input.anyKeyDown && inputEnabled)  {
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