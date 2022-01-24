using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrigMath : MonoBehaviour
{
	[SerializeField] private float angle;

    // Update is called once per frame
    void Update()
    {
        var direction = new Vector3(Mathf.Sin(angle * Mathf.Deg2Rad),0f,Mathf.Cos(angle * Mathf.Deg2Rad));
		Debug.DrawLine(transform.position,direction * 5,Color.yellow);
    }
}
