using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;
using DG.Tweening;

public class ResultManager : MonoBehaviour
{
    [SerializeField]List<GameObject> btn = new List<GameObject>();
    int highScore;
    [SerializeField] Text result_text;
    //[SerializeField] GameObject[] images;
    [Range(0, 2)]
    int num;
    float delayInput;
    public int triggerNum = 0;

    AudioSource source;
    [SerializeField]AudioClip[] sound;


    void Start()
    {
        highScore = GameController.m_score;
        //result_text.DOCounter(0, highScore);
        result_text.text = highScore.ToString();

        //this.transform.position = images[0].transform.position;
        source = GetComponent<AudioSource>();

    }

    void Update()
    {

        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        if (delayInput > 0f)
        {
            delayInput -= Time.deltaTime;
            return;
        }

        if (h > 0)
        {
            num++;
            if (num > btn.Count - 1) num = 0;
            //this.transform.position = images[num].transform.position;
            Sound(0);
            delayInput += 0.2f;
        }
        else if (h < 0)
        {
            num--;
            if (num < 0) num = btn.Count - 1;
            //this.transform.position = images[num].transform.position;
            Sound(0);
            delayInput += 0.2f;
        }
        EventSystem.current.SetSelectedGameObject(btn[num]);
        btn[num].GetComponent<Button>().OnSelect(null);

        for(int i = 0; i < btn.Count; i++)
        {
            if(i != num)
            {
                btn[i].GetComponent<Image>().color = Color.white;
            }
            else
            {
                btn[i].GetComponent<Image>().color = Color.cyan;
            }
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButton("Fire1"))
        {
            GameController.m_score = 0;
            Sound(1);
        }
    }

    void Sound(int trigger)
    {
        if (trigger == 0)//移動
        {
            source.PlayOneShot(sound[trigger]);
        }
        else if (trigger == 1)//決定
        {
            source.PlayOneShot(sound[trigger]);
        }
    }

    public void ToTitle()
    {
        triggerNum = 0;
        FindObjectOfType<GameManager>().clickFlag = true;
    }

    public void ToCharaSelect()
    {
        triggerNum = 1;
        FindObjectOfType<GameManager>().clickFlag = true;

    }

    public void ToStageSelect()
    {
        triggerNum = 2;
        FindObjectOfType<GameManager>().clickFlag = true;

    }
}
