using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _levelTiles;

    private int level = 1;
    
    [HideInInspector] public float _globalEnemyHealth = 3;

    private Transform _levelHolder;

    private float currentLevelLength;
    
    private void Awake()
    {
        
    }

    private void Start()
    {
        _levelHolder = GameObject.Find("Level").transform;
    }

    // Spawns a tile at the end of the current last tile
    public void GenerateLevel(float levelLength)
    {
        currentLevelLength += levelLength;
        
        //spawn a tile, use level to determine position
        Vector3 position = new Vector3(currentLevelLength, 0, 0);
        
        //Vector3 position = gameObject.transform.position;
        
        // Instaniate a random tile from the array of tiles using the RandomizeNumbers method from the Randomize class
        Instantiate(_levelTiles[GetComponent<Randomize>().RandomizeNumbers(0, _levelTiles.Length)], position, Quaternion.identity).transform.parent = _levelHolder;

        if (level % 5 == 0)
        {
            _globalEnemyHealth += 1;
        }
        
        level++;
    }

    public int GetCurrentLevel()
    {
        return level;
    }
}
