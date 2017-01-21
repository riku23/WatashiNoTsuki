using UnityEngine;
using System.Collections;

public class PlayerCharacterMovement : MonoBehaviour
{
	public float movementForce;
	public float jumpForce;
	public Transform guyCollisionChecker1;
	public Transform guyCollisionChecker2;
	public LayerMask whatIsGround;
	public LayerMask whatIsWater;
	public Collider2D onGroundBoxCollider;
	public BoxCollider2D inWaterBoxCollider;

	private float movementDirection;
	private Rigidbody2D rigidbody2d;
	private bool isOnGround;
	private bool isInWater;
	private GameObject boat;
	private bool boated;
	private bool canMove;

	private void Start()
	{
		canMove = true;
		rigidbody2d = GetComponent<Rigidbody2D>();
		boated = false;
	}

	private void Update()
	{
		if (isOnGround)
		{
			canMove = true;
		}
		movementDirection = Input.GetAxisRaw("Horizontal");
		if (boated)
		{
			this.gameObject.transform.eulerAngles = new Vector3(boat.transform.rotation.z, 0, 0);
		}

	}

	private void FixedUpdate()
	{

		if (boated)
		{
			this.gameObject.transform.rotation = boat.transform.rotation;
		}
        
		//Horizontal movement
		isInWater = Physics2D.OverlapArea(guyCollisionChecker1.position, guyCollisionChecker2.position, whatIsWater);
		if (!isInWater)
		{
			ActivateGroundCollider();
			if (canMove)
			{
				rigidbody2d.velocity = new Vector2(movementDirection * movementForce, rigidbody2d.velocity.y);
			}
		}
		else
		{
			canMove = false;
			ActivateWaterCollider();
		}

		//Jump
		isOnGround = Physics2D.OverlapArea(guyCollisionChecker1.position, guyCollisionChecker2.position, whatIsGround);
		if (isOnGround && Input.GetButton("Jump") && !isInWater)
		{
			rigidbody2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
		}
	}

	private void ActivateGroundCollider()
	{
		onGroundBoxCollider.enabled = true;
		inWaterBoxCollider.enabled = false;
	}

	private void ActivateWaterCollider()
	{
		onGroundBoxCollider.enabled = false;
		inWaterBoxCollider.enabled = true;
	}
	/*
    private void OnCollisionStay2D(Collision2D other)
    {
        if(other.gameObject.tag == "Boat")
        {
            Debug.Log("BOAT");
            boated = true;
            this.boat = other.gameObject;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if(other.gameObject.tag == "Boat")
        {
            boated = false;
            this.boat = null;
            this.gameObject.transform.eulerAngles = Vector3.zero;
        }
    }
    */
}
