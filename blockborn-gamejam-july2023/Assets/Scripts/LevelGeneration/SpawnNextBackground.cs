using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNextBackground : MonoBehaviour
{
    private GenerateCity _generateCity;
    
    private void Awake()
    {
        _generateCity = GameObject.Find("Generator").GetComponent<GenerateCity>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _generateCity.Generate();
    }
}
