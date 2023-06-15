using System;
using UnityEngine;

//Code von Michi

public class Singleton<T> : MonoBehaviour, IShouldForceAwake where T : MonoBehaviour
{
    protected bool isAwake;
    public static T Instance { get; private set; }
    protected virtual void Awake()
    {
        if (isAwake)
        {
            Instance = this as T;
        }
        
        isAwake = true;
    }

    public void ForceAwake()
    {
        Awake();
    }
}