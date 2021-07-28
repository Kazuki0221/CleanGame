using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniMapCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject P_Pos = GameObject.FindWithTag("Player");
        Vector3 pos = this.transform.position;
        pos.x = P_Pos.transform.position.x;
        pos.z = P_Pos.transform.position.z;
        this.transform.position = pos;
        
    }
}
