using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{
    public delegate void RoomChange(Rooms targetroom);
    public event RoomChange OnRoomChange;
    
    public List<Room> Room = new List<Room>();

    public Rooms currentroom;
    [SerializeField] private Rooms defaultroom;
    public Rooms targetroom;

    public AudioSource myAudioSource;
    
    public enum Rooms
    {
        None,
        CryoRoom = 1,
        GeneratorRoom = 2,
        Infirmary = 3,
        CaveEntrance = 4,
        Lab = 5,
        HallwayEG = 6,
        Hallway1st = 7,
        EndRoom = 8
    }

    private void Start()
    {
        /*
        foreach (Room room in Room)
       {
           if (room.myRoom == defaultroom)
           {
               break;
           }
           room.gameObject.SetActive(false);
       }
       */
        
        LoadRoom(defaultroom);
    }

    public void LoadRoom(Rooms target)
    {
        if (Application.isPlaying)
            LoadingScreen.Instance.StartFadeIn();

        if (target == Rooms.None)
        {
            currentroom = target;
            
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
            if (Application.isPlaying)
            {
                GameManager.Instance.Save();
            }
        }
    }
}
