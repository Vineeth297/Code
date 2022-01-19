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
	[SerializeField] private float rotationSpeed;
	private Vector3 _axisToRotateAround;

	public bool once;
	
	private void Start()
	{
	}

	void Update()
	{
		FieldOFView();
		
		print(fieldOfView.transform.eulerAngles.z);
		
		if (transform.position.x != patrolPoints[currentPatrolIndex].transform.position.x)
		{
			transform.position = Vector3.MoveTowards(transform.position,
				patrolPoints[currentPatrolIndex].position, speed * Time.deltaTime);
			
		//	fieldOfView.transform.Rotate(_axisToRotateAround * (Time.deltaTime * rotationSpeed));
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

		if (currentPatrolIndex == 0)
		{
			fieldOfView.transform.Rotate(Vector3.back * (Time.deltaTime * rotationSpeed));
		}

		if (currentPatrolIndex == 1)
		{
			fieldOfView.transform.Rotate(Vector3.forward * (Time.deltaTime * rotationSpeed));
		}
		// if (currentPatrolIndex == 0)
		// {
		// 	fieldOfView.transform.rotation = Quaternion.Euler(fieldOfView.transform.rotation.x,fieldOfView.transform.rotation.y,fieldOfView.transform.rotation.z + angle * rotationSpeed);
		// 	angle += Time.deltaTime;
		// 	print("Plus "+ (fieldOfView.transform.rotation.z + angle));
		//
		// }
		// else
		// {
		// 	fieldOfView.transform.rotation = Quaternion.Euler(fieldOfView.transform.rotation.x,fieldOfView.transform.rotation.y,fieldOfView.transform.rotation.z - angle * rotationSpeed);
		// 	angle -= Time.deltaTime;
		// 	print("Minus " + (fieldOfView.transform.rotation.z - angle));
		// }
	}
}


