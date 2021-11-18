using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Converstation : MonoBehaviour
{
    [SerializeField] GameObject sentenceUI; //SentenceManagerオブジェクト
    //SentenceManager sentenceManager;
    // Start is called before the first frame update
    void Start()
    {
        //sentenceManager = sentenceUI.GetComponent<SentenceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "NPC" && Input.GetKeyDown(KeyCode.Return))
        {

        }
    }
}
