using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Graph : MonoBehaviour
{
	[SerializeField] private Transform pointPrefab;
	[Range(10,100)]
	[SerializeField] private int resolution = 10;
	
	[SerializeField] FunctionLibrary.FunctionEnum function;

	private Transform[] points;
	
	
	void Awake()
	{
		points = new Transform[resolution];
		float step = 2f / resolution;
		var scale = Vector3.one * step;
		Vector3 position = Vector3.zero;
		for (int i = 0; i < points.Length; i++)
		{
			Transform pointTranform = points[i] = Instantiate(pointPrefab);
			pointTranform.localScale = scale;
			position.x = (i + 0.5f) * step - 1f;
			position.y = position.x * position.x ;
			pointTranform.localPosition = position;
			pointTranform.SetParent(transform,false);
		}
	}
	
	void Update()
	{
		
		for (int i = 0; i < points.Length; i++)
		{
			Transform point = points[i];
			Vector3 position = point.localPosition;
			//position.y = f(position.x, Time.time);
			position.y = FunctionLibrary.GetFunction(function)
				(position.x, Time.time);
			point.localPosition = position;
		}
		
	}
	
}
