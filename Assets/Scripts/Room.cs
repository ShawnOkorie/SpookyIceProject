using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class Room : MonoBehaviour
{
    public RoomManager.Rooms myRoom;

    private void Start()
    {
        RoomManager.Instance.OnRoomChange += SetupMyIntObjects;
    }

    private void SetupMyIntObjects(RoomManager.Rooms targetroom)
    {
        if (targetroom != myRoom) return;
        
        List<InteractableObject> myObjects = GetComponentsInChildren<InteractableObject>().ToList();

        RoomInfo currentroom = new RoomInfo(myObjects.Count);
        for (int i = 0; i < myObjects.Count; i++)
       {
           currentroom.IntObjects[i] = new IntObject(myObjects[i].objectID,myObjects[i].isInteractable,myObjects[i].requiredProgress);
       }

       GameManager.Instance.GetSavedata.RoomInfos[(int)myRoom] = currentroom;
    }
}
