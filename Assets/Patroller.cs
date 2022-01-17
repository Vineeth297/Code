using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class Patroller : MonoBehaviour
{
	[SerializeField] private GameObject fieldOfView;

	[SerializeField] private List<Transform> patrolPoints;
	[SerializeField] private int currentPatrolIndex;
	
	[Range(-60, 60)]
	[SerializeField] private float angle,speed;

	public bool once;
	
	private void Start()
	{
		
	}

	void Update()
	{
		FieldOFView();
		if (transform.position.x != patrolPoints[currentPatrolIndex].transform.position.x)
		{
			transform.position = Vector3.MoveTowards(transform.position,
				patrolPoints[currentPatrolIndex].position, speed * Time.deltaTime);
			
		}
		else if(!once)
		{
			once = true;
			StartCoroutine(Patrol());
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

	private void FieldOFView()
	{
		
		
		
		fieldOfView.transform.rotation = Quaternion.Euler(new Vector3(Mathf.PingPong(Time.deltaTime * angle , 90) - 45,fieldOfView.transform.rotation.y + 90,fieldOfView.transform.rotation.z + 90));
		
	}
}


