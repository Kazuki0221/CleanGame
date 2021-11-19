using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Converstation : MonoBehaviour
{
    [SerializeField] GameObject sentenceUI; //SentenceManagerオブジェクト
    SentenceManager sentenceManager;

    bool isTalk = false;
    bool trigger = false;
    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectOfType<SentenceManager>())
        {
            sentenceManager = sentenceUI.GetComponent<SentenceManager>();
            sentenceUI.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (trigger) sentenceUI.SetActive(true);
        else sentenceUI.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "NPC" && !isTalk)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                sentenceManager.SetState(SceneState.NPC);
                Debug.Log(sentenceManager.GetState().ToString());
                isTalk = true;
                trigger = true;
            }
        }
    }

    public void SetTrigger(bool t)
    {
        this.trigger = t;
    }
}
