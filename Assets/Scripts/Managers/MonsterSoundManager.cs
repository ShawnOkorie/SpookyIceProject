using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class MonsterSoundManager : MonoBehaviour
{
    private AudioSource myAudioSource;

    [SerializeField] private List<AudioClip> monsterSounds = new List<AudioClip>();
    private int currentClip;

    private float timer;
    private bool timerIsZero;
    [SerializeField] private int minTime;
    [SerializeField] private int maxTime;
    
    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        timer = Random.Range(minTime, maxTime + 1);
    }

    private void Update()
    {
        if (timer <= 0)
        {
            timerIsZero = true;
        }
        
        if (timerIsZero == false)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            currentClip = Random.Range(0, monsterSounds.Count);
            myAudioSource.PlayOneShot(monsterSounds[currentClip]);
            
            timer = Random.Range(minTime, maxTime + 1);
            timerIsZero = false;
        }
        
    }
}
