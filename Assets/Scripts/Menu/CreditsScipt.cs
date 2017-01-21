using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class CreditsScipt : MonoBehaviour {
	
	// Door
	GameObject door;
	// Door script
	BeginDoorScript doorScript;

	// Use this for initialization
	void Start () {
		door = GameObject.Find("Door");
		doorScript = door.GetComponent<BeginDoorScript>();	
	}

	void OnMouseDown()
	{
		StartCoroutine(LoadNext());
	}

	IEnumerator LoadNext()
	{
		yield return new WaitForSeconds (doorScript.SetOpen (false));
		SceneManager.LoadScene ("Credits");
	}

}