using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	[Range(1,500)]
	[SerializeField] private float _speed = 1f;

	[SerializeField] private float fallMultiplier = 0.2f;
	[SerializeField] private float lowJumpMultipler = 2f;
	[SerializeField] private float _jumpForce = 2f;
	
	[SerializeField] private Transform _groundCheck;

	[SerializeField] private bool _isGrounded;

	private float movementX;

	private Rigidbody _rb;

	private void Start()
	{
		_isGrounded = true;
		_rb = GetComponent<Rigidbody>();
	}

	private void FixedUpdate()
	{
		movementX = Input.GetAxis("Horizontal");
		RealisticGravity();

		IsPlayerOnGrounded();
		
		Vector3 movement = new Vector3(movementX,0f,0f);
		//_rb.AddForce(movement * (_speed));
		_rb.MovePosition(movement * (_speed * Time.fixedDeltaTime));
	}

	void Update()
	{
		
		// if (Input.GetKey("right"))
		// {
		// 	transform.Translate(transform.TransformDirection(transform.right * (_speed * Time.deltaTime)), Space.World);
		// }
		//
		// if (Input.GetKey("left"))
		// {
		// 	transform.Translate(-transform.TransformDirection(transform.right * (_speed * Time.deltaTime)), Space.World);
		// }

		if (_isGrounded)
		{
			if (Input.GetKeyDown("up"))
			{
				_rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
				FlipCube();
				_isGrounded = false;
			}
		}
	}

	void IsPlayerOnGrounded()
	{
		if (Physics.Raycast(_groundCheck.position, Vector3.down, out var hit,10f))
		{
			if (Vector3.Distance(_groundCheck.position, hit.point) < 0.15f)
				_isGrounded = true;
			else
				_isGrounded = false;
		} 
		Debug.DrawLine(transform.position + (Vector3.down * 0.5f), hit.point);
	}

	void RealisticGravity()
	{
		if (_rb.velocity.y < 0f)
			_rb.velocity += Vector3.up * (Physics.gravity.y * (fallMultiplier - 1) * Time.deltaTime);
		else if (_rb.velocity.y > 0f)
			_rb.velocity += Vector3.up * (Physics.gravity.y * (lowJumpMultipler - 1) * Time.deltaTime);
	}

	void FlipCube()
	{
		transform.Rotate(Vector3.forward * 45f);
	}
}


