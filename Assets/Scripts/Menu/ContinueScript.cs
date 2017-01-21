using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ContinueScript : MonoBehaviour {
	
	// Current level
	int currentLevel = 0;
	// Is enabled?
	bool continueEnabled = false;
	// Door
	GameObject door;
	// Door script
	BeginDoorScript doorScript;
	// Disabled pitch
	public float disabledPitch = 2f;
	// Original pitch
	float pitch;

	// Use this for initialization
	void Start () {
		currentLevel = PlayerPrefs.GetInt("CurrentLevel");
		door = GameObject.Find("Door");
		doorScript = door.GetComponent<BeginDoorScript>();
		pitch = this.gameObject.GetComponent<AudioSource> ().pitch;

		if (currentLevel == 1) {
			this.transform.GetChild (0).GetComponent<SpriteRenderer> ().color = new Color (0.5f, 0.5f, 0.5f, 1f);
			continueEnabled = false;
		} else {
			this.GetComponent<SpriteRenderer> ().color = new Color (1f, 1f, 1f, 1f);
			continueEnabled = true;
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnMouseDown()
	{
		if (continueEnabled) {
			this.gameObject.GetComponent<AudioSource> ().pitch = pitch;
			this.gameObject.GetComponent<AudioSource> ().Play ();
			GameObject.Find ("New Game").GetComponent<Collider2D> ().enabled = false;
			GameObject.Find ("Continue").GetComponent<Collider2D> ().enabled = false;
			GameObject.Find ("Credits").GetComponent<Collider2D> ().enabled = false;
			StartCoroutine (LoadNext ());
		} else {
			this.gameObject.GetComponent<AudioSource> ().pitch = disabledPitch;
			this.gameObject.GetComponent<AudioSource> ().Play ();
		}
	}

	IEnumerator LoadNext()
	{
		yield return new WaitForSeconds (doorScript.SetOpen (false));
		SceneManager.LoadScene ("Level" + currentLevel);
	}

}