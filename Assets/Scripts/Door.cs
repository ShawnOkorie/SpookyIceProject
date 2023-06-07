using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Room target;
    
    public void ShowInteractability()
    {
        print("Tür öffnen");
    }

    public void Interact()
    {
        RoomManager.Instance.LoadRoom(target.myRoom);
    }
}
