using UnityEngine;
using System.Collections;

public class OrbitScript : MonoBehaviour
{
	public float a, b, increment, t;
	private float x, y;
	private int rotationDirection;
	// Door
	private GameObject door;
	// Door script
	private BeginDoorScript doorScript;
	// Is input enabled?
	bool inputEnabled = false;

	void Start() {
		door = GameObject.Find("Door");
		doorScript = door.GetComponent<BeginDoorScript>();

		//Set the initial position
		t += increment * rotationDirection;
		x = a * Mathf.Cos (t);
		y = b * Mathf.Sin (t);
		this.gameObject.transform.position = new Vector2 (x, y);

		StartCoroutine(EnableInputDelayed());
	}

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
		if (inputEnabled) {
			t += increment * rotationDirection;
			x = a * Mathf.Cos (t);
			y = b * Mathf.Sin (t);
			this.gameObject.transform.position = new Vector2 (x, y);
		}
	}

	IEnumerator EnableInputDelayed() {
		yield return new WaitForSeconds (doorScript.GetOpeningTime ());
		inputEnabled = true;
	}

	public void SetInputEnabled(bool activeValue){
		inputEnabled = activeValue;
	}

}
