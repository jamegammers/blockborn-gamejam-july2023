using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnemyPoolManager : MonoBehaviour
{
    [SerializeField] private Randomize _randomize;
    [SerializeField] private EnemyPoolManager _enemyPoolManager;
    
    [SerializeField] public GameObject[] enemyPrefabs;
    
    public void SpawnEnemy(Vector3 spawnPos, GameObject enemySpawnLocationTransform)
    {
        Instantiate(enemyPrefabs[_randomize.RandomizeNumbers(0, _enemyPoolManager.enemyPrefabs.Length)], spawnPos, Quaternion.identity).transform.parent = enemySpawnLocationTransform.transform;
    }
}
