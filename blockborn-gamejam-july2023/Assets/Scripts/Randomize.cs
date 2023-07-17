using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Randomize : MonoBehaviour
{
    public int levelSeed = 12345;
    
    void Start()
    {
        Random.InitState(levelSeed);
    }
    
    public int RandomizeNumbers(int min, int max)
    {
        return Random.Range(min, max);
    }
}
