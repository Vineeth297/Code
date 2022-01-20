using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using Unity.Mathematics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Patroller : MonoBehaviour
{
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

	[SerializeField] private bool playerLocated;
	
	
	private void Start()
	{
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
}


