using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramerateSet : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 144;
    }
}
