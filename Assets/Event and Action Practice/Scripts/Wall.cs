using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
            
        Events.eventManager.InvokeHitWithWall();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        Events.eventManager.InvokeBounceFromWall();
    }
    
}
