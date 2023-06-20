using System;
using System.Collections;
using System.Collections.Generic;
using DialogSystem;
using Minigames;
using UnityEngine;

public class MiniGameTrigger : MonoBehaviour
{
    public delegate void MinigameStart();
    public event MinigameStart OnMinigameStart;
    
    public delegate void MinigameEnd();
    public event MinigameEnd OnMinigameEnd;
    
    public ProgressManager.Progress minigameProgress;

    [SerializeField] private int difficulty;
    [SerializeField] private int timeLimit;

    [SerializeField] private Minigame myMinigame;
    [SerializeField] private bool destroyOnSolve;
    [SerializeField] private int solve_PID;

    private enum Minigame
    {
        None,
        Skillcheck,
        Radio,
        Keypad,
        Switches
    }
    
    private void Start()
    {
        SkillCheck.Instance.OnMinigameEnd += EndMiniGame;
        RadioMinigame.Instance.OnMinigameEnd += EndMiniGame;
        Keypad.Instance.OnMinigameEnd += EndMiniGame;
        Switches.Instance.OnMinigameEnd += EndMiniGame;
    }

    public void StartMiniGame()
    {
        OnMinigameStart?.Invoke();
        switch (myMinigame)
        {
            case Minigame.None:
                return;
            
            case Minigame.Skillcheck:
                SkillCheck.Instance.StartMinigame(difficulty,timeLimit);
                break;
          
            case Minigame.Radio:
                RadioMinigame.Instance.StartMinigame(difficulty,timeLimit);
                break;
           
            case Minigame.Keypad:
                Keypad.Instance.StartMinigame(difficulty,timeLimit);
                break;
            
            case Minigame.Switches:
                Switches.Instance.StartMinigame(difficulty,timeLimit);
                break;
        }
    }

    public void EndMiniGame(bool solved)
    {
        switch (solved)
        {
            case true:
                ProgressManager.Instance.AddProgress(minigameProgress);
                ExitCanvas();
                if (Application.isPlaying)
                {
                    GameManager.Instance.Save();
                }
                OnMinigameEnd?.Invoke();

                if (solve_PID > 0)
                {
                    DialogManager.Instance.StartDialog(solve_PID);
                }
                if (destroyOnSolve)
                {
                    Destroy(this);
                }
                break;
            
            case false:
                ExitCanvas();
                OnMinigameEnd?.Invoke();
                break;
        }
    }
    
    public void ExitCanvas()
    {
        switch (myMinigame)
        {
            case Minigame.None:
                return;
            
            case Minigame.Skillcheck:
                SkillCheck.Instance.ExitCanvas();
                break;
          
            case Minigame.Radio:
                RadioMinigame.Instance.ExitCanvas();
                break;
           
            case Minigame.Keypad:
                Keypad.Instance.ExitCanvas();
                break;
            
            case Minigame.Switches :
                Switches.Instance.ExitCanvas();
                break;
        }
    }
    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitCanvas();
            OnMinigameEnd?.Invoke();
        }*/
    }
    
}
