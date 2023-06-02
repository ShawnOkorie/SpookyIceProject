using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadioMinigame : MonoBehaviour
{
   [SerializeField] private GraphRenderer graphSolution;
   [SerializeField] private GraphRenderer graphRenderer;
   private GraphRenderer currentGraphRenderer;
   
   [SerializeField] private Slider amplitudeSlider;
   [SerializeField] private Slider frequencySlider;
   [SerializeField] private Slider movementSpeedSlider;

   [SerializeField] private float sliderValueOffset = 3;
   public bool movementIsFixed;

   private void StartMinigame()
   {
      
   }

   public void SetAmplitude(float value)
   {
      currentGraphRenderer.amplitude = value;
   }
   
   public void SetFrequency(float value)
   {
      currentGraphRenderer.frequency = value;
   }

   private void SetSliderValues(GraphRenderer solutionGraph)
   {
      amplitudeSlider.minValue = solutionGraph.graph.baseAmplitude - sliderValueOffset;
      amplitudeSlider.maxValue = solutionGraph.graph.baseAmplitude + sliderValueOffset;

      frequencySlider.minValue = solutionGraph.graph.baseFrequency - sliderValueOffset;
      frequencySlider.maxValue = solutionGraph.graph.baseFrequency + sliderValueOffset;

      while (amplitudeSlider.minValue == amplitudeSlider.value || solutionGraph.graph.baseAmplitude == amplitudeSlider.value)
         amplitudeSlider.value = Random.Range(amplitudeSlider.minValue, amplitudeSlider.maxValue);
      
     
      while (frequencySlider.minValue == frequencySlider.value || solutionGraph.graph.baseFrequency == frequencySlider.value)
         frequencySlider.value = Random.Range(frequencySlider.minValue, frequencySlider.maxValue);

      if (movementIsFixed)
      {
         movementSpeedSlider.minValue = solutionGraph.graph.baseMovementSpeed - sliderValueOffset;
         movementSpeedSlider.maxValue = solutionGraph.graph.baseMovementSpeed + sliderValueOffset;

         while (movementSpeedSlider.minValue == movementSpeedSlider.value || solutionGraph.graph.baseMovementSpeed == movementSpeedSlider.value)
            movementSpeedSlider.value = Random.Range(movementSpeedSlider.minValue, movementSpeedSlider.maxValue);
      }
   }
   
   public void LoadNextPuzzle()
   {
      
   }
}
