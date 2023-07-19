using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyLoop : MonoBehaviour
{
    [SerializeField] private Enemy _enemy;
    [SerializeField] private GameObject _bullet;
    
    public LayerMask layerMask;
    [SerializeField] private float _detectRange = 5f;

    private Vector3[] _shootDirections = 
    { 
        new Vector3(1, 0, 0), 
        new Vector3(1, 1, 0),     
        new Vector3(0, 1, 0), 
        new Vector3(-1, 1, 0),
        new Vector3(-1, 0, 0),
        new Vector3(-1, -1, 0),
        new Vector3(0, -1, 0),
        new Vector3(1, -1, 0)
    };

    private float _enemyHealth;
    private float _enemyDamage;
    
    private LevelPoolManager _levelPoolManager;
    
    //TODO: define attack pattern
    //TODO: define drop on death

    private void Awake()
    {
        _levelPoolManager = GameObject.Find("GameManager").GetComponent<LevelPoolManager>();

        _enemyHealth = _enemy.health;
        _enemyDamage = _enemy.damage;
        
        HandleLevelScaling(_levelPoolManager.GetCurrentLevel());
    }

    private void Update()
    {
        Attack();
    }

    private void Attack()
    {
        foreach (Vector3 direction in _shootDirections)
        {
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _detectRange, layerMask))
            {
                if (hit.collider.transform.gameObject.layer == 6)
                {
                    Debug.Log("Player is hit!");
                }
            }
            //Debug 3D ray
            Debug.DrawRay(transform.position, direction * _detectRange, Color.red);
        }
    }

    public void SpawnEnemy(Vector3 spawnPos)
    {
        Instantiate(_enemy.enemyPrefab, spawnPos, Quaternion.identity);
    }
    
    private void HandleLevelScaling(int level)
    {
        _enemyHealth = _levelPoolManager._globalEnemyHealth;
    }
    
    public void GetHit(int damage)
    {
        _enemyHealth -= damage;
        if (_enemyHealth <= 0) Death();
    }

    private void Death()
    {
        //TODO: drop items or stuff
        //TODO: Death animation / sound?
        Destroy(gameObject);
    }
}
