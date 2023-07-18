using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyLoop : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    private LevelPoolManager _levelPoolManager;

    //TODO: define attack pattern
    //TODO: define drop on death
    //TODO: Aufrufbare Methode die das Health updated und den Enemy zerstört wenn Health <= 0

    private void Awake()
    {
        _levelPoolManager = GameObject.Find("GameManager").GetComponent<LevelPoolManager>();
        Debug.Log(_levelPoolManager);
        HandleLevelScaling(_levelPoolManager.GetCounter());
    }

    public void SpawnEnemy(Vector3 spawnPos)
    {
        Instantiate(_enemy.enemyPrefab, spawnPos, Quaternion.identity);
    }
    
    private void HandleLevelScaling(int level)
    {
        //every 5 levels, increase health and damage by 10%
        if (level % 5 == 0)
        {
            _enemy.health *= 1.1f;
            _enemy.damage *= 1.1f;
        }
    }
}
