using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            var name = this.gameObject.name;
            if(name == "To1F")
            {
                Transform point1 = GameObject.Find("1FPoint").GetComponent<Transform>();
                MovePoint(point1);
            }
            if(name == "To2F")
            {
                Transform point2 = GameObject.Find("2FPoint").GetComponent<Transform>();
                MovePoint(point2);

            }
        }
    }

    void MovePoint(Transform point)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject cameraRig = GameObject.FindGameObjectWithTag("Camera");
        player.transform.position = point.position;
        player.transform.rotation = point.rotation;
        cameraRig.transform.position = point.position;
        cameraRig.transform.rotation = point.rotation;
    }
}
