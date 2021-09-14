using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "Title" && Input.GetKey(KeyCode.Return))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
