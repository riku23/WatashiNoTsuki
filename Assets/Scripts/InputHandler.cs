using UnityEngine;
using System.Collections;

public class InputHandler : MonoBehaviour
{

	public bool GetJumpInput
	{
		get
		{
			return jumpInput;
		}
	}

	public float GetClimbDirection
	{
		get
		{
			return climbDirection;
		}
	}

	public float GetMovementDirection
	{
		get
		{
			return movementDirection;
		}
	}

	private float movementDirection;
	private float climbDirection;
	private bool jumpInput;

	void Start()
	{
		
	}


	void Update()
	{
		movementDirection = Input.GetAxisRaw("Horizontal");
		climbDirection = Input.GetAxisRaw("Vertical");
		jumpInput = Input.GetButtonDown("Jump");
	}

	void FixedUpdate()
	{
		
	}
}
