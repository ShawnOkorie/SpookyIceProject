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

   [SerializeField] private List<Sprite> cutsceneImages = new List<Sprite>();
   [SerializeField] private List<int> cutsceneTextList = new List<int>();

   [SerializeField] private int startIndex;
   [SerializeField] private int endIndex;

   private Animator myAnimator;
   [SerializeField] private AnimationClip sucessAnimation;
   [SerializeField] private AnimationClip deathAnimation;

   private void Start()
   {
      myCanvas = GetComponent<Canvas>();
      myImage = GetComponentInChildren<Image>();
      myAnimator = GetComponent<Animator>();

      DialogManager.Instance.OnDialogEnd += LoadNext;
      SkillCheck.Instance.OnMinigameEnd += PlayAnimation;
   }

   public void StartCutscene(int start, int end)
   {
      OnCutsceneStart?.Invoke();

      startIndex = start;
      endIndex = end;
      
      myImage.sprite = cutsceneImages[startIndex];
      DialogManager.Instance.StartDialog(cutsceneTextList[startIndex]);
   }

   private void LoadNext()
   {
      ++startIndex;

      myImage.sprite = cutsceneImages[startIndex];
      DialogManager.Instance.StartDialog(cutsceneTextList[startIndex]);
      
      if (startIndex == endIndex)
      {
         SkillCheck.Instance.StartMinigame(6,10);
      }
   }

   private void PlayAnimation(bool solved)
   {
      switch (solved)
      {
         case true:
            //myAnimator.Play(); sucess
            
            EndCutscene();
            break;
         case false:
            //myAnimator.Play(); death
            
            //game over screen
            break;
      }
   }
   
   private void EndCutscene()
   {
      myCanvas.gameObject.SetActive(false);
      
      OnCutsceneEnd?.Invoke();
   }
}
