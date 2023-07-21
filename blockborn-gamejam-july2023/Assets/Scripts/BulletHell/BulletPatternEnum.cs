using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "new Bulletpattern", menuName = "Bulletpattern")]
public class BulletPatternEnum : ScriptableObject
{
    public enum BulletPatternsEnum
    {
        None,
        Circle,
        Cone,
        Burst,
        Straight,
        Spiral,
        DoubleSpiral,
        Pyramid,
        WaveDecel,
        WayAllRange,
    }
}