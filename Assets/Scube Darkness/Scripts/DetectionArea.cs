using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DetectionArea : MonoBehaviour
{
	[SerializeField] private GameObject stick;

	[SerializeField] private bool foundPlayer;

	private Vector3 _playerPos;

	private void Start()
	{
		_playerPos = GameObject.FindGameObjectWithTag("Player").transform.position;
	}
	
	private void Update()
	{
		if (!foundPlayer) return;
		var playerPosition = _playerPos;
		stick.transform.RotateAround(stick.transform.position,Vector3.up,10 * Time.deltaTime);
	}
	
	private void OnTriggerEnter(Collider other)
	{
		if (!other.CompareTag("Player")) return;

		foundPlayer = true;
		print("1");
	}
	
	private void OnTriggerExit(Collider other)
	{
		if (!other.CompareTag("Player")) return;

		foundPlayer = false;
		print("1");
	}
}
