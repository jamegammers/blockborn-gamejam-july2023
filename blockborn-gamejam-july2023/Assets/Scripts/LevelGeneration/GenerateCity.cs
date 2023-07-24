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
    public Vector3 lastBuilding;

    private int _clearObjects = 0;

    void Awake()
    {
        lastBuilding = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
        /*if (city == null)
        {
            city = this;
        } else if (city != this)
        {
            Destroy(gameObject);
        }*/
    }

    private void Update()
    {
        if (generateOn)
        {
            Debug.Log("Generate new Houses");
            Generate();
        }
    }

    public void AddObject(GameObject objToAdd)
    {
        buildings.Add(objToAdd);
    }

    private void GetLastBuilding()
    {
        if (buildings.Count > 0)
        {
            GameObject lastBuildingobj = buildings[buildings.Count - 1];
            lastBuilding = new Vector3(lastBuildingobj.transform.position.x, 0, 0);
            gridSpawner.SetNewOrigin(lastBuilding);
        }
    }
    
    public void Generate()
    {
        
        perlinGenerator.Generate();
        gridSpawner.Generate();
        GetLastBuilding();
        generateOn = false;

        if (_clearObjects > 2)
        {
            ClearObjects();
            _clearObjects = 0;
        } else _clearObjects++;
    }
    
    private void ClearObjects()
    {
        for (int i = 0; i < gridSpawner.gridX * gridSpawner.gridZ; i++)
        {
            Destroy(buildings[i]);
        }
    }
}
