using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(RoomManager))]

public class CustomComponentRooms : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        RoomManager script = (RoomManager) target;

        if (GUILayout.Button("Open"))
        {
            script.LoadRoom(script.targetroom);
        }
    }
}
