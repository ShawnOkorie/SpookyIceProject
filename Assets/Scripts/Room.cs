using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DialogSystem;
using UnityEngine;
using UnityEngine.Serialization;

public class Room : MonoBehaviour
{
    public RoomManager.Rooms myRoom;
    [FormerlySerializedAs("DialogueId")] public int DialogueID;
    private List<InteractableObject> myObjects = new List<InteractableObject>();
    [SerializeField] private List<ObjectDoor> myObjDoor = new List<ObjectDoor>();
    private RoomInfo currRoomInfo;
    private void Start()
    {
        RoomManager.Instance.OnRoomChange += SetupMyIntObjects;
        RoomManager.Instance.OnRoomChange += StartDialogue;
        GameStateManager.Instance.OnRespawn += LoadMyIntObjects;
    }

    private void SetupMyIntObjects(RoomManager.Rooms targetroom)
    {
        if (targetroom != myRoom) return;
        
        myObjects = GetComponentsInChildren<InteractableObject>().ToList();

        for (int i = myObjects.Count - 1; i >= 0; i--)
        {
            if (myObjDoor.Contains(myObjects[i]))
            {
                myObjects.RemoveAt(i);
            }
        }

        currRoomInfo = new RoomInfo(myObjects.Count, myObjDoor.Count);
        for (int i = 0; i < myObjects.Count; i++)
       {
           currRoomInfo.IntObjects[i] = new IntObject(myObjects[i].objectID,myObjects[i].isSolved,myObjects[i].requiredProgress);
       }

        for (int i = 0; i < myObjDoor.Count; i++)
        {
            currRoomInfo.objectDoors[i] = new ObjDoor(myObjDoor[i].isOpen,myObjDoor[i].objectID);
        }

       GameManager.Instance.GetSavedata.RoomInfos[(int)myRoom] = currRoomInfo;
    }

    private void LoadMyIntObjects(RoomManager.Rooms currentroom)
    {
        if (currentroom == myRoom)
        {
            for (int i = 0; i < myObjects.Count; i++)
            {
                for (int j = 0; j < currRoomInfo.IntObjects.Length; j++)
                {
                    if (myObjects[i].objectID == currRoomInfo.IntObjects[j].objectID)
                    {
                        myObjects[i].requiredProgress = currRoomInfo.IntObjects[j].requiredProgress;
                        myObjects[i].isSolved = currRoomInfo.IntObjects[j].isSolved;
                    }
                }
            }

            for (int i = 0; i < myObjDoor.Count; i++)
            {
                for (int j = 0; j < currRoomInfo.objectDoors.Length; j++)
                {
                    if (myObjDoor[i].objectID == currRoomInfo.objectDoors[j].objectID)
                    {
                        myObjDoor[i].isOpen = currRoomInfo.objectDoors[j].isOpen;
                    }
                }
            }
        }
    }

    public void StartDialogue(RoomManager.Rooms targetroom)
    {
        DialogManager.Instance.StartDialog(DialogueID);
    }
}
