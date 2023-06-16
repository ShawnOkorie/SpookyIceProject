using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
   private const int roomNumber = 11;
   public SaveData saveData;

   protected override void Awake()
   {
      base.Awake();
      SaveSystem.Load(out SaveData saveData);
      this.saveData = saveData;

      if (this.saveData.firstLoad)
      {
         this.saveData.firstLoad = false;
         
         InitRoom();
         
         Save();
      }
   }

   public void Save()
   {
      SaveSystem.Save(saveData);
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
