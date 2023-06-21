using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RadioMinigame : Singleton<RadioMinigame>, IMinigames
{
   
   public delegate void MinigameEnd(bool solved);
   public event MinigameEnd OnMinigameEnd;

   [SerializeField] private InteractableObject myIntObject;
   [SerializeField] private MiniGameTrigger myMiniGameTrigger;
   [SerializeField] private Image greenLight;
   [SerializeField] private GameObject brokenSlider;
   [SerializeField] private Canvas myCanvas;
   [Header("Graphs")]
   [SerializeField] private GraphRenderer solutionGraph;
   [SerializeField] private GraphRenderer graphRenderer;

   public List<GraphAsset> graphAssetList = new List<GraphAsset>();
   private GraphAsset currentGraph;
   private int listindex;
   
   [Header("Sliders")]
   [SerializeField] private Slider amplitudeSlider;
   [SerializeField] private Slider frequencySlider;
   [SerializeField] private Slider movementSpeedSlider;
   
   [HideInInspector] public bool movementIsFixed;

   public void StartMinigame(int difficulty, int timeLimit)
   {
      myCanvas.gameObject.SetActive(true);
      
      if (ProgressManager.Instance.ContainsProgress(ProgressManager.Progress.radioRepaired))
      {
         movementIsFixed = true;
      }
      
      if (movementIsFixed)
      {
         listindex = 3;
         movementSpeedSlider.gameObject.SetActive(true);
         brokenSlider.gameObject.SetActive(false);
         currentGraph = graphAssetList[listindex];
         solutionGraph.graph = currentGraph;
      }
      else
      {
         listindex = 0;
         movementSpeedSlider.gameObject.SetActive(false);
         brokenSlider.gameObject.SetActive(true);
         currentGraph = graphAssetList[listindex];
         solutionGraph.graph = currentGraph;
      }
      
      //SetSliderValues(currentGraph);
      SetHandlePosition(currentGraph);
   }

   public void ExitCanvas()
   {
      myCanvas.gameObject.SetActive(false);
   }
   

   public void SetAmplitude()
   {
      graphRenderer.amplitude = amplitudeSlider.value;

      ChangeLamp(CheckIfSolved());
   }
   
   public void SetFrequency()
   {
      graphRenderer.frequency = frequencySlider.value;
      
      ChangeLamp(CheckIfSolved());
   }

   public void SetMovementSpeed()
   {
      if (movementIsFixed)
         graphRenderer.movementSpeed = movementSpeedSlider.value;
      
      ChangeLamp(CheckIfSolved());
   }

   /*private void SetSliderValues(GraphAsset graphAsset)
   {
      amplitudeSlider.minValue = graphAsset.baseAmplitude - sliderValueOffset;
      amplitudeSlider.maxValue = graphAsset.baseAmplitude + sliderValueOffset;

      frequencySlider.minValue = graphAsset.baseFrequency - sliderValueOffset;
      frequencySlider.maxValue = graphAsset.baseFrequency + sliderValueOffset;

      
      if (movementIsFixed)
      {
         movementSpeedSlider.minValue = graphAsset.baseMovementSpeed - sliderValueOffset;
         movementSpeedSlider.maxValue = graphAsset.baseMovementSpeed + sliderValueOffset;
      }
   }*/

   private void SetHandlePosition(GraphAsset graphAsset)
   {
      while (amplitudeSlider.minValue == amplitudeSlider.value || graphAsset.baseAmplitude == amplitudeSlider.value)
         amplitudeSlider.value = Random.Range(amplitudeSlider.minValue, amplitudeSlider.maxValue);
      
      while (frequencySlider.minValue == frequencySlider.value || graphAsset.baseFrequency == frequencySlider.value)
         frequencySlider.value = Random.Range(frequencySlider.minValue, frequencySlider.maxValue);
      
      if (movementIsFixed)
      {
         while (movementSpeedSlider.minValue == movementSpeedSlider.value || solutionGraph.graph.baseMovementSpeed == movementSpeedSlider.value)
            movementSpeedSlider.value = Random.Range(movementSpeedSlider.minValue, movementSpeedSlider.maxValue);
      }
   }

   private bool CheckIfSolved()
   {
      if (graphRenderer.amplitude == solutionGraph.amplitude)
      {
         if (graphRenderer.frequency == solutionGraph.frequency)
         {
            if (movementIsFixed == false)
            {
               return true;
            }

            if (graphRenderer.movementSpeed == solutionGraph.movementSpeed)
            {
               return true;
            }
         }
      }
      return false;
   }

   public void LoadNext()
   {
      if (greenLight.gameObject.activeSelf)
      {
         ++listindex;
         greenLight.gameObject.SetActive(false);
         
         if (listindex == 3)
         {
            OnMinigameEnd.Invoke(true);
            myIntObject.solvingObjectID = 99;
            myIntObject.isSolved = false;
            myIntObject.addedProgress = ProgressManager.Progress.radioRepaired;
            myMiniGameTrigger.minigameProgress = ProgressManager.Progress.radiosolved2;
            return;
         }

         if (listindex == 6)
         {
            OnMinigameEnd.Invoke(true);
            return;
         }
      
         currentGraph = graphAssetList[listindex];
         solutionGraph.graph = currentGraph;
      
         //SetSliderValues(currentGraph);
      }
   }

   private void ChangeLamp(bool solved)
   {
      switch (solved)
      {
         case true:
            greenLight.gameObject.SetActive(true);
            break;
         case false:
            greenLight.gameObject.SetActive(false);
            break;
      }
   }

   private void Start()
   {
      gameObject.SetActive(false);
   }
   
   private void Update()
   {
      if (Input.GetKeyDown(KeyCode.Escape))
      {
         gameObject.SetActive(false);
      }
   }
}
