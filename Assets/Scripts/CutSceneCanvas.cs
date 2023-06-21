using System;
using System.Collections;
using System.Collections.Generic;
using DialogSystem;
using Minigames;
using UnityEngine;
using UnityEngine.UI;

public class CutSceneCanvas : Singleton<CutSceneCanvas>
{
   public delegate void CutsceneStart();
   public event CutsceneStart OnCutsceneStart;
    
   public delegate void CutsceneEnd();
   public event CutsceneEnd OnCutsceneEnd;

   [SerializeField] public Canvas myCanvas;
   private Image myImage;
   private bool hasDied;

   [SerializeField] private AudioSource myAudioSource;
   [SerializeField] private Animator myAnimator;
   [SerializeField] private AnimationClip sucess;

   private void Start()
   {
      myImage = GetComponentInChildren<Image>();

      DialogManager.Instance.OnDialogEnd += StartMinigame;
      SkillCheck.Instance.OnMinigameEnd += PlayAnimation;
      LoadingScreen.Instance.OnFadeEnd += StartDialog;
      
      myCanvas.gameObject.SetActive(false);
   }

   private void StartMinigame()
   {
      if (hasDied)
      {
         return;
      }
      
      SkillCheck.Instance.StartMinigame(6,15);
   }

   public void StartCutscene()
   {
      hasDied = false;
      myCanvas.gameObject.SetActive(true);
      OnCutsceneStart?.Invoke();
   }

   private void StartDialog()
   {
      if (myCanvas.gameObject.activeSelf == false)
      {
         return;
      }
      
      DialogManager.Instance.StartDialog(105);
   }
   /*private void LoadNext()
   {
      ++startIndex;

      myImage.sprite = cutsceneImages[startIndex];
      
      DialogManager.Instance.StartDialog(cutsceneTextList[startIndex]);
      
      if (startIndex == endIndex)
      {
         SkillCheck.Instance.StartMinigame(6,10);
      }
   }*/

   private void PlayAnimation(bool solved)
   {
      switch (solved)
      {
         case true:
            myAnimator.SetTrigger("survived");
            StartCoroutine(End());
            break;
         case false:
            myAnimator.SetTrigger("failed");
            myAudioSource.Play();
            hasDied = true;
            break;
      }
   }

   private IEnumerator End()
   {
      yield return new WaitForSeconds(sucess.length);
      EndCutscene();
   }
   
   private void EndCutscene()
   {
      myCanvas.gameObject.SetActive(false);
      
      OnCutsceneEnd?.Invoke();
   }
}
