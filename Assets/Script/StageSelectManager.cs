using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class StageSelectManager : MonoBehaviour
{
    [SerializeField] List<Sprite> stages = new List<Sprite>();//ステージリスト
    [SerializeField] Image image;
    [SerializeField] Image[] aroowButton;//左or右
    int num = 0;

    float delayInput;

    AudioSource source;
    [SerializeField] AudioClip []sound;

    // Start is called before the first frame update
    void Start()
    {
        image.sprite = stages[0];
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        float h = CrossPlatformInputManager.GetAxis("Horizontal");
        if (delayInput > 0f)
        {
            delayInput -= Time.deltaTime;
            return;
        }

        if (h > 0)//右
        {
            num++;
            if (num >= stages.Count) num = 0;
            Sound(0);
            delayInput += 0.2f;
            //StartCoroutine(Select(aroowButton[1]));
        }
        else if (h < 0)//左
        {
            num--;
            if (num < 0) num = stages.Count - 1;
            Sound(0);
            delayInput += 0.2f;
            //StartCoroutine(Select(aroowButton[0]));
        }

        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButton("Fire1"))
        {
            Sound(1);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Sound(2);
        }

        image.sprite = stages[num];
    }

    public int PushNum()
    {
        return num;
    }

    //IEnumerator Select(Image button)
    //{
    //    button.color = new Color(160, 160, 160);
    //    yield return new WaitForSeconds(0.2f);
    //    button.color = new Color(255, 255, 255);
    //    yield return new WaitForSeconds(0.2f);
    //}

    public string StageName()
    {
        return stages[num].name;
    }

    void Sound(int trigger)
    {
        if(trigger == 0)//移動
        {
            source.PlayOneShot(sound[trigger]);
        }
        else if(trigger == 1)//決定
        {
            source.PlayOneShot(sound[trigger]);
        }
        else if(trigger == 2)//戻る
        {
            source.PlayOneShot(sound[trigger]);
        }
    }

}
