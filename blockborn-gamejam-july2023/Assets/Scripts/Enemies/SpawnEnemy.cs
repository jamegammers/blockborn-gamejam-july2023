using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnEnemy : MonoBehaviour
{
    [SerializeField] private GameObject _gameManager;
    [SerializeField] GameObject _enemyTransformParent;

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager");
        _gameManager.GetComponent<EnemyPoolManager>().SpawnEnemy(transform.position, _enemyTransformParent);
    }

}
