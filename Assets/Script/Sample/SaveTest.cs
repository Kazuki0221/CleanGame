using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

[Serializable]
public struct SaveDataTest
{
    public List<bool> flags;
}

public static class SaveTest
{
    public static SaveDataTest sd;
    const string filePath = "saveTest.json";

    public static void saveFlag(bool _flags)
    {
        sd.flags.Add(_flags);
        Save();
    }

    public static void loadFlag()
    {
        foreach(var f in sd.flags)
        {
            Debug.Log(f);
        }
    }

    public static void Save()
    {
        string json = JsonUtility.ToJson(sd);
#if UNITY_EDITOR
        string path = Directory.GetCurrentDirectory();
#else
        string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('//');
#endif
        path += ("/" + filePath);
        StreamWriter writer = new StreamWriter(path, false);
        writer.WriteLine(json);
        writer.Flush();
        writer.Close();
    }

    public static void Load()
    {
        try
        {
        #if UNITY_EDITOR
            string path = Directory.GetCurrentDirectory();
        #else
            string path = AppDomain.CurrentDomain.BaseDirectory.TrimEnd('//');
        #endif
            FileInfo info = new FileInfo(path + "/" + filePath);
            StreamReader reader = new StreamReader(info.OpenRead());
            string json = reader.ReadToEnd();
            sd = JsonUtility.FromJson<SaveDataTest>(json);
        }
        catch(Exception e)
        {
            sd = new SaveDataTest();
        }
    }
}
