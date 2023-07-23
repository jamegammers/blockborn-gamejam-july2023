using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridSpawner : MonoBehaviour
{

    public int gridX = 4;
    public int gridZ = 4;
    public List<GameObject> prefabsToSpawn;
    public GameObject prefabToSpawn;
    public Vector3 gridOrigin = Vector3.zero;
    public float gridOffset = 2f;
    public bool generateOnEnable;


    void OnEnable()
    {
        if (generateOnEnable)
        {
            Generate();
        }
    }

    public void Generate()
    {
        SpawnGrid();
    }


    void SpawnGrid()
    {
        for (int x = 0; x < gridX; x++)
        {
            for (int z = 0; z < gridZ; z++)
            {
                RandomizePrefab();
                GameObject clone = Instantiate(prefabToSpawn, 
                    transform.position + gridOrigin + new Vector3(gridOffset * x, 0, gridOffset * z), transform.rotation);
                clone.transform.localPosition = new Vector3(clone.transform.localPosition.x + clone.GetComponent<BuildingsNum>().buildwidth, clone.transform.localPosition.y + Random.Range(1, 11),
                    clone.transform.localPosition.z);
                //RandomSizeBuilding(clone);
                clone.transform.SetParent(this.transform);
            }
        }
    }


    private void RandomSizeBuilding(GameObject prefab)
    {
        prefab.transform.localScale = new Vector3(prefabToSpawn.transform.localScale.x, Random.Range(1, 11), prefabToSpawn.transform.localScale.z);
    }

    private void RandomizePrefab()
    {
        prefabToSpawn = prefabsToSpawn[Random.Range(0, prefabsToSpawn.Count)];
    }
    
}