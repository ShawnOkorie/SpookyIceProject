using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class GraphAsset : ScriptableObject
{
    public GraphRenderer.GraphType graphType;
    
    public float baseAmplitude;
    public float baseFrequency;
    public float baseMovementSpeed;
}
