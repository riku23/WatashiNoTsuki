using UnityEngine;
using System.Collections;

public class HeartSpawner : MonoBehaviour
{

	public int heartAmount;
	public float heartSpawnRange;
	public float heartDelay;
	public GameObject heartPrefab;
	Vector2 spawnPosition;

	public void SpawnHearts()
	{
		StartCoroutine(SpawnCoroutine());
	}

	private IEnumerator SpawnCoroutine()
	{
		for (int i = 0; i < heartAmount; i++)
		{
			SpawnHeart();
			yield return new WaitForSeconds(heartDelay);
		}
	}

	private void SpawnHeart()
	{
		Vector2 randomPos = heartSpawnRange * Random.insideUnitCircle;
		randomPos.y = Mathf.Abs(randomPos.y);
		spawnPosition = (Vector2)transform.position + randomPos;
		Instantiate(heartPrefab, spawnPosition, Quaternion.identity);
	}
}
