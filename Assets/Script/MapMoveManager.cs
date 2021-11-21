using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapMoveManager : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") )
        {
            var name = this.gameObject.tag;
            if (name == "HouseArea") {
                SceneManager.LoadScene("House");
            }
            else if(name == "OutArea")
            {
                SceneManager.LoadScene("City");
            }
            else if(name == "MapOutArea")
            {
                Debug.Log("Move out Map");
            }
        }

    }
}
