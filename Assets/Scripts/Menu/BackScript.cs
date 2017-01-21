using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class BackScript : MonoBehaviour {
	
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
		this.gameObject.GetComponent<AudioSource>().Play();
		GameObject.Find ("Back").GetComponent<Collider2D> ().enabled = false;
		StartCoroutine(LoadNext());
	}

	IEnumerator LoadNext()
	{
		yield return new WaitForSeconds (doorScript.SetOpen (false));
		SceneManager.LoadScene ("Menu");
	}

}