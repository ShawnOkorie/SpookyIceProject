using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DialogSystem;
using Minigames;
using UnityEngine;

public class GameStateManager : Singleton<GameStateManager>
{
    public delegate void SetUninteractible();
    public event SetUninteractible OnSetUninteractible;
    
    public delegate void SetInteractible();
    public event SetInteractible OnSetInteractible;
    
    private List<MiniGameTrigger> triggerList = new List<MiniGameTrigger>();

    protected override void Awake()
    {
        base.Awake();
        triggerList = FindObjectsOfType<MiniGameTrigger>().ToList();
    }
    
    private void OnEnable()
    {
        foreach (MiniGameTrigger trigger in triggerList)
        {
            trigger.OnMinigameStart += InvokeUninteractible;
            trigger.OnMinigameEnd += InvokeInteractible;
        }
    }

    private void Start()
    {
        LoadingScreen.Instance.OnFadeStart += InvokeUninteractible;
        LoadingScreen.Instance.OnFadeEnd += InvokeInteractible;
        
        DialogManager.Instance.OnDialogStart += InvokeUninteractible;
        DialogManager.Instance.OnDialogEnd += InvokeUninteractible;
    }

    private void InvokeUninteractible()
    {
        OnSetUninteractible?.Invoke();
    }
    
    private void InvokeInteractible()
    {
        OnSetInteractible?.Invoke();
    }
}
