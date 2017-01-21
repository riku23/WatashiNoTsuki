using UnityEngine;
using System.Collections;

public class OrbitScript : MonoBehaviour
{
	public float a, b, increment, t;
	private float x, y;
	private int rotationDirection;

	void Update()
	{

		if ((Input.GetAxis("RotationAxis") < 0) || Input.GetKey(KeyCode.E))
		{
			rotationDirection = -1;
		}
		else if (Input.GetAxis("RotationAxis") > 0 || Input.GetKey(KeyCode.Q))
		{
			rotationDirection = 1;
		}
		else
		{
			rotationDirection = 0;
		}
	}

	void FixedUpdate()
	{
		t += increment * rotationDirection;
		x = a * Mathf.Cos(t);
		y = b * Mathf.Sin(t);
		this.gameObject.transform.position = new Vector2(x, y); 
	}
}
