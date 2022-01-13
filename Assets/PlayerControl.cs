using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerControl : MonoBehaviour
{
	public static PlayerControl _playerControl;
	
	[Range(1,500)]
	[SerializeField] private float _speed = 1f;

	[SerializeField] private float fallMultiplier = 0.2f;
	[SerializeField] private float lowJumpMultipler = 2f;
	[SerializeField] private float _jumpForce = 2f;

	public bool isGrounded;

	[HideInInspector] public float horizontalAxis;

	private float lastYPosition;

	[HideInInspector] public Rigidbody _rb;

	void Awake()
	{
		_playerControl = this;
	}
	
	private void Start()
	{
		isGrounded = true;
		_rb = GetComponent<Rigidbody>();
		lastYPosition = transform.position.y;
	}

	private void FixedUpdate()
	{
		horizontalAxis = Input.GetAxis("Horizontal");
		
		Vector3 movement = new Vector3(horizontalAxis,0f,0f);
		movement = movement.normalized * (_speed * Time.deltaTime);
		_rb.MovePosition(transform.position + movement);
		
		
		
		RealisticGravity();
	}

	void Update()	 	
	{
		IsPlayerOnGrounded();
		if (isGrounded)
		{
			print("here");
			if (Input.GetKeyDown("up"))
			{
				print("Jump");
				_rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
				isGrounded = false;
			}
		}
		//PerfectLanding();
	}

	void IsPlayerOnGrounded()
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

	void RealisticGravity()
	{
		if (_rb.velocity.y < 0f)
			_rb.velocity += Vector3.up * (Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
		else if (_rb.velocity.y > 0f)
			_rb.velocity += Vector3.up * (Physics.gravity.y * (lowJumpMultipler - 1) * Time.deltaTime);
	}
}


