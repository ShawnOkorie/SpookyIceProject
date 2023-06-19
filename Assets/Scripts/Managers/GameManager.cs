using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
   public const int roomNumber = 14;
   public SaveData GetSavedata => saveData;
   private SaveData saveData;

   protected override void Awake()
   {
      base.Awake();
      SaveSystem.Load(out SaveData saveData);
      this.saveData = saveData;
      if (saveData.RoomInfos == null || saveData.RoomInfos.Length < 1)
      {
         this.saveData.RoomInfos = new RoomInfo[roomNumber];
      }

      if (this.saveData.firstLoad)
      {
         this.saveData.firstLoad = false;
         
         InitRoom();
         
         Save();
      }
   }

   private void Start()
   {
      if (saveData.heatTimer == 0 || HeatManager.Instance.currentTimer <= 0)
      {
         saveData.heatTimer = HeatManager.Instance.currentTimer;
      }

      if (saveData.progressList == null || saveData.progressList.Count < 1)
      {
         saveData.progressList = ProgressManager.Instance.checkpointList;
      }
   }

   public void Save()
   {
      saveData.heatTimer = HeatManager.Instance.currentTimer;
      saveData.progressList = ProgressManager.Instance.checkpointList;
      
      SaveSystem.Save(saveData); 
   }

   public SaveData Load()
   {
      SaveSystem.Load(out SaveData saveData);

      HeatManager.Instance.currentTimer = saveData.heatTimer + 60;
      ProgressManager.Instance.checkpointList = saveData.progressList;
      return saveData;
   }

   private void InitRoom()
   {
      saveData.RoomInfos = new RoomInfo[roomNumber];

      for (int i = 0; i < saveData.RoomInfos.Length; i++)
      {
         saveData.RoomInfos[i].IntObjects = new IntObject[10];
      }
   }
}
