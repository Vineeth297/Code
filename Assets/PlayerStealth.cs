using UnityEngine;
using UnityEngine.Serialization;
using static UnityEngine.Mathf;

public class PlayerStealth : MonoBehaviour
{
	[FormerlySerializedAs("speed")] [SerializeField] private float moveSpeed;
	[SerializeField] private float smoothMoveTime;
	[SerializeField] private float turnSpeed;

	private float _angle;
	private float _moveDirectionSmooth;
	private float _smoothMoveVelocity;
	private Vector3 _velocity;
	private Rigidbody _rb;

	private bool _disabled;
	
	private void Start()
	{
		_rb = GetComponent<Rigidbody>();

		GuardBox.OnGuardSpottedThePlayer += OnDisable;
	}
	private void Update()
    {
		var moveDirection = Vector3.zero;
		if(!_disabled)
			moveDirection = new Vector3(Input.GetAxisRaw("Horizontal"),0f,Input.GetAxisRaw("Vertical")).normalized;
		
		var moveDirectionMagnitude = moveDirection.magnitude;
		_moveDirectionSmooth = SmoothDamp(_moveDirectionSmooth, moveDirectionMagnitude, ref _smoothMoveVelocity, smoothMoveTime);

		var targetAngle = Atan2(moveDirection.x, moveDirection.z) * Rad2Deg;
		_angle = LerpAngle(_angle, targetAngle, turnSpeed * moveDirectionMagnitude * Time.deltaTime);

	#region Movement without Rigidbody
		// transform.eulerAngles = Vector3.up * _angle;
		//
		// transform.Translate(transform.forward * (moveSpeed * _moveDirectionSmooth * Time.deltaTime),Space.World );
	#endregion

		_velocity = transform.forward * (_moveDirectionSmooth * moveSpeed);
	}

	private void FixedUpdate()
	{
		_rb.MoveRotation(Quaternion.Euler(Vector3.up * _angle));
		_rb.MovePosition(_rb.position + _velocity * Time.deltaTime);
	}

	private void OnDisable()
	{
		print("call horay");
		_disabled = true;
	}

	private void OnDestroy()
	{
		GuardBox.OnGuardSpottedThePlayer -= OnDisable;
	}
}
