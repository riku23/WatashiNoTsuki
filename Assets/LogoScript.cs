using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class LogoScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
		// Disable the mouse
		Cursor.visible = false;
		StartCoroutine(LoadNext());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator LoadNext()
	{
		yield return new WaitForSeconds (1.5f);
		SceneManager.LoadScene ("Story1");
	}

}