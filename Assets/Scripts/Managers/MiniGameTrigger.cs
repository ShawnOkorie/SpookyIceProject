using System;
using System.Collections;
using System.Collections.Generic;
using Minigames;
using UnityEngine;

public class MiniGameTrigger : MonoBehaviour
{
    private SkillCheck skillCheck;
    private RadioMinigame radioMinigame;
    private Keypad keypad;
    
    private Minigame myMinigame;
    
    private enum Minigame
    {
        Skillcheck,
        Radio,
        Keypad
    }

    private void Awake()
    {
        switch (myMinigame)
        {
            case Minigame.Skillcheck:
                skillCheck = FindObjectOfType<SkillCheck>();
                break;
            case Minigame.Radio:
                radioMinigame = FindObjectOfType<RadioMinigame>();
                break;
            case Minigame.Keypad:
                keypad = FindObjectOfType<Keypad>();
                break;
        }
    }

    public void StartMiniGame()
    {
        switch (myMinigame)
        {
            case Minigame.Skillcheck:
                skillCheck.StartMinigame();
                break;
          
            case Minigame.Radio:
                radioMinigame.StartMinigame();
                break;
           
            case Minigame.Keypad:
                //keypad.StartMinigame();
                break;
        }
    }
}
