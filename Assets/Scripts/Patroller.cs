using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Patroller : MonoBehaviour
{
	public static Patroller patroller;
	
	[Header("Movement")]
	[SerializeField] private List<Transform> patrolPoints;
	[SerializeField] private int currentPatrolIndex;
	
	[Range(1, 30)]
	[SerializeField] private float speed;

	public bool once;

	[Header("Rotation")]
	[SerializeField] private GameObject fieldOfView;
	[SerializeField] private Vector3 minAngle = new Vector3(0f,45f,0f);
	[SerializeField] private Vector3 maxAngle = new Vector3(0f,90f,0f);
	[SerializeField] private float rotationSpeed;
	[SerializeField] private bool reachedMaxLerp;
	
	private float _lerpTime = 0f;
	private Quaternion _startRotation;
	private Quaternion _endRotation;

	public bool playerLocated;

	[SerializeField] private float followSpeed;
	private Transform _player;
	private Transform _locatedPlayer;

	[SerializeField] private float fovTrackingSpeed = 0.5f;

	[Header("Attack Attributes")] 
	[SerializeField] private GameObject projectile;
	[SerializeField] private float projectileSpeed;
	
	
	private void Awake()
	{
		patroller = this;
	}
	
	private void Start()
	{
		_player = GameObject.FindGameObjectWithTag("Player").transform;
		_startRotation = quaternion.Euler(minAngle);
		_endRotation = quaternion.Euler(maxAngle);
	}

	private void Update()
	{
		if (!playerLocated)
		{
			FieldOfView();
			
			if (transform.position.x != patrolPoints[currentPatrolIndex].transform.position.x)
			{
				transform.position = Vector3.MoveTowards(transform.position,
					patrolPoints[currentPatrolIndex].position, speed * Time.deltaTime);
			}
			else if (!once)
			{
				once = true;
				StartCoroutine(Patrol());
			}
		}
		else
			ChasePlayer();
	}
	
	private IEnumerator Patrol()
	{
		yield return new WaitForSeconds(0.1f);
		if (currentPatrolIndex + 1 < patrolPoints.Count)
			currentPatrolIndex++;
		else
			currentPatrolIndex = 0;
		once = false;
	}

	private void FieldOfView()
	{
		if (_lerpTime < 1f && !reachedMaxLerp)
		{
			_lerpTime += Time.deltaTime * rotationSpeed;
			if (_lerpTime > 1f) reachedMaxLerp = true;
		}

		if (reachedMaxLerp)
		{
			_lerpTime -= Time.deltaTime * rotationSpeed;
			if (_lerpTime < 0f) reachedMaxLerp = false;
		}
		
		fieldOfView.transform.rotation = Quaternion.Lerp(_startRotation,_endRotation,_lerpTime);
	}

	private void ChasePlayer()
	{
		//Locate Player
		_locatedPlayer = _player.gameObject.transform;
		
		//Chase Located Player
		var patrollerPosition = transform.position;
		
		patrollerPosition = Vector3.MoveTowards(patrollerPosition, new Vector3(_locatedPlayer.position.x,
			patrollerPosition.y, 0f), followSpeed * Time.deltaTime);
		transform.position = patrollerPosition;

		Vector3 relativePos =  _locatedPlayer.position - fieldOfView.transform.position;
		
		//Rotate in direction of Located Player
		Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.forward);
		fieldOfView.transform.rotation =
			Quaternion.Slerp(fieldOfView.transform.rotation, new quaternion(0f, 0f, rotation.z,1),fovTrackingSpeed * Time.deltaTime);
		
		AttackPlayer();
	}

	private void AttackPlayer()
	{
		//Attack Player only if the player stays in Enemy's Field of View
		var playerDirection = _locatedPlayer.position - fieldOfView.transform.position;
		var ray = new Ray(fieldOfView.transform.position, playerDirection);
		
		if (!Physics.Raycast(ray, out var hit, 20f)) return;

		StartCoroutine(routine: HitPlayer(hit.point));
		
		Debug.DrawLine(fieldOfView.transform.position,hit.point);
	}

	private IEnumerator HitPlayer(Vector3 playerDirection)
	{
		var attackDirection = _locatedPlayer.transform.position - fieldOfView.transform.position;
		
		var bullet = Instantiate(projectile);
		bullet.transform.position = fieldOfView.transform.position;
		bullet.GetComponent<Rigidbody>().AddForce(attackDirection * projectileSpeed * Time.deltaTime,ForceMode.Impulse);

		yield return new WaitForSeconds(2.5f);
	}
}


