using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class LevelPoolManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _levelTiles;
    private NewInputActionMap _inputActionMap;
    
    private int counter = 1;
    
    private Transform _levelHolder;
    
    private void Awake()
    {
        //Just tesing, delete later
        /*
        _inputActionMap = new NewInputActionMap();
        _inputActionMap.PlayerTesting.Enable();
        _inputActionMap.PlayerTesting.Test.performed += context =>  GenerateLevel();
        */
    }

    private void Start()
    {
        _levelHolder = GameObject.Find("LevelHolder").transform;
    }

    // Spawns a tile at the end of the current last tile
    public void GenerateLevel()
    {
        //spawn a tile, use counter to determine position
        Vector3 position = new Vector3(counter * 25, 0, 0);
        
        // Instaniate a random tile from the array of tiles using the RandomizeNumbers method from the Randomize class
        Instantiate(_levelTiles[GetComponent<Randomize>().RandomizeNumbers(0, _levelTiles.Length)], position, Quaternion.identity).transform.parent = _levelHolder;
        counter++;
    }

    public int GetCounter()
    {
        return counter;
    }
}
