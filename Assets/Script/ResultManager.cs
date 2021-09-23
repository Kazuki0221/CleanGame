using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;

public class ResultManager : MonoBehaviour
{
    int highScore;
    [SerializeField] Text result_text;
    [SerializeField] GameObject[] images;
    [Range(0, 2)]
    int num;
    float delayInput;
    int triggerButton;

    // Start is called before the first frame update
    void Start()
    {
        highScore = GameController.m_score;
        result_text.text = "Your Score : " + highScore.ToString() + "点";

        this.transform.position = images[0].transform.position;
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

        if (h > 0)
        {
            num++;
            if (num > 2) num = 0;
            this.transform.position = images[num].transform.position;
            delayInput += 0.2f;
        }
        else if (h < 0)
        {
            num--;
            if (num < 0) num = 0;
            this.transform.position = images[num].transform.position;
            delayInput += 0.2f;
        }
    }

    public int PushTrigger()
    {
        return num;
    }
}
