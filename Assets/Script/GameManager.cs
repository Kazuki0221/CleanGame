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
    private AsyncOperation async;
    GameObject LoadingUI;
    Slider slider;

    StageSelectManager sManager;

    //bool startScene = true;

    public GameMode mode;

    public bool clickFlag;

    public CharaData chara;


    /// <summary>
    /// SceneLoad(num)関数内訳
    /// 0→タイトルシーン、1→キャラ選択シーン
    /// 2→ステージ選択シーン、3→ステージ遷移
    /// 4→リザルトシーン
    /// </summary>
    void Update()
    {
        GameController gController = FindObjectOfType<GameController>();
        //ロードUI
        if (GameObject.Find("Loading"))
        {
            LoadingUI = GameObject.Find("Loading");
            slider = FindObjectOfType<Slider>();
            LoadingUI.SetActive(false);
            //startScene = false;
        }

        //ボタンをクリックした際実行
        if (clickFlag) 
        {
            if (SceneManager.GetActiveScene().name == "Title")//タイトルシーン
            {
                TitleManager titleManager = FindObjectOfType<TitleManager>();

                if (titleManager.modeTrigger == 0)
                {
                    mode = GameMode.Adventure;
                    SceneManager.LoadScene("City");
                }
                else if (titleManager.modeTrigger == 1)
                {
                    mode = GameMode.Game;
                    StartCoroutine(SceneLoad(1));
                }

            }
            else if(SceneManager.GetActiveScene().name == "CharacterSelect")//キャラ選択シーン
            {
                StartCoroutine(SceneLoad(2));
            }
            else if(SceneManager.GetActiveScene().name == "StageSelect")//ステージ選択シーン
            {
                sManager = FindObjectOfType<StageSelectManager>();
                StartCoroutine(SceneLoad(3));
            }
            else if (SceneManager.GetActiveScene().name == "ResultScene")//リザルトシーン
            {
                ResultManager resultManager = FindObjectOfType<ResultManager>();
                StartCoroutine(SceneLoad(resultManager.triggerNum));
            }
        }

        if (gController)
        {
            if (gController.PushTime() < 0)
            {
                StartCoroutine(SceneLoad(4));
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if(SceneManager.GetActiveScene().name == "CharacterSelect")
            {
                StartCoroutine(SceneLoad(0));
            }
            else if(SceneManager.GetActiveScene().name == "StageSelect")
            {
                StartCoroutine(SceneLoad(1));
            }
        }

    }

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    //シーン遷移先
    IEnumerator SceneLoad(int trigger)
    {
        if(trigger == 0)
        {
            yield return new WaitForSeconds(0.2f);
            LoadingUI.SetActive(true);
            async = SceneManager.LoadSceneAsync("Title");
        }
        else if(trigger == 1)
        {
            yield return new WaitForSeconds(0.2f);
            LoadingUI.SetActive(true);
            async = SceneManager.LoadSceneAsync("CharacterSelect");
        }
        else if(trigger == 2)
        {
            yield return new WaitForSeconds(0.5f);
            LoadingUI.SetActive(true);
            async = SceneManager.LoadSceneAsync("StageSelect");
        }
        else if(trigger == 3)
        {
            yield return new WaitForSeconds(0.2f);
            LoadingUI.SetActive(true);
            async = SceneManager.LoadSceneAsync(sManager.StageName());
        }
        else if(trigger == 4)
        {
            yield return new WaitForSeconds(0.2f);
            LoadingUI.SetActive(true);

            async = SceneManager.LoadSceneAsync("ResultScene");
        }

        while (slider.value < 1)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            yield return null;
        }
        //startScene = true;
        clickFlag = false;
    }

}
