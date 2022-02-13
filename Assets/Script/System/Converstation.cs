using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public class Converstation : MonoBehaviour
{
    [SerializeField] GameObject sentenceUI; //SentenceManagerオブジェクト
    SentenceManager sentenceManager;
    PlayerControl player;

    bool isTalk = false;
    bool trigger = false;

    void Start()
    {
        if (FindObjectOfType<SentenceManager>())
        {
            sentenceManager = sentenceUI.GetComponent<SentenceManager>();
            sentenceUI.SetActive(false);
        }
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerControl>();
    }

    void Update()
    {
        if (GetTrigger())
        {
            sentenceUI.SetActive(true);
            //player.SetState(State.Talk);
        }
        else
        {
            sentenceUI.SetActive(false);
            isTalk = false;
            //player.SetState(State.Normal);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "NPC" && !isTalk)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                player.SetState(State.Talk);
                SentenceManager.SetState(SceneState.NPC);
                isTalk = true;
                SetTrigger(true);
            }
        }
    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if(other.gameObject.tag == "NPC")
    //    {
    //        player.SetState(State.Normal);
    //        SentenceManager.SetState(SceneState.Active);
    //    }
    //}

    public void SetTrigger(bool t)
    {
        this.trigger = t;
    }
    bool GetTrigger()
    {
        return trigger;
    }
}
