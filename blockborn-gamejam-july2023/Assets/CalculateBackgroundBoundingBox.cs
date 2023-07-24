using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalculateBackgroundBoundingBox : MonoBehaviour
{
    private Transform parentTransform;

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
        
            Debug.Log(b.size);
        }
    
}
