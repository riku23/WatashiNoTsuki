using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Story2Script : MonoBehaviour {

	// Door
	GameObject door;
	// Door script
	BeginDoorScript doorScript;

	// Use this for initialization
	void Start () {
		door = GameObject.Find("Door");
		doorScript = door.GetComponent<BeginDoorScript>();
		StartCoroutine(LoadNext());
	}

	IEnumerator LoadNext()
	{
		yield return new WaitForSeconds (6f);
		yield return new WaitForSeconds (doorScript.SetOpen (false));
		SceneManager.LoadScene ("Menu");
	}

}