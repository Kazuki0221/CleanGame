using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private AsyncOperation async;
    GameObject LoadingUI;
    Slider slider;

    AudioSource source;
    [SerializeField]AudioClip start;

    /// <summary>
    /// SceneLoad(num)関数内訳
    /// 0→タイトルシーン、1→キャラ選択シーン
    /// 2→ステージ選択シーン、3→ステージ遷移
    /// 4→リザルトシーン
    /// </summary>
    void Update()
    {
        GameController gController = FindObjectOfType<GameController>();
        if (GameObject.Find("Loading"))
        {
            LoadingUI = GameObject.Find("Loading");
            slider = FindObjectOfType<Slider>();
            LoadingUI.SetActive(false);
        }


        if (Input.GetKey(KeyCode.Return))
        {
            LoadingUI.SetActive(true);
            if (SceneManager.GetActiveScene().name == "Title")//タイトルシーン
            {
                StartCoroutine(SceneLoad(1));
            }
            else if(SceneManager.GetActiveScene().name == "CharacterSelect")//キャラ選択シーン
            {
                StartCoroutine(SceneLoad(2));
            }
            else if(SceneManager.GetActiveScene().name == "StageSelect")
            {
                StartCoroutine(SceneLoad(3));
            }
            else if (SceneManager.GetActiveScene().name == "ResultScene")//リザルトシーン
            {
                ResultManager resultManager = FindObjectOfType<ResultManager>();
                StartCoroutine(SceneLoad(resultManager.PushTrigger()));
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
        source = GetComponent<AudioSource>();
        DontDestroyOnLoad(this.gameObject);
    }

    IEnumerator SceneLoad(int trigger)
    {
        if(trigger == 0)
        {
            source.PlayOneShot(start);
            yield return new WaitForSeconds(1);
            async = SceneManager.LoadSceneAsync("Title");
        }
        else if(trigger == 1)
        {
            yield return new WaitForSeconds(1);
            async = SceneManager.LoadSceneAsync("CharacterSelect");
        }
        else if(trigger == 2)
        {
            yield return new WaitForSeconds(1);
            async = SceneManager.LoadSceneAsync("StageSelect");
        }
        else if(trigger == 3)
        {
            yield return new WaitForSeconds(1);

            StageSelectManager sManager = FindObjectOfType<StageSelectManager>();
            async = SceneManager.LoadSceneAsync(sManager.StageName());
        }
        else if(trigger == 4)
        {
            yield return new WaitForSeconds(2);
            async = SceneManager.LoadSceneAsync("ResultScene");
        }

        while (!async.isDone)
        {
            var progressVal = Mathf.Clamp01(async.progress / 0.9f);
            slider.value = progressVal;
            yield return null;
        }
    }

   
}
