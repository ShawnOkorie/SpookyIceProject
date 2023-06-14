using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Keypad : MonoBehaviour, IMinigames
{
  [SerializeField] private Canvas myCanvas;
  [SerializeField] private TextMeshProUGUI display;

  private string answer = "46239";

  public void Number(int number)
  {
    display.text += number.ToString();
  }

  public void Enter()
  {
    if (display.text == answer)
    {
      display.text = "True";
      ProgressManager.Instance.AddProgress(ProgressManager.Progress.Poo);
    }
    
    else
      display.text = "FALSE";
  }

  public void Clear()
  {
    display.text = null;
  }


  public void StartMinigame(int difficulty, int timeLimit)
  {
    answer = difficulty.ToString();
    myCanvas.gameObject.SetActive(true);
  }
  
  public void ExitCanvas()
  {
    myCanvas.gameObject.SetActive(false);
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.Escape))
      {
        ExitCanvas();
      }
  }
  
}
