using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum GameMode
{
    Adventure,
    Game,
}
public class GameManager : MonoBehaviour
{
    //GameObject loadingUI;

    StageSelectManager sManager;

    //bool startScene = true;

    public GameMode mode;

    public bool clickFlag;

    public CharaData chara;

    //遷移前のシーン名格納変数
    public static string sceneName;

    public enum BeforeSceneState
    {
        Adventure,
        StageSelect,
    }
    public BeforeSceneState sceneState;

    private void Start()
    {
        Cursor.visible = false;
    }
    /// <summary>
    /// SceneLoad(num)関数内訳
    /// 0→タイトルシーン、1→キャラ選択シーン
    /// 2→ステージ選択シーン、3→ステージ遷移
    /// 4→リザルトシーン
    /// </summary>
    void Update()
    {
        GameController gController = FindObjectOfType<GameController>();
        ////ロードUI
        //if (GameObject.Find("Loading"))
        //{
        //    //LoadingUI = GameObject.Find("Loading");
        //    //slider = FindObjectOfType<Slider>();
        //    //LoadingUI.SetActive(false);
        //    //startScene = false;
        //}

        //ボタンをクリックした際実行
        if (clickFlag) 
        {
            if (SceneManager.GetActiveScene().name == "Title")//タイトルシーン
            {
                TitleManager titleManager = FindObjectOfType<TitleManager>();

                if (titleManager.modeTrigger == 0)
                {
                    mode = GameMode.Adventure;
                    //SceneManager.LoadScene("City"); 
                    clickFlag = false;
                }
                else if (titleManager.modeTrigger == 1)
                {
                    mode = GameMode.Game;
                    //StartCoroutine(SceneLoad(1));
                    SceneLoad(1);
                }

            }
            else if(SceneManager.GetActiveScene().name == "CharacterSelect")//キャラ選択シーン
            {
                //StartCoroutine(SceneLoad(2));
                SceneLoad(2);
            }
            else if(SceneManager.GetActiveScene().name == "StageSelect")//ステージ選択シーン
            {
                sManager = FindObjectOfType<StageSelectManager>();
                //StartCoroutine(SceneLoad(3));
                SceneLoad(3);
            }
            else if (SceneManager.GetActiveScene().name == "ResultScene")//リザルトシーン
            {
                ResultManager resultManager = FindObjectOfType<ResultManager>();
                //StartCoroutine(SceneLoad(resultManager.triggerNum));
                SceneLoad(resultManager.triggerNum);
            }
        }

        if (gController)
        {
            if (gController.PushTime() < 0)
            {
                //StartCoroutine(SceneLoad(4));
                SceneLoad(4);
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(SceneManager.GetActiveScene().name == "CharacterSelect")
            {
                //StartCoroutine(SceneLoad(0));
                SceneLoad(0);
            }
            else if(SceneManager.GetActiveScene().name == "StageSelect")
            {
                //StartCoroutine(SceneLoad(1));
                SceneLoad(1);
            }
        }

    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //シーン遷移先
    void SceneLoad(int trigger)
    {
        if(trigger == 0)
        {
            //yield return new WaitForSeconds(0.2f);
            //LoadingUI.SetActive(true);
            SceneManager.LoadScene("Title");
        }
        else if(trigger == 1)
        {
            //yield return new WaitForSeconds(0.2f);
            //LoadingUI.SetActive(true);
            SceneManager.LoadScene("CharacterSelect");
        }
        else if(trigger == 2)
        {
            //yield return new WaitForSeconds(0.5f);
            //LoadingUI.SetActive(true);
            SceneManager.LoadScene("StageSelect");
        }
        else if(trigger == 3)
        {
            //yield return new WaitForSeconds(0.2f);
            //LoadingUI.SetActive(true);
            SceneManager.LoadScene(sManager.StageName());
        }
        else if(trigger == 4)
        {
            //yield return new WaitForSeconds(0.2f);
            //LoadingUI.SetActive(true);

            SceneManager.LoadScene("ResultScene");
        }

        clickFlag = false;
    }

}
