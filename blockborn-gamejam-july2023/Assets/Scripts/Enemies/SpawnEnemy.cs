using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnEnemy : MonoBehaviour
{
    private EnemyLoop _enemyLoop;
    [SerializeField] private GameObject _enemyPrefab;
    private Enemy _enemyScriptableObject;
    
    private void Awake()
    {
        _enemyLoop = _enemyPrefab.GetComponent<EnemyLoop>();
        _enemyLoop.SpawnEnemy(transform.position);
    }
}
