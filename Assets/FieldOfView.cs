using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
	private Transform _parent;
	private Vector3 _locatedPlayerPosition;
	private void Start()
	{
		_parent = transform.root;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			//print("Found Player");
			_locatedPlayerPosition = other.transform.position;
			//print(_locatedPlayerPosition);
			Patroller.patroller.playerLocated = true;
		}
	}
	
}
