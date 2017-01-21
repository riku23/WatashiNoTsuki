using UnityEngine;
using System.Collections;

public class VictoryScript : MonoBehaviour
{
	public float victoryHeight;
	bool isRightPosition;
	public GameObject player;

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
		if (isRightPosition && collider.gameObject.Equals(player))
		{
			print("Victory!");
		}
	}


}
