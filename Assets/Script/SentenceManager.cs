﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;


public enum SceneState
{
    Story,
    NPC
}
public class SentenceManager : MonoBehaviour
{
    
    SceneState sceneState;

    Entity_Sheets es;//エクセルデータ
    int num = 0;    //会話順
    int sheetsID = 0;//シートID
    bool active = true;//決定した際のトリガー用変数

    [SerializeField] Text charaname;    //キャラ名テキスト
    [SerializeField] Text sentence;     //会話テキスト
    [SerializeField] Image[] charaImg = new Image[2];   //立ち絵
    [SerializeField] List<Sprite> chara = new List<Sprite>();   //キャラの立ち絵データ
    int[] temp = {0, 0};    //キャラID保存用配列

    Color isTalk = Color.white; //話している人の表示
    Color noTalk = Color.grey;  //話していない人の表示

    //Converstation converstation;

    //Activeになった時実行
    void OnEnable()
    {
        es = Resources.Load("SentenceData") as Entity_Sheets;
        //converstation = FindObjectOfType<Converstation>();
        sceneState = GetState();
        if (sceneState == SceneState.Story) sheetsID = 0;
        else if (sceneState == SceneState.NPC) sheetsID = 1;
        //charaImg[0].sprite = chara[es.sheets[0].list[0].charaID];
        charaImg[0].color = new Color(0, 0, 0, 0);
        charaImg[1].color = new Color(0, 0, 0, 0);
        num = 0;
        temp[0] = es.sheets[sheetsID].list[0].charaID;

        for(int i = 0; i < es.sheets[sheetsID].list.Count; i++)
        {
            if(temp[1] != temp[0])
            {
                temp[1] = es.sheets[sheetsID].list[i].charaID;
                Debug.Log(temp[1]);
                break;
            }
        }
        
    }

    //非Activeになった時実行
    //private void OnDisable()
    //{
    //    num = 0;
    //}

    void Update()
    {
        //会話進行処理
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (num < es.sheets[0].list.Count - 1)
            {
                num++;
                active = true;
            }
            else
            {
                //converstation.SetTrigger(false);
                this.gameObject.SetActive(false);
            }
        }

        StorySheet(sheetsID);
        
    }

    //ストーリーシート
    void StorySheet(int sID)
    {
        if (active)
        {
            if (temp[0] == es.sheets[sID].list[num].charaID)
            {
                ChangeChara(0);
            }
            else if (temp[1] == es.sheets[sID].list[num].charaID)
            {
                ChangeChara(1);

            }
            else if (temp[0] != es.sheets[sID].list[num].charaID)
            {
                ChangeChara(0);

            }
            else if (temp[1] != es.sheets[sID].list[num].charaID)
            {
                ChangeChara(1);

            }
            active = false;
        }

        charaname.text = es.sheets[sID].list[num].name;
        sentence.text = es.sheets[sID].list[num].sentence;
    }
    //キャラ切り替え関数
    void ChangeChara(int n)
    {
        if (n == 0)
        {
            if (charaImg[1].color.a != 0) charaImg[1].color = noTalk;
        }
        else if (n == 1)
        {
            if (charaImg[0].color.a != 0) charaImg[0].color = noTalk;       
        }

        charaImg[n].color = isTalk;
        charaImg[n].sprite = chara[es.sheets[0].list[num].charaID];
        temp[n] = es.sheets[0].list[num].charaID;

    }

    public void SetState(SceneState state)
    {
        this.sceneState = state;
    }
    
    public SceneState GetState()
    {
        return sceneState;
    }

}
