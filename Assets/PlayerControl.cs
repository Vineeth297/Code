using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
	[Range(1,10)]
	[SerializeField] private float _speed = 1f;

	[SerializeField] private float _jumpForce = 2f;
	[SerializeField] private Transform _groundCheck;

	private Rigidbody _rb;
	[SerializeField]private bool _isGrounded;

	private float _velocity;
	private float _gravity = -9.81f;
	private float _gravityScale = 5;
	private float _distanceToCheck = 0.05f;
	private void Start()
	{
		_rb = GetComponent<Rigidbody>();
	}

	void Update()
	{
		_velocity += _gravity * _gravityScale * Time.deltaTime;
		
		IsPlayerOnGround();
		
		if (Input.GetKey("right"))
		{
			transform.Translate(Vector3.right * (_speed * Time.deltaTime));
		}
		
		if (Input.GetKey("left"))
		{
			transform.Translate(Vector3.left * (_speed * Time.deltaTime));
		}

		if (_isGrounded && _velocity < 0)
		{
			_velocity = 0;
		}

		if (Input.GetKeyDown(KeyCode.Space))
		{
			_velocity = _jumpForce;
		}

		transform.Translate(new Vector3(0, _velocity, 0) * Time.deltaTime);
	}

	void IsPlayerOnGround()
	{
		if (Physics2D.Raycast(transform.position, Vector2.down, _distanceToCheck))
		{
			_isGrounded = true;
		}
		else
		{
			_isGrounded = false;
		}
	}
	
}
