using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CanvasController : MonoBehaviour
{
    [SerializeField] private TMP_Text text; 
    
    private void Start()
    {
        
        Events.eventManager.hitWithWall += OnHitColorChange;
        Events.eventManager.bounceFromWall += OnBounceBackColorChange;
        
    }

    private void OnDisable()
    {
        Events.eventManager.hitWithWall -= OnHitColorChange;
        Events.eventManager.bounceFromWall -= OnBounceBackColorChange;
    }

    private void OnHitColorChange()
    {
        text.text = "Blue";
        text.color = Color.blue;
    }

    private void OnBounceBackColorChange()
    {
        text.text = "Yellow";
        text.color = Color.yellow;
    }
}
