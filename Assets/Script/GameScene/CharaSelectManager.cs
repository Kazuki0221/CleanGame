using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;


public class CharaSelectManager : MonoBehaviour
{
    //public static CharaData chara;//メインへ渡すキャラデータ
    [SerializeField] List<GameObject> character = new List<GameObject>();//キャラクター
    [Range(0, 2)]
    int num = 0;
    Vector3 areaPos;//SelectAreaの位置

    float delayInput;
    bool trigger;

    [SerializeField] Image image;//立ち絵
    [SerializeField] Image c_back; 
    [SerializeField] Text charaName; 
    [SerializeField] List<CharaData> charaData = new List<CharaData>();//全キャラデータ
    AudioSource source;
    [SerializeField] AudioClip []sound;

    GameManager gameManager;

    void Start()
    {
        areaPos = character[0].transform.position;
        areaPos.y += 1;
        this.transform.position = areaPos;
        source = GetComponent<AudioSource>();
        trigger = false;

        gameManager = FindObjectOfType<GameManager>();
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
            source.PlayOneShot(sound[0]);
            delayInput = 0.2f;
        }
        else if(h < 0)
        {
            num--;
            if (num < 0) num = character.Count - 1;
            areaPos = character[num].transform.position;
            areaPos.y += 1;
            this.transform.position = areaPos;
            source.PlayOneShot(sound[0]);
            delayInput = 0.2f;
        }

        image.sprite = charaData[num].image;
        c_back.sprite = charaData[num].charaBack;
        charaName.text = charaData[num].Name;
        //キャラ確定
        if ((Input.GetKey(KeyCode.Return)|| Input.GetButton("Fire1"))&& !trigger)
        {
            source.PlayOneShot(charaData[num].voice);
            //GameManager.chara = charaData[num].character;
            gameManager.chara = charaData[num];
            trigger = true;
            gameManager.clickFlag = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            source.PlayOneShot(sound[1]);
        }
    }

    
}
