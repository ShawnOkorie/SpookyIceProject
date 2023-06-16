using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{
    public delegate void RoomChange(Rooms targetroom);
    public event RoomChange OnRoomChange;
    
    public List<Room> Room = new List<Room>();

    private Rooms currentroom;
    [SerializeField] private Rooms defaultroom;
    public Rooms targetroom;
    
    public enum Rooms
    {
        None,
        CryoRoom = 0,
        GeneratorRoom = 1,
        Infirmary = 2,
        CaveEntrance = 3,
        Lab = 4,
        HallwayEG = 5,
        Hallway1st = 6,
        LabSafeOpen = 7,
        LabSafeClosed = 8,
        LabMonitor = 9,
        GenRoomTanks = 10
    }

    private void Start()
    {
       LoadRoom(defaultroom);
    }

    public void LoadRoom(Rooms target)
    {
        if (Application.isPlaying)
            LoadingScreen.Instance.StartFadeIn();

        if (target == Rooms.None)
        {
            foreach (Room room in Room)
                room.gameObject.SetActive(false);
        }
        
        if (target != currentroom)
        {
            foreach (Room room in Room)
            {
                if (currentroom == room.myRoom)
                    room.gameObject.SetActive(false);
            }

            foreach (Room room in Room)
            {
                if (target == room.myRoom)
                {
                    currentroom = target;
                    room.gameObject.SetActive(true);
                }
            }
            OnRoomChange?.Invoke(target);
            GameManager.Instance.Save();
        }
    }
}
