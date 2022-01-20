using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldOfView : MonoBehaviour
{
	private Transform _parent;
	private void Start()
	{
		_parent = transform.root;
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.CompareTag("Player"))
		{
			print("Found Player");
		}
	}

	private IEnumerator ChasePlayer()
	{
		//Locate Player
		
		//Attack Player
		//Chase Player
		yield return null;
	}
}
