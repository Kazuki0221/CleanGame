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

    // Start is called before the first frame update
    void Start()
    {
        image.sprite = stages[0];
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
            if (num >= stages.Count) num = stages.Count - 1;
            delayInput += 0.2f;
            //StartCoroutine(Select(aroowButton[1]));
        }
        else if (h < 0)//左
        {
            num--;
            if (num < 0) num = 0;
            delayInput += 0.2f;
            //StartCoroutine(Select(aroowButton[0]));
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

}
