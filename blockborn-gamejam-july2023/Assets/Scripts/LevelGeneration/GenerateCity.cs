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
    
    void Awake()
    {
        lastBuilding = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        
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

    private void GetLastBuilding()
    {
        if (buildings.Count > 0)
        {
            GameObject lastBuildingobj = buildings[buildings.Count - 1];
            lastBuilding = new Vector3(lastBuildingobj.transform.position.x, 0, 0);
            gridSpawner.SetNewOrigin(lastBuilding);
        }
    }
    
    private void Generate()
    {
        ClearObjects();
        perlinGenerator.Generate();
        gridSpawner.Generate();
        GetLastBuilding();
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
