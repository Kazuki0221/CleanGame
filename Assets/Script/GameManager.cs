using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
   
    /// <summary>
    /// SceneLoad(num)関数内訳
    /// 0→タイトルシーン、1→キャラ選択シーン
    /// 2→ステージ選択シーン、3→リザルトシーン
    /// </summary>
    void Update()
    {
        GameController gController = FindObjectOfType<GameController>();

        if (Input.GetKey(KeyCode.Return))
        {
            if (SceneManager.GetActiveScene().name == "Title")//タイトルシーン
            {
                SceneLoad(1);
            }
            else if(SceneManager.GetActiveScene().name == "CharacterSelect")//キャラ選択シーン
            {
                SceneLoad(2);
            }
            else if(SceneManager.GetActiveScene().name == "StageSelect")
            {
                StageSelectManager sManager = FindObjectOfType<StageSelectManager>();
                SceneManager.LoadScene(sManager.StageName());
            }
            else if (SceneManager.GetActiveScene().name == "ResultScene")//リザルトシーン
            {
                ResultManager resultManager = FindObjectOfType<ResultManager>();
                SceneLoad(resultManager.PushTrigger());
            }
        }

        if (gController)
        {
            if (gController.PushTime() < 0)
            {
                SceneLoad(3);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(SceneManager.GetActiveScene().name == "CharacterSelect")
            {
                SceneLoad(0);
            }
            else if(SceneManager.GetActiveScene().name == "StageSelect")
            {
                SceneLoad(1);
            }
        }

    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    void SceneLoad(int trigger)
    {
        if(trigger == 0)
        {
            SceneManager.LoadScene("Title");
        }
        else if(trigger == 1)
        {
            SceneManager.LoadScene("CharacterSelect");
        }
        else if(trigger == 2)
        {
            SceneManager.LoadScene("StageSelect");
        }
        else if(trigger == 3)
        {
            SceneManager.LoadScene("ResultScene");
        }
    }
}
