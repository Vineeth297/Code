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
			
			//fieldOfView.transform.rotation = Quaternion.Euler(fieldOfView.transform.rotation.x,fieldOfView.transform.rotation.y, 1f);
			
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
			fieldOfView.transform.rotation = Quaternion.Euler(fieldOfView.transform.rotation.x,fieldOfView.transform.rotation.y,fieldOfView.transform.rotation.z + angle * rotationSpeed);
			//fieldOfView.transform.Rotate(new Vector3(fieldOfView.transform.rotation.x, fieldOfView.transform.rotation.y ,fieldOfView.transform.rotation.z - angle * Time.deltaTime));
			angle += Time.deltaTime;
			print("Plus "+ (fieldOfView.transform.rotation.z + angle));

		}
		else
		{
			//fieldOfView.transform.Rotate(new Vector3(fieldOfView.transform.rotation.x, fieldOfView.transform.rotation.y ,fieldOfView.transform.rotation.z +  angle * Time.deltaTime));
			fieldOfView.transform.rotation = Quaternion.Euler(fieldOfView.transform.rotation.x,fieldOfView.transform.rotation.y,fieldOfView.transform.rotation.z - angle * rotationSpeed);
			angle -= Time.deltaTime;
			print("Minus " + (fieldOfView.transform.rotation.z - angle));
		}

	}
}


