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

    public GameObject option;
    GameObject help;
    bool helpFlag = false;

    GameManager gameManager;
    [SerializeField] List<CharaData> CharaDatas = new List<CharaData>();
    [SerializeField] Image[] images = new Image[2];
    [SerializeField] Text text;

    AudioSource audioSource;
    [SerializeField] AudioClip openMenu;
    [SerializeField] AudioClip select;

    private void Start()
    {
        audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();

        option = GameObject.Find("Option");
        option.SetActive(false);
        help = GameObject.Find("Help");
        help.SetActive(false);
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
            FindObjectOfType<PlayerControl>().SetState(State.Talk);
            Help();
        }

        string charaName = GameObject.FindGameObjectWithTag("Player").name.Replace("(Adventure)", "");
        gameManager = FindObjectOfType<GameManager>();
        gameManager.chara = CharaDatas.Where(c => c.name == charaName).FirstOrDefault();
    }
    void Update()
    {
        if (help.activeSelf && (option.activeSelf || !option.activeSelf))
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                audioSource.PlayOneShot(openMenu);
                FindObjectOfType<PlayerControl>().SetState(State.Normal);
                help.SetActive(false);
                helpFlag = false;
            }
        }
        else if (option.activeSelf)
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


            for (int i = 0; i < button.Count; i++)
            {
                if (i == num)
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
                audioSource.PlayOneShot(openMenu);
                FindObjectOfType<PlayerControl>().SetState(State.Normal);
                option.SetActive(false);
            }

        }
        else if (helpFlag == false)
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                audioSource.PlayOneShot(openMenu);
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
        audioSource.PlayOneShot(select);

        Vector3 pos = GameObject.FindGameObjectWithTag("Player").transform.position;
        Quaternion rot = GameObject.FindGameObjectWithTag("Player").transform.rotation;
        string sceneName = SceneManager.GetActiveScene().name;
        SaveDataManager.SaveProgress(flags, pos, rot, sceneName);
    }

    public void Load()
    {
        audioSource.PlayOneShot(select);

        flags = SaveDataManager.sd.flags;
        PlayerControl.loadFlag = true;
        SceneManager.LoadScene(SaveDataManager.sd.lastSceneName);
    }

    public void ToTitle()
    {
        audioSource.PlayOneShot(select);

        SceneManager.LoadScene("Title");
    }

    public void Help()
    {
        audioSource.PlayOneShot(select);
        helpFlag = true;
        help.SetActive(true);
    }

}
