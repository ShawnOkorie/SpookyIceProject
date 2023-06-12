using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
   private LoadingScreen loadingScreen;

   private void Awake()
   {
      loadingScreen = FindObjectOfType<LoadingScreen>();
   }

   public void Play()
   {
      loadingScreen.ActivateLoadingScreen();
      SceneManager.LoadScene("Main");
   }

   public void Settings()
   {
      
   }

   public void Exit()
   {
      Application.Quit();
   }
}
