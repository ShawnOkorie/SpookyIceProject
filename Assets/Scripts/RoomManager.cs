using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : Singleton<RoomManager>
{
    public List<Room> Room = new List<Room>();

    private Rooms currentroom;
    [SerializeField] public Rooms targetroom;

    public enum Rooms
    {
        Hallway1,
        CryoRoom,
        GeneratorRoom,
        Infirmary,
        Cave,
        Lab,
        
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
        if (target != currentroom)
        {
            foreach (Room room in Room)
            {
                if (currentroom == room.myRoom)
                {
                    room.gameObject.SetActive(false);
                }

                if (target == room.myRoom)
                {
                    currentroom = target;
                    room.gameObject.SetActive(true);
                }
            }
        }
    }
}
