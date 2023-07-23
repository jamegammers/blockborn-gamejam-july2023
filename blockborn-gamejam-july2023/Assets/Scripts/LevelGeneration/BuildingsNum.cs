using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsNum : MonoBehaviour
{
    [SerializeField] private int buildHeight;
    [SerializeField] private int buildWidth;
    
    public int buildheight { get { return buildHeight; } }
    public int buildwidth { get { return buildWidth; } }
}
