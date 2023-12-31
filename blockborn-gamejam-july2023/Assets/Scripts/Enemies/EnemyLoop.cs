using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

//TODO: define drop on death

public class EnemyLoop : MonoBehaviour
{
    private GameObject _gameManager;
    private LevelPoolManager _levelPoolManager;
    private Randomize _randomize;
    private EnemyPoolManager _enemyPoolManager;
    
    [SerializeField] private Enemy _enemy;

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
    
    
    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager");
        _randomize = _gameManager.GetComponent<Randomize>();
        
        _levelPoolManager = _gameManager.GetComponent<LevelPoolManager>();
        _enemyPoolManager = _gameManager.GetComponent<EnemyPoolManager>();
        

        _enemyHealth = _enemy.health;
        _enemyDamage = _enemy.damage;
        HandleLevelScaling(_levelPoolManager.GetCurrentLevel());
    }

    private void Update()
    {
        CheckForPlayerInRange();
    }

    private void CheckForPlayerInRange()
    {
        foreach (Vector3 direction in _shootDirections)
        {
            Ray ray = new Ray(transform.position, direction);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, _detectRange, layerMask))
            {
                if (hit.collider.transform.gameObject.layer == 6)
                {
                    //Debug.Log("Player is hit!");
                }
            }
            
            //Debug 3D ray
            //Debug.DrawRay(transform.position, direction * _detectRange, Color.red);
            
            //TODO: call function that activates pattern + attacks
        }
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

    public Enemy GetEnemy()
    {
        return _enemy;
    }
    
    private void Death()
    {
        //TODO: drop items or stuff
        //TODO: Death animation / sound?
        Destroy(gameObject);
    }
}
