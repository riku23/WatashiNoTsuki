using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ContinueScript : MonoBehaviour {
	
	// Shadow
	public GameObject black;
	// Current level
	int currentLevel = 0;
	// Is enabled?
	bool continueEnabled = false;
	// Door
	GameObject door;
	// Door script
	BeginDoorScript doorScript;

	// Use this for initialization
	void Start () {
		currentLevel = PlayerPrefs.GetInt("CurrentLevel");
		currentLevel = 1;
		door = GameObject.Find("Door");
		doorScript = door.GetComponent<BeginDoorScript>();
			
		if (currentLevel == 0) {
			black.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, .5f);
			continueEnabled = false;
		} else {
			black.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 0f);
			continueEnabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		if (continueEnabled)
			StartCoroutine(LoadNext());
	}

	IEnumerator LoadNext()
	{
		yield return new WaitForSeconds (doorScript.SetOpen (false));
		SceneManager.LoadScene ("Level" + currentLevel);
	}

}