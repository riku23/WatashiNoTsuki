using UnityEngine;
using System.Collections;

public class HeartHandler : MonoBehaviour
{
	public float totalDistance;
	public float speed;
	public float nCicli;
	//distance per frame

	private float currentDistance;

	void Update()
	{
		currentDistance += speed;
		Vector2 newPosition = transform.position;
		newPosition.y += speed;
		newPosition.x += 0.02f * Mathf.Sin(nCicli * 2 * Mathf.PI * currentDistance / totalDistance);
		transform.position = newPosition;
		if (currentDistance > totalDistance)
		{
			Destroy(gameObject);
		}

	}
}
