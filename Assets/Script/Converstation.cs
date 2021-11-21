using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Converstation : MonoBehaviour
{
    [SerializeField] GameObject sentenceUI; //SentenceManagerオブジェクト
    SentenceManager sentenceManager;

    bool isTalk = false;
    bool trigger = false;

    void Start()
    {
        if (FindObjectOfType<SentenceManager>())
        {
            sentenceManager = sentenceUI.GetComponent<SentenceManager>();
            sentenceUI.SetActive(false);
        }
    }

    void Update()
    {
        if (GetTrigger() ) sentenceUI.SetActive(true);
        else 
        {
            sentenceUI.SetActive(false);
            isTalk = false;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "NPC" && !isTalk)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                sentenceManager.SetState(SceneState.NPC);
                isTalk = true;
                SetTrigger(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject.tag == "NPC")
        {
            sentenceManager.SetState(SceneState.Active);
        }
    }

    public void SetTrigger(bool t)
    {
        this.trigger = t;
    }
    bool GetTrigger()
    {
        return trigger;
    }
}
