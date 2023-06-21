using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BGMManager : Singleton<BGMManager>
{
   [SerializeField] private AudioSource myAudioSource;
   [SerializeField] private AudioClip startBGM;
   [SerializeField] private AudioClip loopBGM;

   [SerializeField] private AudioClip minigameBGM;
   [SerializeField] private AudioClip minigameLoopBGM;
   
   [SerializeField] private AudioClip cutsceneBGM;
   protected override void Awake()
   {
      base.Awake();
      myAudioSource = GetComponent<AudioSource>();
   }

   private void Start()
   {
      RoomManager.Instance.OnRoomChange += SubscribeToEvents;
      
      CutSceneCanvas.Instance.OnCutsceneStart += StartBGMCutscene;
      CutSceneCanvas.Instance.OnCutsceneEnd += StartBGMCutscene;
      
      StartBGMNormal();
   }

   private void SubscribeToEvents(RoomManager.Rooms room)
   {
      foreach (MiniGameTrigger trigger in GameStateManager.Instance.triggerList)
      {
         trigger.OnMinigameStart += StartBGMMinigame;
         trigger.OnMinigameEnd += StartBGMNormal;
      }
   }

   private void StartBGMNormal()
   {
      myAudioSource.Stop();
      myAudioSource.clip = startBGM;
      myAudioSource.Play();
      StartCoroutine(Loop(startBGM,loopBGM));
   }
   
   IEnumerator Loop(AudioClip audioClip,AudioClip audioClip2)
   {
      yield return new WaitForSeconds(audioClip.length);
      myAudioSource.Stop();
      myAudioSource.clip = audioClip2;
      myAudioSource.loop = true;
      myAudioSource.Play();
   }

   private void StartBGMCutscene()
   {
      myAudioSource.Stop();
      myAudioSource.clip = cutsceneBGM;
      myAudioSource.loop = false;
      myAudioSource.Play();
   }

   private void StartBGMMinigame()
   {
      myAudioSource.Stop();
      myAudioSource.clip = minigameBGM;
      myAudioSource.loop = false;
      myAudioSource.Play();
      StartCoroutine(Loop(minigameBGM,minigameLoopBGM));
   }
}
