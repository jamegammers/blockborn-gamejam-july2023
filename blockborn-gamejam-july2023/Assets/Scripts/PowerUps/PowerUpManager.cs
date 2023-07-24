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

    [SerializeField] private int _spawnChance = 11;

    public void InstanciatePowerUp(Vector3 spawnPos, GameObject powerUpSpawnLocation)
    {
        
        //Debug.Log(_randomize.RandomizeNumbers(0, 0));
        
        // if randomize number is 0, spawn a power up
        if (_randomize.RandomizeNumbers(0, _spawnChance) == 0)
        {
            // pick a random power up from the list
            Instantiate(_powerUpList[_randomize.RandomizeNumbers(0, _powerUpList.Length)], 
                spawnPos, Quaternion.identity).transform.parent = powerUpSpawnLocation.transform;
            
            _spawnChance = 11;
        }
        // else, increase spawn chance
        else
        {
            if (_spawnChance > 0)
            {
                _spawnChance--;
            }
        }
    }
}
