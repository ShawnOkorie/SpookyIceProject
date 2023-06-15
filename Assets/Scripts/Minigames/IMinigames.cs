using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMinigames
{
   public void StartMinigame(int difficulty, int timeLimit);

   //public void EndMiniGame();

   public void ExitCanvas();
}
