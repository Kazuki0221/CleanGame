using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   

    void Update()
    {
        if (Input.GetKey(KeyCode.Return))
        {
            if (SceneManager.GetActiveScene().name == "Title")
            {
                SceneManager.LoadScene("CharacterSelect");
            }
            else if(SceneManager.GetActiveScene().name == "CharacterSelect")
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
