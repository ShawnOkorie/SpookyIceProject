using System;
using TMPro;
using UnityEngine;

public class Keypad : Singleton<Keypad>, IMinigames
{
  public delegate void MinigameFail(bool solved);
  public event MinigameFail OnMinigameEnd;
  
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
      OnMinigameEnd.Invoke(true);
    }

    else
    {
      display.text = "FALSE";
      OnMinigameEnd.Invoke(false);
    } 
      
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

  private void Start()
  {
    gameObject.SetActive(false);
  }
}
