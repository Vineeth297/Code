using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Trial : MonoBehaviour
{
    [SerializeField] private GameObject stick;
    [SerializeField] private float angle;

    void Update()
    {
        stick.transform.Rotate(new Vector3(stick.transform.rotation.x, stick.transform.rotation.y + angle * Time.deltaTime,stick.transform.rotation.z));
    }
}
