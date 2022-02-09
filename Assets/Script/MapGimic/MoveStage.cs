using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;


public class MoveStage : MonoBehaviour
{
    [SerializeField]List<GameObject> button = new List<GameObject>();
    int num = 0;
    float delayInput;
    [SerializeField] string stageName;//遷移先ステージ
    //[SerializeField] int stageNum = 0;//遷移先ステージID
    //List<PlayerPrefs> storyFlags = new List<PlayerPrefs>();

    [SerializeField] List<CharaData> CharaDatas = new List<CharaData>();

    GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    void Update()
    {
        if (delayInput > 0f)
        {
            delayInput -= Time.deltaTime;
            return;
        }

        float v = Input.GetAxis("Vertical");


        if (v > 0)
        {
            num--;
            if (num < 0) num = button.Count - 1;
            //Sound(0);
            delayInput += 0.2f;
        }
        else if (v < 0)
        {
            num++;
            if (num > button.Count - 1) num = 0;
            //Sound(0);
            delayInput += 0.2f;
        }
        EventSystem.current.SetSelectedGameObject(button[num]);
        button[num].GetComponent<Button>().OnSelect(null);

        if (num == 0)
        {
            button[0].GetComponent<Image>().color = Color.cyan;
            button[1].GetComponent<Image>().color = Color.white;

        }
        else if (num == 1)
        {
            button[1].GetComponent<Image>().color = Color.cyan;
            button[0].GetComponent<Image>().color = Color.white;
        }
    }

    public void ToMove()
    {
        string charaName = GameObject.FindGameObjectWithTag("Player").name.Replace("(Adventure)", "");
        gameManager.chara = CharaDatas.Where(c => c.name == charaName).FirstOrDefault();
        gameManager.sceneState = GameManager.BeforeSceneState.Adventure;
        GameManager.sceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(stageName);
    }

    public void Back()
    {
        PlayerControl player = FindObjectOfType<PlayerControl>();
        player.message.SetActive(false);
        player.SetState(State.Normal);

    }
}
