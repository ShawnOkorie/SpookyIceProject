using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{ 
  [SerializeField]private TextMeshProUGUI Ans;
  
  private string Answer = "46239";

  public void Number(int number)
  {
    Ans.text += number.ToString();
  }

  public void Enter()
  {
    if (Ans.text == Answer)
    {
      Ans.text = "True";
      
    }
    else
    {
      Ans.text = "FALSE";
    }
  }

  public void Clear()
  {
    Ans.text = null;
  }
}
