using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpTrigger : MonoBehaviour
{
   public delegate void CloseUpOpen();
   public event CloseUpOpen OnCloseUpOpen;
    
   public delegate void CloseUpExit();
   public event CloseUpExit OnCloseUpExit;
   
   [SerializeField] private Canvas myCloseUp;

   public void OpenCloseUp()
   {
      OnCloseUpOpen?.Invoke();
      
      myCloseUp.gameObject.SetActive(true);
   }

   private void ExitCloseUp()
   {
      OnCloseUpExit?.Invoke();
      
      myCloseUp.gameObject.SetActive(false);
   }

   private void Update()
   {
      if (Input.GetMouseButtonDown(0) && myCloseUp.gameObject.activeSelf)
      {
         ExitCloseUp();
      }
   }
}
