using UnityEngine;
using System.Collections;

public class BeginDoorScript : MonoBehaviour
{

	// Left and rigth door
	public GameObject doorLeft, doorRigth;
	// Stop
	// bool stop = false;
	// Speed
	public float speed = 2f;
	// x value of target position
	public float xTarget = 15f;
	// Target position
	Vector3 leftTarget, rigthTarget;
	// Starting position
	Vector3 leftStart, rigthStart;
	// Support variables
	float step, z;
	Vector3 delta;
	// Open
	public bool open = true;

	// Use this for initialization
	void Start()
	{
		leftStart = doorLeft.transform.position;
		rigthStart = doorRigth.transform.position;
		z = leftStart.z;
		leftTarget = new Vector3(xTarget * -1, leftStart.y, z);
		rigthTarget = new Vector3(xTarget, leftStart.y, z);
	}
	
	// Update is called once per frame
	void Update()
	{
		step = Time.deltaTime * speed;

		if (open)
		{
			// Left door
			delta = Vector3.MoveTowards(doorLeft.transform.position, leftTarget, step);
			doorLeft.transform.position = delta;
			// Rigth door
			delta = Vector3.MoveTowards(doorRigth.transform.position, rigthTarget, step);
			doorRigth.transform.position = delta;
		}
		else
		{
			// Left door
			delta = Vector3.MoveTowards(doorLeft.transform.position, leftStart, step);
			doorLeft.transform.position = delta;
			// Rigth door
			delta = Vector3.MoveTowards(doorRigth.transform.position, rigthStart, step);
			doorRigth.transform.position = delta;
		}
	}

	public float SetOpen(bool b)
	{
		open = b;
		return Mathf.Abs(doorRigth.transform.position.x - rigthStart.x) / speed;
	}

	public float GetOpeningTime()
	{
		return Mathf.Abs(rigthStart.x - rigthTarget.x) / speed;
	}

}