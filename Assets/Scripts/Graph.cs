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
		points = new Transform[resolution * resolution];
		float step = 2f / resolution;
		var scale = Vector3.one * step;
		
		for (int i = 0,x = 0, z = 0; i < points.Length; i++, x++)
		{
			Transform pointTranform = Instantiate(pointPrefab);
			pointTranform.localScale = scale;
			pointTranform.SetParent(transform,false);
			points[i] = pointTranform;
		}
	}
	
	void Update()
	{
		float step = 2f / resolution;
		float v = 0.5f * step - 1f;
		
		for (int i = 0,x = 0,z = 0; i < points.Length; i++, x++)
		{
			if (x == resolution)
			{
				x = 0;
				z += 1;
				v = (z + 0.5f) * step - 1f;
			}
			
			float u = (x + 0.5f) * step - 1f;
			
			points[i].localPosition = FunctionLibrary.GetFunction(function)(u,v, Time.time);
		}
	}
}
