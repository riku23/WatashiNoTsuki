using UnityEngine;
using System.Collections;

public class Vehicle : MonoBehaviour
{
	public float movementForce;

	private bool playerIsControlling;
	private float movementDirection;
	private Rigidbody2D rigidbody2d;

	private void Start()
	{
		rigidbody2d = GetComponent<Rigidbody2D>();
		playerIsControlling = false;
	}

	private void Update()
	{
		if (playerIsControlling)
		{
			movementDirection = Input.GetAxisRaw("Horizontal");
		}
	}

	private void FixedUpdate()
	{
		if (playerIsControlling)
		{
			rigidbody2d.velocity = new Vector2(movementDirection * movementForce, rigidbody2d.velocity.y);
		}
	}

	private void OnCollisionEnter2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			playerIsControlling = true;
		}
	}

	private void OnCollisionExit2D(Collision2D other)
	{
		if (other.gameObject.CompareTag("Player"))
		{
			playerIsControlling = false;
		}
	}
}