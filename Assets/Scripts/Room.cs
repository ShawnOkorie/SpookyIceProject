using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Room : MonoBehaviour
{
    public RoomManager.Rooms myRoom;

    public List<InteractableObject> myObjects = new List<InteractableObject>();

    private void Start()
    {
        RoomManager.Instance.OnRoomChange += GetMyIntObjects;
    }

    private void GetMyIntObjects(RoomManager.Rooms targetroom)
    {
        if (targetroom != myRoom) return;
       // myIntObjects = GetComponentsInChildren<InteractableObject>().ToList();
       

       for (int i = 0; i < myObjects.Count; i++)
       {
           GameManager.Instance.saveData.RoomInfos[(int)myRoom].IntObjects[i] = new IntObject();
           GameManager.Instance.saveData.RoomInfos[(int)myRoom].IntObjects[i].objectID = myObjects[i].objectID;
       }
    }
}
