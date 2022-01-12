using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotationControl : MonoBehaviour
{
    private PlayerControl _playerControl;

    [SerializeField] private float _distanceToCheck = 0.001f;
    [SerializeField] private float _offset = 0.1f;
    
    void Start()
    {
        _playerControl = PlayerControl._playerControl;
       // print(_playerControl);
    }
    void Update()
    {
        var rotation = transform.rotation;
        if(!_playerControl.isGrounded)
        {
            print(_playerControl._rb.velocity);
            if(_playerControl._rb.velocity.x > 0f)
                transform.Rotate(rotation.x, rotation.y, -15f, Space.World);
            else
                transform.Rotate(rotation.x, rotation.y, 15f, Space.World);

        }
            
        
        //PerfectLanding();
    }
    
    void PerfectLanding()
    {
        Ray ray = new Ray(transform.root.position + (Vector3.down * 0.5f), Vector3.down);
        if(_playerControl.isGrounded && _playerControl._rb.velocity.y < 0f)
        {
            if (Physics.Raycast(ray, out var hit, _distanceToCheck))
            {
                
                Vector3 closestPoint = hit.collider.ClosestPoint(transform.position);
                print("CLosest " + closestPoint);
                Vector3 snappingPosition = new Vector3(transform.position.x,closestPoint.y + _offset,transform.position.z);
                print("snapped to " + snappingPosition);
                transform.position = snappingPosition;
                Debug.DrawLine(transform.position + (Vector3.down * 0.5f), hit.point);
            }
        }
    }
}
