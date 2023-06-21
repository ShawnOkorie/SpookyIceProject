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
    
    public delegate void RespawnPlayer(RoomManager.Rooms currentroom);
    public event RespawnPlayer OnRespawn;
    
    [SerializeField] private List<MiniGameTrigger> triggerList = new List<MiniGameTrigger>();
    [SerializeField] private List<CloseUpTrigger> closeupList = new List<CloseUpTrigger>();

    protected override void Awake()
    {
        base.Awake();
        /*triggerList = FindObjectsOfType<MiniGameTrigger>().ToList();
        closeupList = FindObjectsOfType<CloseUpTrigger>().ToList();*/
    }
    
    private void OnEnable()
    {
        foreach (MiniGameTrigger trigger in triggerList)
        {
            trigger.OnMinigameStart += InvokeUninteractible;
            trigger.OnMinigameEnd += InvokeInteractible;
        }

        foreach (CloseUpTrigger closeUp in closeupList)
        {
            closeUp.OnCloseUpOpen += InvokeUninteractible;
            closeUp.OnCloseUpExit += InvokeInteractible;
        }
    }

    private void Start()
    {
        LoadingScreen.Instance.OnFadeStart += InvokeUninteractible;
        LoadingScreen.Instance.OnFadeEnd += InvokeInteractible;
        
        DialogManager.Instance.OnDialogStart += InvokeUninteractible;
        DialogManager.Instance.OnDialogEnd += InvokeInteractible;

        CutSceneCanvas.Instance.OnCutsceneStart += InvokeUninteractible;
        CutSceneCanvas.Instance.OnCutsceneEnd += InvokeInteractible;
    }

    private void InvokeUninteractible()
    {
        OnSetUninteractible?.Invoke();
    }
    
    private void InvokeInteractible()
    {
        if (CutSceneCanvas.Instance.myCanvas.gameObject.activeSelf)
            return;
        
        OnSetInteractible?.Invoke();
    }

    public void Respawn()
    {
        GameManager.Instance.Load();
        OnRespawn?.Invoke(RoomManager.Instance.currentroom);
    }
}
