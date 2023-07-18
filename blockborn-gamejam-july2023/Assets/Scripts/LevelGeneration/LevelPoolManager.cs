using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _levelTiles;

    private int level = 1;
    
    private Transform _levelHolder;
    
    private void Awake()
    {
        
    }

    private void Start()
    {
        _levelHolder = GameObject.Find("LevelHolder").transform;
    }

    // Spawns a tile at the end of the current last tile
    public void GenerateLevel(int levelLength)
    {
        //spawn a tile, use level to determine position
        Vector3 position = new Vector3(level * levelLength, 0, 0);
        
        // Instaniate a random tile from the array of tiles using the RandomizeNumbers method from the Randomize class
        Instantiate(_levelTiles[GetComponent<Randomize>().RandomizeNumbers(0, _levelTiles.Length)], position, Quaternion.identity).transform.parent = _levelHolder;
        level++;
    }

    public int GetCurrentLevel()
    {
        return level;
    }
}
