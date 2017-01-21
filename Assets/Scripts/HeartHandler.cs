using UnityEngine;
using System.Collections;

public class HeartHandler : MonoBehaviour
{
	public float totalDistance;
	public float speed;
	//distance per frame

	private float currentDistance;

	void Update()
	{
		currentDistance += speed;
		Vector2 newPosition = transform.position;
		newPosition.y += speed;
		newPosition.x += Mathf.Sin(currentDistance);
		transform.position = newPosition;
		if (currentDistance > totalDistance)
		{
			Destroy(gameObject);
		}
	}
}
