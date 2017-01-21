using UnityEngine;
using System.Collections;

public class PlayerCharacterMovement : MonoBehaviour
{
	public bool IsOnGround
	{
		get
		{ 
			return isOnGround;
		}
	}

	public float movementForce;
	public float jumpForce;
	public Transform guyCollisionChecker1;
	public Transform guyCollisionChecker2;
	public LayerMask whatIsGround;
	public LayerMask whatIsWater;
	public Collider2D onGroundBoxCollider;
	public BoxCollider2D inWaterBoxCollider;

	private float movementDirection;
	private float climbDirection;
	private Rigidbody2D rigidbody2d;
	private bool isOnGround;
	private bool isInWater;
	private GameObject boat;
	private bool boated;
	private bool canMove;
	public bool canClimb;
	float originalGravity;

	private void Start()
	{

		canMove = true;
		rigidbody2d = GetComponent<Rigidbody2D>();
		originalGravity = rigidbody2d.gravityScale;
		boated = false;
		canClimb = false;
	}

	private void Update()
	{
		if (isOnGround)
		{
			canMove = true;
		}
		movementDirection = Input.GetAxisRaw("Horizontal");
        if (canClimb)
        {
            climbDirection = Input.GetAxisRaw("Vertical");
        }
        else
        {
            climbDirection = 0f;
        }

	}

	private void FixedUpdate()
	{

		if (boated)
		{
			this.gameObject.transform.rotation = boat.transform.rotation;
		}

		if (canClimb)
		{
			rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, climbDirection * movementForce);
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
            rigidbody2d.velocity = new Vector2(0, rigidbody2d.velocity.y);
			canMove = false;
			ActivateWaterCollider();
		}

		//Jump
		isOnGround = Physics2D.OverlapArea(guyCollisionChecker1.position, guyCollisionChecker2.position, whatIsGround);
		if (isOnGround && Input.GetButton("Jump") && !isInWater && !canClimb)
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

	private void OnCollisionStay2D(Collision2D other)
	{
		if (other.gameObject.tag == "Boat")
		{
			Debug.Log("BOAT");
			boated = true;
			this.boat = other.gameObject;
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.tag == "Boat")
		{
			boated = false;
			this.boat = null;
			this.gameObject.transform.eulerAngles = Vector3.zero;
		}
	}

	private void OnTriggerStay2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Stairs") && isOnGround)
		{
			rigidbody2d.gravityScale = 0f;
			canClimb = true;
		}
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.gameObject.CompareTag("Stairs"))
		{
            rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0);
            canClimb = false;
			rigidbody2d.gravityScale = originalGravity;
			
		}
	}

}
