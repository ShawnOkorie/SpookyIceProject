using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class HeatManager : MonoBehaviour
{
    private Coroutine currentTimer;
    [SerializeField] private float timeLimit; 
    private float timer;
   
    private float freezePercent;
    [SerializeField] private Image freezeStageImage;
    [SerializeField] private List<Sprite> freezingStages = new List<Sprite>();

    private void Start()
    {
        timer = timeLimit;
    }


    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            TimerEnded();
        }

        freezePercent =  timer / timeLimit;

        print(freezePercent);
        
        /*
        if (freezePercent <= 50)
        {
            freezeStageImage.sprite = freezingStages[0];
        }
        
        if (freezePercent <= 30)
        {
            freezeStageImage.sprite = freezingStages[1];
        }*/
    }

    private void TimerEnded()
    {
        //trigger death
    }

    private void ResetTimer()
    {
        timer = timeLimit;
    }
}
