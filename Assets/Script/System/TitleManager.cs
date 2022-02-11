﻿using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [SerializeField] List<GameObject> mode = new List<GameObject>();
    [SerializeField] GameObject[] buttons = new GameObject[2];
    int num = 0;
    float delayInput;
    public int modeTrigger = 0;

    //UI
    [SerializeField] Text start_text;
    bool flashTrigger = true;
    Color c;
    float alpha;

    public bool modeClick = false;
    public bool startOrContinue = false;

    //Audio
    AudioSource source;
    [SerializeField] AudioClip sound;
    [SerializeField] AudioClip start;

    void Start()
    {
        source = GetComponent<AudioSource>();

        c = start_text.color;
        alpha = 1;
        mode.ForEach(go => go.SetActive(false));
        foreach (var b in buttons)
        {
            b.SetActive(false);
        }

    }

    void Update()
    {
        if (delayInput > 0f)
        {
            delayInput -= Time.deltaTime;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Fire1"))
        {
            if (!modeClick)
            {
                start_text.gameObject.SetActive(false);
                mode.ForEach(go => go.SetActive(true));
                modeClick = true;
            }

            else if(!startOrContinue)
            {
                mode.ForEach(go => go.SetActive(false));
                foreach(var b in buttons)
                {
                    b.SetActive(true);
                }
                startOrContinue = true;
            }
            //if (trigger)
            //    source.PlayOneShot(start);
        }


        float v = Input.GetAxis("Vertical");


        if (v > 0)
        {
            num--;
            if (num < 0) num = mode.Count - 1;
            //Sound(0);
            delayInput += 0.2f;
        }
        else if (v < 0)
        {
            num++;
            if (num > mode.Count - 1 ) num = 0;
            //Sound(0);
            delayInput += 0.2f;
        }
        if (mode[0].activeSelf && mode[1].activeSelf) 
        {
            EventSystem.current.SetSelectedGameObject(mode[num]);
            mode[num].GetComponent<Button>().OnSelect(null);

            if (num == 0)
            {
                mode[0].GetComponent<Image>().color = Color.cyan;
                mode[1].GetComponent<Image>().color = Color.white;
                modeTrigger = 0;

            }
            else if (num == 1)
            {
                mode[1].GetComponent<Image>().color = Color.cyan;
                mode[0].GetComponent<Image>().color = Color.white;
                modeTrigger = 1;
            }
        }
        else if(buttons[0].activeSelf && buttons[1].activeSelf)
        {
            EventSystem.current.SetSelectedGameObject(buttons[num]);
            buttons[num].GetComponent<Button>().OnSelect(null);

            if (num == 0)
            {
                buttons[0].GetComponent<Image>().color = Color.cyan;
                buttons[1].GetComponent<Image>().color = Color.white;

            }
            else if (num == 1)
            {
                buttons[1].GetComponent<Image>().color = Color.cyan;
                buttons[0].GetComponent<Image>().color = Color.white;
            }
        }


        //UI
        
        if (!modeClick)
        {
            start_text.color = new Color(c.r, c.g, c.b, alpha);

            if (flashTrigger)
            {
                alpha -= Time.deltaTime * 0.5f;
            }
            else
            {
                alpha += Time.deltaTime * 0.5f;
            }

            if (alpha > 1)
            {
                alpha = 1;
                flashTrigger = true;
            }
            else if (alpha < 0)
            {
                alpha = 0;
                flashTrigger = false;
            }
        }
        
    }

    public void OnClick()
    {
        FindObjectOfType<GameManager>().clickFlag = true ;
        num = 0;
    }

    public void Load()
    {
        //ロード判定
        GameManager.sceneName = "Load";
        SceneManager.LoadScene(SaveDataManager.sd.lastSceneName);
    }

    public void Init()
    {
        string path = Directory.GetCurrentDirectory() + "/SaveData.json";
        if (File.Exists(path))
        {
            SaveDataManager.InitData();
        }
        else
        {
            SaveDataManager.Load();
            Debug.Log("データ作成:");
            
        }
        SceneManager.LoadScene("City");
    }
}
