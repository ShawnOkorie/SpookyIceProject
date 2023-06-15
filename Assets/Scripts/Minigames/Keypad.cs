using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class Keypad : MonoBehaviour
{ 
 [SerializeField] private TextMeshProUGUI Display;

  public string Answer = "46239";

  public void Number(int number)
  {
    Display.text += number.ToString();
  }

  public void Enter()
  {
    if (Display.text == Answer)
      Display.text = "True";
    
    else
      Display.text = "FALSE";
  }

  public void Clear()
  {
    Display.text = null;
  }
  
  
}
