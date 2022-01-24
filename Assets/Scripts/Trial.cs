using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

public class Trial : MonoBehaviour
{

    [SerializeField] private GameObject whateverYouWantToThrow;
    [SerializeField] private float startTime;

    private float _timeDiff;
    private void Update()
    {
        if (_timeDiff < 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Instantiate(whateverYouWantToThrow);
                _timeDiff = startTime;
            }
        }
        else
            _timeDiff -= Time.deltaTime;
    }
}
