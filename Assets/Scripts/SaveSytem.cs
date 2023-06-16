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
}
[Serializable]
public class SaveData
{
    public bool firstLoad;
    public RoomInfo[] RoomInfos;
}

[Serializable]
public class IntObject
{
    public int objectID;
}

[Serializable]
public class RoomInfo
{
    public IntObject[] IntObjects;
}

    
