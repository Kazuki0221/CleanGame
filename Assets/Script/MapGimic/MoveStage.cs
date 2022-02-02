using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class MoveStage : MonoBehaviour
{

    [SerializeField]List<GameObject> button = new List<GameObject>();
    int num = 0;
    float delayInput;
    [SerializeField] string stageName;

    [SerializeField] List<CharaData> CharaDatas = new List<CharaData>();



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

    public void ToStage()
    {
        string charaName = GameObject.FindGameObjectWithTag("Player").name.Replace("(Adventure)", "");
        FindObjectOfType<GameManager>().chara = CharaDatas.Where(c => c.name == charaName).FirstOrDefault();
        SceneManager.LoadScene(stageName);
    }

    public void Back()
    {
        Debug.Log("Back");
    }
}
