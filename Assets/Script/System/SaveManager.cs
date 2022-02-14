using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.ThirdPerson;

public class SaveManager : MonoBehaviour
{
    public static List<bool> flags = new List<bool>();
    [SerializeField] List<GameObject> button = new List<GameObject>();
    int num = 0;
    float delayInput;

    GameObject option;

    GameManager gameManager;
    [SerializeField] List<CharaData> CharaDatas = new List<CharaData>();
    [SerializeField] Image[] images = new Image[2];
    [SerializeField] Text text;

    private void Start()
    {
        if (GameManager.sceneName == "Load")
        {
            flags = SaveDataManager.sd.flags;
            PlayerControl.loadFlag = true;
            GameManager.sceneName = "";
        }
        else if(GameManager.sceneName == "Init")
        {
            flags.Add(false);
            GameManager.sceneName = "";
        }
        option = GameObject.Find("Option");
        option.SetActive(false);

        string charaName = GameObject.FindGameObjectWithTag("Player").name.Replace("(Adventure)", "");
        gameManager = FindObjectOfType<GameManager>();
        gameManager.chara = CharaDatas.Where(c => c.name == charaName).FirstOrDefault();
    }
    void Update()
    {
        if (option.activeSelf) 
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


            for (int i = 0; i < button.Count;i++)
            {
                if(i == num)
                {
                    button[i].GetComponent<Image>().color = Color.cyan;
                }
                else
                {
                    button[i].GetComponent<Image>().color = Color.white;
                }
            }

            if (Input.GetKeyDown(KeyCode.Tab))
            {
                FindObjectOfType<PlayerControl>().SetState(State.Normal);
                option.SetActive(false);
            }

        }
        else
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                FindObjectOfType<PlayerControl>().SetState(State.Talk);
                option.SetActive(true);
                images[0].sprite = gameManager.chara.charaBack;
                images[1].sprite = gameManager.chara.image;
                text.text = gameManager.chara.Name;
            }
        }
    }
    public void Save()
    {
        Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Quaternion rot = GameObject.FindGameObjectWithTag("Player").transform.rotation;
        string sceneName = SceneManager.GetActiveScene().name;
        SaveDataManager.SaveProgress(flags, pos, rot, sceneName);
    }

    public void Load()
    {
        flags = SaveDataManager.sd.flags;
        PlayerControl.loadFlag = true;
        SceneManager.LoadScene(SaveDataManager.sd.lastSceneName);
    }

    public void ToTitle()
    {
        SceneManager.LoadScene("Title");
    }

}
