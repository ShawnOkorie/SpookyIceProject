using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{
    [SerializeField] private LoadingScreen loadingScreen;
    
    public List<Room> Room = new List<Room>();

    private Rooms currentroom;
    [SerializeField] private Rooms defaultroom;
    public Rooms targetroom;
    
    public enum Rooms
    {
        None,
        CryoRoom,
        GeneratorRoom,
        Infirmary,
        CaveEntrance,
        Lab,
        HallwayEG,
        Hallway1st
    }

    private void Start()
    {
       LoadRoom(defaultroom);
    }

    public void LoadRoom(Rooms target)
    {
        if (loadingScreen == null)
            loadingScreen = FindObjectOfType<LoadingScreen>();

        if (Application.isPlaying)
            loadingScreen.ActivateLoadingScreen();
        
        /*if (currentroom == Rooms.None)
            currentroom = defaultroom;*/

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
        }
       
    }
}
