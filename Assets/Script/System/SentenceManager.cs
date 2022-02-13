using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;



public enum SceneState
{
    Active,
    Story,
    NPC
}
public class SentenceManager : MonoBehaviour
{
    
    [SerializeField]public static SceneState sceneState;

    //bool firstEnable = true;

    Entity_Sheets es;//エクセルデータ
    int num = 0;    //会話順
    int endNum;     //最終会話
    int sheetsID = 0;//シートID
    bool active = true;//決定した際のトリガー用変数
    [SerializeField]int storyID;

    [SerializeField] Text charaname;    //キャラ名テキスト
    [SerializeField] Text sentence;     //会話テキスト
    [SerializeField] Image[] charaImg = new Image[2];   //立ち絵
    [SerializeField] List<Sprite> chara = new List<Sprite>();   //キャラの立ち絵データ
    int[] temp = {0, 0};    //キャラID保存用配列

    Color isTalk = Color.white; //話している人の表示
    Color noTalk = Color.grey;  //話していない人の表示

    Converstation converstation;

    //Activeになった時実行
    void OnEnable()
    {
        //if (sceneState == SceneState.Story) firstEnable = false;

        //if (!firstEnable)
        //{

        //}
        //else firstEnable = false;
       
        es = Resources.Load("SentenceData") as Entity_Sheets;
        InitData();//データ初期化
        if (sceneState == SceneState.Story)
        {
            storyID = MoveStage.storyID;
            storyID = 0;
            for (int i = 0; i < es.sheets[sheetsID].list.Count; i++)
            {
                if (es.sheets[sheetsID].list[i].storyID == storyID)
                {
                    num = i;
                    break;
                }
            }
            for (int i = num; i < es.sheets[sheetsID].list.Count; i++)
            {
                if (es.sheets[sheetsID].list[i].storyID != storyID)
                {
                    endNum = i - 1;
                    break;
                }
                endNum = i;
            }
        }

        if (FindObjectOfType<Converstation>()) converstation = FindObjectOfType<Converstation>();
        if (sceneState == SceneState.Story) sheetsID = 0;
        else if (sceneState == SceneState.NPC) sheetsID = 1;

        temp[0] = es.sheets[sheetsID].list[num].charaID;
        charaImg[0].sprite = chara[temp[0]];

        for (int i = num; i < es.sheets[sheetsID].list.Count; i++)
        {
            if (es.sheets[sheetsID].list[i].charaID != temp[0])
            {
                temp[1] = es.sheets[sheetsID].list[i].charaID;
                charaImg[1].sprite = chara[temp[1]];
                break;
            }

        }
    }

    //非Activeになった時実行
    private void OnDisable()
    {
        InitData();
    }

    void Update()
    {

        //会話進行処理
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (num < endNum)
            {
                num++;
                active = true;
            }
            else
            {
                if(sceneState == SceneState.Story)
                {
                    SceneManager.LoadScene(MoveStage.ToStageName);
                    SetState(SceneState.Active);
                }
                else if(sceneState == SceneState.NPC)
                {
                    if (converstation)
                    {
                        converstation.SetTrigger(false);
                        FindObjectOfType<PlayerControl>().SetState(State.Normal);
                        SetState(SceneState.Active);
                    }
                }
            }
        }
        if(active) StorySheet(sheetsID);
    }

    //ストーリーシート
    void StorySheet(int sID)
    {

        if (temp[0] == es.sheets[sID].list[num].charaID )
        {
            ChangeChara(0);
        }
        else if (temp[1] == es.sheets[sID].list[num].charaID)
        {
            ChangeChara(1);

        }
        else 
        {
            ChangeChara(0);

        }
        
        charaname.text = es.sheets[sID].list[num].name;
        sentence.text = es.sheets[sID].list[num].sentence;
        active = false;
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
        temp[n] = es.sheets[sheetsID].list[num].charaID;

        charaImg[n].sprite = chara[temp[n]];

    }

    public static void SetState(SceneState state)
    {
        sceneState = state;
    }
    
    public SceneState GetState()
    {
        return sceneState;
    }

    //初期化関数
    private void InitData()
    {
        active = true;
        num = 0;
        temp[0] = 0;
        temp[0] = 0;
        charaImg[0].color = new Color(0, 0, 0, 0);
        charaImg[1].color = new Color(0, 0, 0, 0);
    }
}
