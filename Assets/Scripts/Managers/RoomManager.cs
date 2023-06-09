using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{
    public List<Room> Room = new List<Room>();

    private Rooms currentroom;
    public Rooms targetroom;
    [SerializeField] private Rooms defaultroom;
    public enum Rooms
    {
        None,
        CryoRoom,
        GeneratorRoom,
        Infirmary,
        Cave,
        Lab,
        Hallway1,
        Hallway2
    }

    /*private void Start()
    {
        foreach (Room room in Room)
        {
            if (currentroom == room.myRoom)
            {
                room.gameObject.SetActive(true);
            }
        }
    }*/

    public void LoadRoom(Rooms target)
    {
        if (currentroom == Rooms.None)
            currentroom = defaultroom;
        
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
