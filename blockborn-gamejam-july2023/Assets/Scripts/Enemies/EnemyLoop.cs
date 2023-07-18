using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyLoop : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    private float _enemyHealth;
    private float _enemyDamage;
    
    private LevelPoolManager _levelPoolManager;
    
    //TODO: define attack pattern
    //TODO: define drop on death
    //TODO: Aufrufbare Methode die das Health updated und den Enemy zerstört wenn Health <= 0

    private void Awake()
    {
        _levelPoolManager = GameObject.Find("GameManager").GetComponent<LevelPoolManager>();

        _enemyHealth = _enemy.health;
        _enemyDamage = _enemy.damage;
        
        HandleLevelScaling(_levelPoolManager.GetCurrentLevel());
    }

    public void SpawnEnemy(Vector3 spawnPos)
    {
        Instantiate(_enemy.enemyPrefab, spawnPos, Quaternion.identity);
    }
    
    private void HandleLevelScaling(int level)
    {
        _enemyHealth = _levelPoolManager._globalEnemyHealth;
        Debug.Log(_enemyHealth);
    }
    
    public void GetHit(int damage)
    {
        _enemyHealth -= damage;
        Debug.Log(_enemyHealth);
        if (_enemyHealth <= 0) Death();
    }

    private void Death()
    {
        //TODO: drop items or stuff
        //TODO: Death animation / sound?
        Destroy(gameObject);
    }
}
