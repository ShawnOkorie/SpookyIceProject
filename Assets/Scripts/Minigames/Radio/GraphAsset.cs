using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class GraphAsset : ScriptableObject
{
    public GraphType graphType;
    public enum GraphType
    {
        Sin,
        Cos,
        Tan
    }
    
    public float baseAmplitude;
    public float baseFrequency;
    public float baseMovementSpeed;
    
}
