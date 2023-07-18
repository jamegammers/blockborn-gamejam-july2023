using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CalculateBoundingBox : MonoBehaviour
{
    private Transform parentTransform;
    [SerializeField] GameObject _endTile;
    
    private void Awake()
    {
        // Find GameManager in Hierarchy
        
        
        parentTransform = transform.parent;
        
        Renderer[] rr = parentTransform.GetComponentsInChildren<Renderer>();
        Bounds b = rr[0].bounds;
        foreach (Renderer r in rr)
        {
            b.Encapsulate(r.bounds); 
            
        }
        
        //Debug.Log(b.size);
        
        _endTile.GetComponent<SpawnNextTile>().SetLevelLength((int) b.size.x);
    }
    
}
