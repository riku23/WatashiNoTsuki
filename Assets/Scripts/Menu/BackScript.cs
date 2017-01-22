using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackScript : MonoBehaviour {
	
	// Door
	GameObject door;
	// Door script
	BeginDoorScript doorScript;
	// Has been selected?
	bool selected = false;

	// Use this for initialization
	void Start () {
		door = GameObject.Find("Door");
		doorScript = door.GetComponent<BeginDoorScript>();	
	}
	
	void Update()
	{
		if ((Input.GetKeyDown (KeyCode.JoystickButton0) || Input.GetKeyDown (KeyCode.Return) || Input.GetKeyDown (KeyCode.A)) && !selected)  {
			selected = true;
			this.gameObject.GetComponent<AudioSource> ().Play ();
			StartCoroutine (LoadNext ());
		}
	}

	IEnumerator LoadNext()
	{
		yield return new WaitForSeconds (doorScript.SetOpen (false));
		SceneManager.LoadScene ("Menu");
	}

}