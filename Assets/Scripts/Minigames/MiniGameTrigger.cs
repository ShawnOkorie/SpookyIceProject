using System;
using System.Collections;
using System.Collections.Generic;
using Minigames;
using UnityEngine;

public class MiniGameTrigger : MonoBehaviour
{
    public delegate void MinigameStart();
    public event MinigameStart OnMinigameStart;
    
    public delegate void MinigameEnd();
    public event MinigameEnd OnMinigameEnd;
   
    private InteractableObject interactableObject;
    
    [SerializeField] private ProgressManager.Progress minigameProgress;

    [SerializeField] private int difficulty;
    [SerializeField] private int timeLimit;
    
    [SerializeField] private SkillCheck skillCheck;
    [SerializeField] private RadioMinigame radioMinigame;
    [SerializeField] private Keypad keypad;
    //private Switches switches;
    
    [SerializeField] private Minigame myMinigame;
    
    private enum Minigame
    {
        None,
        Skillcheck,
        Radio,
        Keypad,
        Switches
    }

    private void Awake()
    {
        interactableObject = GetComponent<InteractableObject>();
    }

    private void Start()
    {
        SkillCheck.Instance.OnMinigameEnd += ExitCanvas;
        RadioMinigame.Instance.OnMinigameEnd += ExitCanvas;
        Keypad.Instance.OnMinigameEnd += ExitCanvas;
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
        }
    }

    public void EndMiniGame(bool solved)
    {
        switch (solved)
        {
            case true:
                //ProgressManager.Instance.AddProgress(ProgressManager.Progress.Pee);
                //Destroy(this);
                break;
            
            case false:
                ExitCanvas();
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
        }
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitCanvas();
            OnMinigameEnd?.Invoke();
        }
    }
    
}
