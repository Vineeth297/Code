using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Events;

public class Cube : MonoBehaviour
{
    private void OnEnable()
    {
        eventManager.hitWithWall += OnHitWithWall;
        eventManager.bounceFromWall += OnBounceFromWall;
    }

    private void OnDisable()
    {
        eventManager.hitWithWall -= OnHitWithWall;
        eventManager.bounceFromWall -= OnBounceFromWall;
    }

    private void OnHitWithWall()
    {
        GetComponent<MeshRenderer>().material.color = Color.blue;
    }

    private void OnBounceFromWall()
    {
        GetComponent<MeshRenderer>().material.color = Color.yellow;
    }
    
}
