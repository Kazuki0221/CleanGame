using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveTestScript : MonoBehaviour
{
    //List<bool> flags = new List<bool>();
    void Start()
    {
        SaveTest.Load();
        Debug.Log(System.IO.Directory.GetCurrentDirectory());
    }

    public void Save()
    {
        //flags.Add(true);
        SaveTest.saveFlag(true);
    }

    public void Data()
    {
        SaveTest.loadFlag();
    }

}
