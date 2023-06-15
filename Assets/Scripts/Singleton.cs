using System;
using UnityEngine;

//Code von Michi

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour, IShouldForceAwake
{
    protected bool isAwake;
    public static T Instance { get; private set; }
    protected virtual void Awake()
    {
        if (isAwake)
        {
            Instance = this as T;
        }
    }

    public void ForceAwake()
    {
        Awake();
        isAwake = true;
    }
}