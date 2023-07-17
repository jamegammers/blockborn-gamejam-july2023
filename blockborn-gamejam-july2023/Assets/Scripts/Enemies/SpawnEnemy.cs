using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    private LevelPoolManager _levelPoolManager;
    [SerializeField] private Enemy _enemy;
    
    //[Header("Overrides")]


    private void Awake()
    {
        _levelPoolManager = GameObject.Find("GameManager").GetComponent<LevelPoolManager>();
        _enemy.HandleLevelScaling(_levelPoolManager.GetCounter());
        _enemy.SpawnEnemy(transform.position);
    }
}
