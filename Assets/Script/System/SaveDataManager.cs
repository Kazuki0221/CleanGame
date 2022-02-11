using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using UnityEngine;

[Serializable]
public struct SaveData
{
    public List<bool> flags;
    public Vector3 playerPos;
    public Quaternion playerRot;
    public string lastSceneName;
}

public static class SaveDataManager
{
    public static SaveData sd;
    const string filePath = "SaveData.json";

    //セーブ
    public static void SaveProgress(List<bool> _flags, Vector3 pos,Quaternion rot, string name)
    {
        sd.flags = _flags;
        sd.playerPos = pos;
        sd.playerRot = rot;
        sd.lastSceneName = name;
        Save();
    }
    //進捗ロード
    public static List<bool> LoadFrags()
    {
        return sd.flags;
    }

    //セーブデータの初期化
    public static void InitData()
    {
        if (sd.flags == null)
        {
            sd.flags = new List<bool>();
            sd.flags.Add(false);
        }
        
        sd.flags.Clear();
        sd.flags.Add(false);
        sd.playerPos = new Vector3(0, 0, 0);
        sd.playerRot = Quaternion.Euler(0, 0, 0);
        sd.lastSceneName = "";
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
            sd = JsonUtility.FromJson<SaveData>(json);
        }
        catch (Exception e)
        {
            sd = new SaveData();
        }
    }
}