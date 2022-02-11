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
            GameManager.sceneName = "";
        }
        option = GameObject.Find("Option");
        option.SetActive(false);
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
        else
        {
            if(Input.GetKeyDown(KeyCode.Tab))
            {
                option.SetActive(true);
            }
        }
    }
    public void Save()
    {
        Transform transform = GameObject.FindGameObjectWithTag("Player").transform;
        string sceneName = SceneManager.GetActiveScene().name;
        SaveDataManager.SaveProgress(flags, transform, sceneName);
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

    public void Back()
    {
        option.SetActive(false);
    }

}
