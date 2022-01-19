using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCamouflage : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        print("Entered");
        if (other.CompareTag("Player"))
        {
            print("Compared");
            other.tag = "Hidden";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        print("Left");
        if (other.CompareTag("Hidden"))
        {
            print("Changed to player");
            other.tag = "Player";
        }
    }
}
