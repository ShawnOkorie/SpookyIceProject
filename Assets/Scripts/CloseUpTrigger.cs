using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CloseUpTrigger : MonoBehaviour
{
   public delegate void CloseUpOpen();
   public event CloseUpOpen OnCloseUpOpen;
    
   public delegate void CloseUpExit();
   public event CloseUpExit OnCloseUpExit;
   
   [SerializeField] private Canvas Pinboard;
   [SerializeField] private Canvas PcClose;
   [SerializeField] private Canvas SafeClose;
   [SerializeField] private Canvas InSafeClose;
   [SerializeField] private Canvas SchrankCloseUp;
   [SerializeField] private Canvas GenTankClose;
   [SerializeField] private Canvas AkteCloseUp;
   
   [SerializeField] private Closeup CloseUps;
   
   
   private enum Closeup
   { 
      None,
      PinBoard,
      PcClose,
      SafeClose,
      InSafeClose,
      SchrankCloseUp,
      GenTankClose,
      AkteCloseUp
   }
   
   
    private void Update()
      {
         if (Input.GetMouseButtonDown(0))
         {
            ExitCloseUp();
         }
      }
   
   public void OpenCloseUp()
   {
      OnCloseUpOpen?.Invoke();
      switch (CloseUps)
      {
         case Closeup.None:
            return;
            
         case Closeup.PinBoard:
            RoomManager.Instance.LoadRoom(RoomManager.Rooms.None);
            Pinboard.gameObject.SetActive(true);
            break;
         
         case Closeup.PcClose:
            RoomManager.Instance.LoadRoom(RoomManager.Rooms.None);
            PcClose.gameObject.SetActive(true);
            break;
         
         case Closeup.SafeClose:
            RoomManager.Instance.LoadRoom(RoomManager.Rooms.None);
            SafeClose.gameObject.SetActive(true);
            break;
         
         case Closeup.InSafeClose:
            RoomManager.Instance.LoadRoom(RoomManager.Rooms.None);
            InSafeClose.gameObject.SetActive(true);
            break;
         
         case Closeup.SchrankCloseUp:
            RoomManager.Instance.LoadRoom(RoomManager.Rooms.None);
            SchrankCloseUp.gameObject.SetActive(true);
            break;
         
         case Closeup.GenTankClose:
            RoomManager.Instance.LoadRoom(RoomManager.Rooms.None);
            GenTankClose.gameObject.SetActive(true);
            break;
         
         case Closeup.AkteCloseUp:
            RoomManager.Instance.LoadRoom(RoomManager.Rooms.None);
            AkteCloseUp.gameObject.SetActive(true);
            break;
         
      }
   }
   
   

   
   private void ExitCloseUp()
   {
      OnCloseUpExit?.Invoke();
      
      switch (CloseUps)
      {
         case Closeup.None:
            return;
            
         case Closeup.PinBoard:
            RoomManager.Instance.LoadRoom(RoomManager.Rooms.CryoRoom);
            Pinboard.gameObject.SetActive(false);
            break;
         
         case Closeup.PcClose:
            RoomManager.Instance.LoadRoom(RoomManager.Rooms.Lab);
            PcClose.gameObject.SetActive(false);
            break;
         
         case Closeup.SafeClose:
            SafeClose.gameObject.SetActive(false);
            break;
         
         case Closeup.InSafeClose:
            RoomManager.Instance.LoadRoom(RoomManager.Rooms.Lab);
            InSafeClose.gameObject.SetActive(false);
            break;
         
         case Closeup.SchrankCloseUp:
            RoomManager.Instance.LoadRoom(RoomManager.Rooms.Infirmary);
            SchrankCloseUp.gameObject.SetActive(false);
            break;
         
         case Closeup.GenTankClose:
            RoomManager.Instance.LoadRoom(RoomManager.Rooms.GenRoomTanks);
            GenTankClose.gameObject.SetActive(false);
            break;
         
         case Closeup.AkteCloseUp:
            RoomManager.Instance.LoadRoom(RoomManager.Rooms.CaveEntrance);
            AkteCloseUp.gameObject.SetActive(false);
            break;
         
      }
   }

}
