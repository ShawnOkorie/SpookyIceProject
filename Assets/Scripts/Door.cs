using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Door : MonoBehaviour
{
    public Room targetroom;
    
    private void OnMouseDown()
    {
        RoomManager.Instance.LoadRoom(targetroom.myRoom);
    }
}
