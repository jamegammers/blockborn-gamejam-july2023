using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateCity : MonoBehaviour
{
    public static GenerateCity city;
    public List<GameObject> buildings = new List<GameObject>();

    public PerlinGenerator perlinGenerator;
    public GridSpawner gridSpawner;

    public bool generateOn = true;
    
    void Awake()
    {
        if (city == null)
        {
            city = this;
        } else if (city != this)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (generateOn)
        {
            Debug.Log("Generate new Houses");
            Generate();
        }
        else
        {
            ClearObjects();
        }
    }

    public void AddObject(GameObject objToAdd)
    {
        buildings.Add(objToAdd);
    }
    
    private void Generate()
    {
        ClearObjects();
        perlinGenerator.Generate();
        gridSpawner.Generate();
    }
    
    private void ClearObjects()
    {
        foreach (GameObject obj in buildings)
        {
            Destroy(obj);
        }
        buildings.Clear();
    }
}
