using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Trial : MonoBehaviour
{
    [SerializeField] private GameObject stick;
    [SerializeField] private float turnSpeed;
	[SerializeField] private int angle;
	
	private Vector3 _axisToRotateAround;

	void Start()
	{
		_axisToRotateAround = Vector3.up;
	}

    void Update()
	{ 
		Rotate();	
	}

	private void Rotate()
	{
		stick.transform.Rotate(_axisToRotateAround * (turnSpeed * Time.deltaTime));
		angle = Mathf.FloorToInt(stick.transform.eulerAngles.y);
		
		if (angle == 60 || angle == 300)
			_axisToRotateAround = -_axisToRotateAround;
	}
}
