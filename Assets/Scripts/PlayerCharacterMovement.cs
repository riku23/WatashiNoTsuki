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

	public bool IsInWater
	{
		get
		{
			return isInWater;
		}
	}

	public bool IsOnVictoryPlatform
	{
		get
		{
			return isOnVictoryPlatform;
		}
	}

	public bool CanMove
	{
		set
		{
			canMove = value;
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
	private Animator anim;
	private Rigidbody2D rigidbody2d;
	private bool isOnGround;
	private bool isInWater;
	private bool isOnVictoryPlatform;
	private GameObject boat;
	private bool boated;
	private bool canMove;
	public bool canClimb;
	private bool isJumping;
	private float xOld;
	private float xNew;
	float originalGravity;

	private void Start()
	{

		canMove = true;
		anim = GetComponent<Animator>();
		rigidbody2d = GetComponent<Rigidbody2D>();
		originalGravity = rigidbody2d.gravityScale;
		boated = false;
		canClimb = false;
		#if UNITY_EDITOR
		gameObject.AddComponent<InputHandler>();
		#else
		StartCoroutine(GiveControls());
		#endif
	}

	private IEnumerator GiveControlsAfterDelay()
	{
		float delay = GameObject.Find("Door").GetComponent<BeginDoorScript>().GetOpeningTime();
		yield return new WaitForSeconds(delay);
		gameObject.AddComponent<InputHandler>();
	}

	private void Update()
	{
		anim.SetBool("isOnGround", isOnGround);

		if (isOnGround)
		{
			canMove = true;
		}
		if (gameObject.GetComponent<InputHandler>() != null)
		{
			movementDirection = gameObject.GetComponent<InputHandler>().GetMovementDirection;
		}
		else
		{
			movementDirection = 0f;
		}
		if (canClimb)
		{
			if (gameObject.GetComponent<InputHandler>() != null)
			{
				climbDirection = gameObject.GetComponent<InputHandler>().GetClimbDirection;
			}
			else
			{
				climbDirection = 0f;
			}
		}
		else
		{
			climbDirection = 0f;
		}
		int animVelocity = (int)Mathf.Clamp(movementDirection * movementForce, -1f, 1f);
		anim.SetInteger("velocityInt", animVelocity);
		if (animVelocity == 1)
		{
			transform.localScale = new Vector3(1, 1, 1);
		}
		if (animVelocity == -1)
		{
			transform.localScale = new Vector3(-1, 1, 1);
		}
       
	}

	private void FixedUpdate()
	{
		/*if (xNew - xOld > 0)
			isJumping = true;
		else
			isJumping = false;*/
		if (boated)
		{
			this.gameObject.transform.rotation = boat.transform.rotation;
		}

		if (canClimb)
		{
            
			rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, climbDirection * movementForce);
			if (rigidbody2d.velocity.y != 0f)
			{
				anim.SetBool("Climb", true);
			}
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
		if (isOnGround && /*!isJumping &&*/ !isInWater && !canClimb && gameObject.GetComponent<InputHandler>() != null && gameObject.GetComponent<InputHandler>().GetJumpInput)
		{
			rigidbody2d.velocity = new Vector2(rigidbody2d.velocity.x, 0);
			rigidbody2d.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
		}


    
	}

	private void ActivateGroundCollider()
	{
		anim.SetBool("Swim", false);
		onGroundBoxCollider.enabled = true;
		inWaterBoxCollider.enabled = false;
	}

	private void ActivateWaterCollider()
	{
		anim.SetBool("Swim", true);
		onGroundBoxCollider.enabled = false;
		inWaterBoxCollider.enabled = true;
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Victory Platform"))
		{
			isOnVictoryPlatform = true;
		}
		else
		{
			isOnVictoryPlatform = false;
		}
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
		isOnVictoryPlatform = false;
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
			anim.SetBool("Climb", false);
		}
	}



}
