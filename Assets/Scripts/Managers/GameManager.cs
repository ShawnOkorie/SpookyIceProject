using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{
   public const int roomNumber = 16;
   public SaveData GetSavedata => saveData;
   private SaveData saveData;
   private InventoryInfo invInfo;

   private List<InteractableObject> interactableObjects = new List<InteractableObject>();

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
      if (invInfo == null)
      {
         invInfo = new InventoryInfo(Inventory.Instance.invObjects.Count);
      }

      saveData.InventoryInfo = invInfo;
      
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
      for (int i = 0; i < Inventory.Instance.invObjects.Count; i++)
      {
         invInfo.InvObjects[i] = new IntObject(Inventory.Instance.invObjects[i].objectID,
            Inventory.Instance.invObjects[i].isSolved, Inventory.Instance.invObjects[i].requiredProgress);
      }

      saveData.InventoryInfo = invInfo;
      saveData.heatTimer = HeatManager.Instance.currentTimer;
      saveData.progressList = ProgressManager.Instance.checkpointList;
      
      SaveSystem.Save(saveData); 
   }

   public void Load()
   {
      SaveSystem.Load(out SaveData saveData);

      for (int i = 0; i < saveData.InventoryInfo.InvObjects.Length; i++)
      {
        int currObjID = saveData.InventoryInfo.InvObjects[i].objectID;
        interactableObjects = FindObjectsOfType<InteractableObject>().ToList();

        foreach (InteractableObject obj in interactableObjects)
        {
           if (obj.objectID != currObjID)
           {
              break;
           }

           obj.transform.SetParent(Inventory.Instance.myLayout.transform);
        }
      }
      
      HeatManager.Instance.currentTimer = saveData.heatTimer + 60;
      ProgressManager.Instance.checkpointList = saveData.progressList;
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
