using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Trial : MonoBehaviour
{
    [SerializeField] private GameObject stick;

    Vector3 startAngles = new Vector3(0, 45, 0);
	Vector3 endAngles = new Vector3(0, 90, 0);
 
	Quaternion startRot, endRot;
	
	[Range(0,1)]
	[SerializeField] float lerpValue = 0;
	
	public float speed = 0.5f;
	public bool atLerpMax;
	
	void Start()
	{

		startRot = Quaternion.Euler(startAngles);
		endRot = Quaternion.Euler(endAngles);
	}

    void Update()
	{ 
		Rotate();	
	}

	private void Rotate()
	{
		if (lerpValue < 1 && !atLerpMax)
		{
			lerpValue += Time.deltaTime * speed;
			
			if (lerpValue > 1f) atLerpMax = true;
		}

		if (atLerpMax)
		{
			lerpValue -= Time.deltaTime * speed;
			
			if (lerpValue < 0f) atLerpMax = false;
		}
		
		stick.transform.rotation = Quaternion.Lerp(startRot, endRot, lerpValue);
		
		// stick.transform.Rotate(_axisToRotateAround * (turnSpeed * Time.deltaTime));
		// angle = Mathf.FloorToInt(stick.transform.eulerAngles.y);
		//
		// if (angle == 60 || angle == 300)
		// 	_axisToRotateAround = -_axisToRotateAround;
		
		
	}
}
