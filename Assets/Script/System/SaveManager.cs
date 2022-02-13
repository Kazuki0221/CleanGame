using System.Collections;
using System.Collections.Generic;
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
        Debug.Log(SaveDataManager.sd.flags[0]);
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
