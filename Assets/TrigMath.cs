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
		Debug.DrawRay(transform.position,direction * 5,Color.yellow);
		
		var inputDirection = new Vector3(Input.GetAxisRaw("Horizontal"),0f,Input.GetAxisRaw("Vertical")).normalized;
		transform.Translate(inputDirection * (5 * Time.deltaTime),Space.World);

		var inputAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;
		transform.eulerAngles = Vector3.up * inputAngle;
	}
}
