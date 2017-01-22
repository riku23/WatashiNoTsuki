using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ToMenuScript : MonoBehaviour {
	
	// Door
	GameObject door;
	// Door script
	BeginDoorScript doorScript;
	// Has been exited?
	bool exited = false;

	// Use this for initialization
	void Start () {
		door = GameObject.Find("Door");
		doorScript = door.GetComponent<BeginDoorScript>();	
	}

	// Update is called once per frame
	void Update () {
		if (!(SceneManager.GetActiveScene().name == "Menu" || SceneManager.GetActiveScene().name == "Credits") && (Input.GetKeyDown (KeyCode.JoystickButton7) || Input.GetKeyDown (KeyCode.Escape)) && !exited) {
			exited = true;
			StartCoroutine (LoadNext ());
		}
	}
		
	IEnumerator LoadNext()
	{
		yield return new WaitForSeconds (doorScript.SetOpen (false));
		SceneManager.LoadScene ("Menu");
	}

}