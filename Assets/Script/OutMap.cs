using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutMap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var outMap = GameObject.FindGameObjectsWithTag("MapOutArea");
        var lines = new LineRenderer[outMap.Length];
        //outMap.ForEach(outMap, go => );
        for(int i = 0; i < outMap.Length; i++)
        {
            lines[i] = outMap[i].AddComponent<LineRenderer>();
        }
        var positions = new Vector3[] 
        { 
            new Vector3(0, 0, 0), 
            new Vector3(0, 0, 8) 
        };
        foreach(var l in lines)
        {
            l.SetPositions(positions);
        }

    }

}
