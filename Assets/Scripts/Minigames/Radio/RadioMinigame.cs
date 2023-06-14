using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class RadioMinigame : MonoBehaviour, IMinigames
{
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

   [SerializeField] private float sliderValueOffset = 3;
   public bool movementIsFixed;

   public void StartMinigame(int difficulty = 4, int timeLimit = 15)
   {
      myCanvas.gameObject.SetActive(true);
      
      if (movementIsFixed)
      {
         listindex = 3;
         movementSpeedSlider.gameObject.SetActive(true);
         currentGraph = graphAssetList[listindex];
         solutionGraph.graph = currentGraph;
      }
      else
      {
         listindex = 0;
         movementSpeedSlider.gameObject.SetActive(false);
         currentGraph = graphAssetList[listindex];
         solutionGraph.graph = currentGraph;
      }
      
      SetSliderValues(currentGraph);
   }

   public void SetAmplitude()
   {
      graphRenderer.amplitude = amplitudeSlider.value;

      if (CheckIfSolved())
         LoadNext();
   }
   
   public void SetFrequency()
   {
      graphRenderer.frequency = frequencySlider.value;
      
      if (CheckIfSolved())
         LoadNext();
   }

   public void SetMovementSpeed()
   {
      if (movementIsFixed)
         graphRenderer.movementSpeed = movementSpeedSlider.value;
      
      if (CheckIfSolved())
         LoadNext();
   }

   private void SetSliderValues(GraphAsset graphAsset)
   {
      amplitudeSlider.minValue = graphAsset.baseAmplitude - sliderValueOffset;
      amplitudeSlider.maxValue = graphAsset.baseAmplitude + sliderValueOffset;

      frequencySlider.minValue = graphAsset.baseFrequency - sliderValueOffset;
      frequencySlider.maxValue = graphAsset.baseFrequency + sliderValueOffset;

      while (amplitudeSlider.minValue == amplitudeSlider.value || graphAsset.baseAmplitude == amplitudeSlider.value)
         amplitudeSlider.value = Random.Range(amplitudeSlider.minValue, amplitudeSlider.maxValue);
      
      while (frequencySlider.minValue == frequencySlider.value || graphAsset.baseFrequency == frequencySlider.value)
         frequencySlider.value = Random.Range(frequencySlider.minValue, frequencySlider.maxValue);

      if (movementIsFixed)
      {
         movementSpeedSlider.minValue = graphAsset.baseMovementSpeed - sliderValueOffset;
         movementSpeedSlider.maxValue = graphAsset.baseMovementSpeed + sliderValueOffset;

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

   private void LoadNext()
   {
      if (listindex == 3 || listindex == 5)
         myCanvas.gameObject.SetActive(false);
      
      ++listindex;
      currentGraph = graphAssetList[listindex];
      solutionGraph.graph = currentGraph;
      
      SetSliderValues(currentGraph);
   }
}
