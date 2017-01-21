using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class VictoryScript : MonoBehaviour
{
	public static readonly int MAX_LEVELS = 3;

	public float victoryHeight;
	public float delayBeforeVictory;
	public int currentLevel;

	private bool isRightPosition;
	private bool spawnedHearts;

	private void Update()
	{
		if (transform.position.y > victoryHeight)
		{
			isRightPosition = true;
		}
		else
		{
			isRightPosition = false;
		}
	}

	private void OnTriggerStay2D(Collider2D collider)
	{
		if (isRightPosition && collider.gameObject.CompareTag("Player") && collider.gameObject.GetComponent<PlayerCharacterMovement>().IsOnVictoryPlatform)
		{
			Destroy(collider.gameObject.GetComponent<InputHandler>());
			Victory(collider);
		}
	}

	private void Victory(Collider2D collider)
	{
		if (!spawnedHearts)
		{
			spawnedHearts = true;
            collider.gameObject.GetComponent<Animator>().SetBool("Victory", true);
			collider.gameObject.GetComponent<HeartSpawner>().SpawnHearts();
			gameObject.GetComponent<HeartSpawner>().SpawnHearts();
		}
		StartCoroutine(LoadNext());
	}

	private IEnumerator LoadNext()
	{
		if (currentLevel < MAX_LEVELS)
		{
			PlayerPrefs.SetInt("CurrentLevel", currentLevel + 1);
			yield return new WaitForSeconds(delayBeforeVictory);
			yield return new WaitForSeconds(GameObject.Find("Door").GetComponent<BeginDoorScript>().SetOpen(false));
			SceneManager.LoadScene("Level" + (currentLevel + 1));
		}
		else
		{
			yield return new WaitForSeconds(delayBeforeVictory);
			yield return new WaitForSeconds(GameObject.Find("Door").GetComponent<BeginDoorScript>().SetOpen(false));
			SceneManager.LoadScene("Final");
		}
	}
}