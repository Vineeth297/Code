using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerControl : MonoBehaviour
{
	public static PlayerControl playerControl;
	
	[Range(1,500)]
	[SerializeField] private float speed = 1f;

	[SerializeField] private float fallMultiplier = 0.2f;
	[SerializeField] private float lowJumpMultiplier = 2f;
	[SerializeField] private float jumpForce = 2f;

	public bool isGrounded;

	[HideInInspector] public float horizontalAxis;

	private float _lastYPosition;

	[HideInInspector] public Rigidbody rb;

	private void Awake()
	{
		playerControl = this;
	}
	
	private void Start()
	{
		isGrounded = true;
		rb = GetComponent<Rigidbody>();
		_lastYPosition = transform.position.y;
	}

	private void FixedUpdate()
	{
		horizontalAxis = Input.GetAxis("Horizontal");
		
		Vector3 movement = new Vector3(horizontalAxis,0f,0f);
		movement = movement.normalized * (speed * Time.deltaTime);
		rb.MovePosition(transform.position + movement);
		
		RealisticGravity();
	}

	private void Update()	 	
	{
		IsPlayerOnGrounded();
		
		if (isGrounded)
		{
			if (Input.GetKeyDown("up"))
			{
				rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
				isGrounded = false;
			}
		}
		//PerfectLanding();
	}

	private void IsPlayerOnGrounded()
	{
		Vector3 rayOrigin = transform.position ;
		Ray ray = new Ray(rayOrigin, Vector3.down);
		if (Physics.Raycast(ray, out var hit, 0.6f))
		{ 
			isGrounded = true;
			Debug.DrawLine(rayOrigin, hit.point);
		}
		else
			isGrounded = false;
	}

	private void RealisticGravity()
	{
		if (rb.velocity.y < 0f)
			rb.velocity += Vector3.up * (Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
		else if (rb.velocity.y > 0f)
			rb.velocity += Vector3.up * (Physics.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime);
	}
}


