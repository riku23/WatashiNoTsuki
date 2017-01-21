using UnityEngine;
using System.Collections;

public class VictoryScript : MonoBehaviour
{
	public float victoryHeight;
	bool isRightPosition;
	public GameObject player;
	bool spawnedHearts;

	void Update()
	{
		if (transform.position.y > victoryHeight)
		{
			isRightPosition = true;
		}
		else
		{
			isRightPosition = false;
		}
		//print(isRightPosition);
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		print(collider.gameObject.CompareTag("Player"));
		if (isRightPosition && collider.gameObject.CompareTag("Player"))
		{
			print("Victory!");
			if (!spawnedHearts)
			{
				spawnedHearts = true;
				collider.gameObject.GetComponent<HeartSpawner>().SpawnHearts();
			}
		}
	}


}
