using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Door : Singleton<RoomManager>
{
    public Room targetroom;

    private void OnMouseDown()
    {
        Instance.LoadRoom(targetroom.myRoom);
    }
}
