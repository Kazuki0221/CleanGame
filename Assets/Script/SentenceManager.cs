using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;
using UnityEngine.UI;

public class SentenceManager : MonoBehaviour
{

    Entity_Sheets es;
    int num = 0;
    bool active = false;

    [SerializeField] Text charaname;
    [SerializeField] Text sentence;
    [SerializeField] Image[] charaImg = new Image[2];
    [SerializeField] List<Sprite> chara = new List<Sprite>();
    int[] temp = {0, 1};

    Color isTalk = Color.white;
    Color noTalk = Color.grey;

    void Start()
    {
        es = Resources.Load("SentenceData") as Entity_Sheets;
        charaImg[0].sprite = chara[es.sheets[0].list[0].id];
        charaImg[1].color = new Color(0, 0, 0, 0);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) && num < es.sheets[0].list.Count - 1)
        {
            num++;
            active = true;
        }
        if (active)
        {
            if(temp[0] == es.sheets[0].list[num].id)
            {
                ChangeChara(0);
            }
            else if(temp[1] == es.sheets[0].list[num].id)
            {
                ChangeChara(1);

            }
            else if (temp[0] != es.sheets[0].list[num].id)
            {
                ChangeChara(0);

            }
            else if (temp[1] != es.sheets[0].list[num].id)
            {
                ChangeChara(1);

            }
            active = false;
        }

        charaname.text = es.sheets[0].list[num].name;
        sentence.text = es.sheets[0].list[num].sentence;
    }

    void ChangeChara(int n)
    {
        if (n == 0)
        {
            charaImg[n].sprite = chara[es.sheets[0].list[num].id];
            if (charaImg[1].color.a != 0) charaImg[1].color = noTalk;
            charaImg[n].color = isTalk;

            temp[n] = es.sheets[0].list[num].id;
        }
        if(n == 1)
        {
            charaImg[n].sprite = chara[es.sheets[0].list[num].id];
            if (charaImg[0].color.a != 0) charaImg[0].color = noTalk;
            charaImg[n].color = isTalk;

            temp[n] = es.sheets[0].list[num].id;
        }
    }
}
