using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour
{

    private Transform _enemySpawnPointsTransform;
    private Transform[] _enemySpawnPoints;
    
    void Awake()
    {
        _enemySpawnPointsTransform = transform.Find("EnemySpawnPoints");

        foreach (Transform child in _enemySpawnPointsTransform)
        {
            
        }
    }
}
