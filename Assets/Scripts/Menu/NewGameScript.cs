using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewGameScript : MonoBehaviour {
	
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
		GameObject.Find ("New Game").GetComponent<Collider2D> ().enabled = false;
		GameObject.Find ("Continue").GetComponent<Collider2D> ().enabled = false;
		GameObject.Find ("Credits").GetComponent<Collider2D> ().enabled = false;
		StartCoroutine(LoadNext());
	}

	IEnumerator LoadNext()
	{
		yield return new WaitForSeconds (doorScript.SetOpen (false));
		SceneManager.LoadScene ("Level1-Marco");
	}

}