using UnityEngine;
using System.Collections;

public class VictoryScript : MonoBehaviour
{
	CircleCollider2D victoryCollider;
	public float victoryHeight;
	bool isRightPosition;

	void Start()
	{
		victoryCollider = new CircleCollider2D();
	}

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
		print(isRightPosition);
	}

	void OnTriggerStay2D(Collider2D collider)
	{
		if (isRightPosition)
		{
			print("Victory!");
		}
	}
}
