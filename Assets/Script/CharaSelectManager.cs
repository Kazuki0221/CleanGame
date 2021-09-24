using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;


public class CharaSelectManager : MonoBehaviour
{
    public static CharaData chara;//メインへ渡すキャラデータ
    [SerializeField] List<GameObject> character = new List<GameObject>();//キャラクター
    [Range(0, 2)]
    int num = 0;
    Vector3 areaPos;//SelectAreaの位置

    float delayInput;

    [SerializeField] Image image;//立ち絵
    [SerializeField] Image c_back; 
    [SerializeField] Text charaName; 
    [SerializeField] List<CharaData> charaData = new List<CharaData>();//全キャラデータ
    // Start is called before the first frame update
    void Start()
    {
        areaPos = character[0].transform.position;
        areaPos.y += 1;
        this.transform.position = areaPos;
    }

    // Update is called once per frame
    void Update()
    {
        float h = CrossPlatformInputManager.GetAxisRaw("Horizontal");


        if (delayInput > 0f) {
            delayInput -= Time.deltaTime;
            return;
        }

        //キャラ選択
        if (h > 0)
        {
            num++;
            if (num > character.Count - 1) num = 0;
            areaPos = character[num].transform.position;
            areaPos.y += 1;
            this.transform.position = areaPos;
            delayInput = 0.2f;
        }
        else if(h < 0)
        {
            num--;
            if (num < 0) num = character.Count - 1;
            areaPos = character[num].transform.position;
            areaPos.y += 1;
            this.transform.position = areaPos;
            delayInput = 0.2f;
        }

        image.sprite = charaData[num].image;
        c_back.sprite = charaData[num].charaBack;
        charaName.text = charaData[num].Name;
        //キャラ確定
        if (Input.GetKey(KeyCode.Return))
        { 
            chara = charaData[num];
        }
    }

    
}
