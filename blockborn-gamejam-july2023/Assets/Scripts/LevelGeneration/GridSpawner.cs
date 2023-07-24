using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GridSpawner : MonoBehaviour
{

    public int gridX = 4;
    public int gridZ = 4;
    public List<GameObject> prefabsToSpawn;
    public GameObject prefabToSpawn;
    public Vector3 gridOrigin;
    public float gridOffset = 2f;
    public bool generateOnEnable;

    [SerializeField] private GenerateCity _generateCity;
    
    [SerializeField] public GameObject BuildingList;
    public Vector3 lastBuilding;
    
    
    public void SetNewOrigin(Vector3 newOrigin)
    {
        lastBuilding = newOrigin;
        gridOrigin = newOrigin;
    }

    void OnEnable()
    {
        /*if (generateOnEnable)
        {
            Generate();
        }*/
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
                    transform.position + gridOrigin + new Vector3(gridOffset * x + lastBuilding.x, 0, gridOffset * z), transform.rotation);

                clone.transform.position = new Vector3(clone.transform.position.x,
                    clone.transform.position.y + Random.Range(1, 11), clone.transform.position.z + Random.Range(0f,3f));
                
                lastBuilding = new Vector3(clone.GetComponent<BuildingsNum>().buildwidth, 0, 0);
                clone.transform.SetParent(BuildingList.transform);
                _generateCity.AddObject(clone);
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