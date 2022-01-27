using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Events : MonoBehaviour
{
    public static Events eventManager;

    public Action hitWithWall;
    public Action bounceFromWall;

    private void Awake()
    {
        if(eventManager == null)
            eventManager = this;
        else
            Destroy(this);
    }

    public void InvokeHitWithWall() => hitWithWall?.Invoke();

    public void InvokeBounceFromWall() => bounceFromWall?.Invoke();
}
