using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerRotationControl : MonoBehaviour
{
    private PlayerControl _playerControl;

    [SerializeField] private float distanceToCheck = 0.001f;
    [SerializeField] private float offset = 0.1f;
	[SerializeField] private float angle = 45f;
	
	public void Start()
    {
	    _playerControl = PlayerControl.playerControl;
    }

    public void Update()
	{
		var rotation = transform.rotation;
        if(!_playerControl.isGrounded)
		{
			if(_playerControl.horizontalAxis > 0f)
				transform.Rotate(rotation.x,rotation.y, -angle * Time.deltaTime);
			if(_playerControl.horizontalAxis < 0f)
				transform.Rotate(rotation.x,rotation.y, angle * Time.deltaTime);
		}
		
		PerfectLanding();
    }

    private void PerfectLanding()
    {
        Ray ray = new Ray(transform.root.position + (Vector3.down * 0.5f), Vector3.down);
        if(_playerControl.isGrounded && _playerControl.rb.velocity.y < 0f)
        {
            if (Physics.Raycast(ray, out var hit, distanceToCheck))
            {
				Vector3 closestPoint = hit.collider.ClosestPoint(transform.position);
               
                Vector3 snappingPosition = new Vector3(hit.point.x,closestPoint.y + offset,hit.point.z);
                transform.position = Vector3.Lerp(closestPoint, snappingPosition + (Vector3.up * 0.5f), 0.5f);
				
                Debug.DrawLine(transform.position + (Vector3.down * 0.5f), hit.point);
            }
            transform.rotation = Quaternion.Euler(0f,0f,0f);
        }
    }
}
