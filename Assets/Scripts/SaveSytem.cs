using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using UnityEngine;

//Code from Michael Lambertz

public static class SaveSystem
{
    public static string Path = Application.persistentDataPath + "/DASaveData.save";
        
    public static void Save(SaveData data)
    {
        var serializer = new XmlSerializer(typeof(SaveData));
        var stream = new FileStream(Path, FileMode.Create);
        serializer.Serialize(stream, data);
        stream.Close();
    }

    public static void Load(out SaveData data)
    {
        Debug.Log(Path);
        if (File.Exists(Path))
        {
            var serializer = new XmlSerializer(typeof(SaveData));
            var stream = new FileStream(Path, FileMode.Open);
            data = serializer.Deserialize(stream) as SaveData;
            stream.Close();
        }
        else
        {
            data = new SaveData();
            Save(data);
        }
    }
}//End Code from Michael Lambertz
[Serializable]
public class SaveData
{
    public bool firstLoad;
    public RoomInfo[] RoomInfos;
    public List<ProgressManager.Progress> progressList;
    public float heatTimer;
    public InventoryInfo InventoryInfo;
}

[Serializable]
public class IntObject
{
    public int objectID;
    public bool isSolved;
    public ProgressManager.Progress requiredProgress;

    public IntObject()
    {
        
    }
    
    public IntObject(int objectID, bool isSolved, ProgressManager.Progress reqProgress)
    {
        this.objectID = objectID;
        this.isSolved = isSolved;
        requiredProgress = reqProgress;
    }
}

[Serializable]
public class ObjDoor
{
    public int objectID;
    public bool isOpen;

    public ObjDoor()
    {
        
    }
    
    public ObjDoor(bool isOpen, int objectID)
    {
        this.isOpen = isOpen;
        this.objectID = objectID;
    }
}

[Serializable]
public class RoomInfo
{
    public IntObject[] IntObjects;
    public ObjDoor[] objectDoors;

    public RoomInfo()
    {
        
    }
    
    public RoomInfo(int objectCount, int doorCount)
    {
        IntObjects = new IntObject[objectCount];
        objectDoors = new ObjDoor[doorCount];
    }
}

[Serializable]
public class InventoryInfo
{
    public IntObject[] InvObjects;

    public InventoryInfo()
    {
        
    }
    
    public InventoryInfo(int objectCount)
    {
        InvObjects = new IntObject[objectCount];
    }
}

    
