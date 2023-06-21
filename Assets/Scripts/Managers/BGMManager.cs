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

   [SerializeField] private AudioClip cutsceneBGM;
   protected override void Awake()
   {
      base.Awake();
      myAudioSource = GetComponent<AudioSource>();
   }

   private void Start()
   {
      CutSceneCanvas.Instance.OnCutsceneStart += StartBGMCutscene;
      CutSceneCanvas.Instance.OnCutsceneEnd += StartBGMCutscene;
      
      StartBGMNormal();
   }

   private void StartBGMNormal()
   {
      myAudioSource.Stop();
      myAudioSource.clip = startBGM;
      myAudioSource.Play();
      StartCoroutine(Loop());
   }
   
   IEnumerator Loop()
   {
      yield return new WaitForSeconds(startBGM.length);
      myAudioSource.Stop();
      myAudioSource.clip = loopBGM;
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
}
