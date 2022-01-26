using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float projectionSpeed;
    private Vector3 _projectionDirection;
    
    private GameObject _target;

    private void Start()
    {
        _target = GameObject.FindGameObjectWithTag("Player");
        _projectionDirection = _target.transform.position - transform.position;
    }

    private void Update() => transform.Translate(_projectionDirection * (projectionSpeed * Time.deltaTime));

    private void OnTriggerEnter(Collider other)
    {
        if(!other.CompareTag("Player")) Destroy(gameObject);
    }
}
