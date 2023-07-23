using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PowerUpManager : MonoBehaviour
{
    [SerializeField] private Randomize _randomize;
    
    [Space]
    [SerializeField] private GameObject[] _powerUpList;

    public void InstanciatePowerUp(Vector3 spawnPos, GameObject powerUpSpawnLocation)
    {
        // pick a random power up from the list
        Instantiate(_powerUpList[_randomize.RandomizeNumbers(0, _powerUpList.Length)], 
            spawnPos, Quaternion.identity).transform.parent = powerUpSpawnLocation.transform;
    }
    
}
