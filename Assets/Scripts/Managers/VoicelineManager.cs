using System;
using System.Collections;
using System.Collections.Generic;
using DialogSystem;
using UnityEditor;
using UnityEngine;

public class VoicelineManager : Singleton<VoicelineManager>
{
    private AudioSource myAudioSource;
    private AudioClip voiceline;

    private void Start()
    {
        DialogManager.Instance.OnLineStarted += PlayVoiceLine;

        myAudioSource = GetComponent<AudioSource>();
    }

    private void PlayVoiceLine(int pid)
    {
        MonsterSoundManager.Instance.gameObject.SetActive(false);
        
        voiceline = Resources.Load<AudioClip>("Voicelines/" + pid);
        myAudioSource.Stop();
        myAudioSource.clip = voiceline;
        myAudioSource.Play();
    }
}
