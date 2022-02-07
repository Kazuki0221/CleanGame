using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class ResultManager : MonoBehaviour
{
    [SerializeField]List<GameObject> btn = new List<GameObject>();
    List<GameObject> activeButton = new List<GameObject>();
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
        GameManager gameManager = FindObjectOfType<GameManager>();
        if(gameManager.sceneState == GameManager.BeforeSceneState.Adventure)
        {
            for(int i = 0; i < 3; i++)
            {
                btn[i].SetActive(false);
            }
        }
        else if(gameManager.sceneState == GameManager.BeforeSceneState.StageSelect)
        {
            for(int i = 3; i < btn.Count; i++)
            {
                btn[i].SetActive(false);
            }
        }

        activeButton = btn.Where(go => go.activeSelf).ToList();

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
            if (num > activeButton.Count - 1) num = 0;
            //this.transform.position = images[num].transform.position;
            Sound(0);
            delayInput += 0.2f;
        }
        else if (h < 0)
        {
            num--;
            if (num < 0) num = activeButton.Count - 1;
            //this.transform.position = images[num].transform.position;
            Sound(0);
            delayInput += 0.2f;
        }
        EventSystem.current.SetSelectedGameObject(activeButton[num]);
        btn[num].GetComponent<Button>().OnSelect(null);

        for(int i = 0; i < activeButton.Count; i++)
        {
            if(i != num)
            {
                activeButton[i].GetComponent<Image>().color = Color.white;
            }
            else
            {
                activeButton[i].GetComponent<Image>().color = Color.cyan;
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

    public void Retry()
    {

    }

    public void ToMap()
    {
        FindObjectOfType<GameManager>().mode = GameMode.Adventure;
        SceneManager.LoadScene(GameManager.sceneName);
        GameManager.sceneName = SceneManager.GetActiveScene().name;
    }
}
