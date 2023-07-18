using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnNextTile : MonoBehaviour
{
    private GameObject _gameManager;
    private int levelLength;

    private void Awake()
    {
        _gameManager = GameObject.Find("GameManager");
    }

    // due to collision matrix, only player can trigger this
    private void OnTriggerEnter(Collider other)
    {
        _gameManager.GetComponent<LevelPoolManager>().GenerateLevel(levelLength);
        
        //remove Box Collider to prevent spawning multiple tiles
        Destroy(GetComponent<BoxCollider>());
    }
    
    public void SetLevelLength(int length)
    {
        levelLength = length;
    }
    
    
    //TODO: spawn enemies using gameManager
    
}
