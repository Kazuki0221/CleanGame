﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //private AsyncOperation async;
    //GameObject LoadingUI;
    //Slider slider;
    //bool load = false;

    StageSelectManager sManager;
    /// <summary>
    /// SceneLoad(num)関数内訳
    /// 0→タイトルシーン、1→キャラ選択シーン
    /// 2→ステージ選択シーン、3→ステージ遷移
    /// 4→リザルトシーン
    /// </summary>
    void Update()
    {
        GameController gController = FindObjectOfType<GameController>();
        //if (GameObject.Find("Loading"))
        //{
        //    LoadingUI = GameObject.Find("Loading");
        //    slider = FindObjectOfType<Slider>();
        //    LoadingUI.SetActive(false);
        //}


        if (Input.GetKey(KeyCode.Return))
        {
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
                sManager = FindObjectOfType<StageSelectManager>();
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
        DontDestroyOnLoad(this.gameObject);
    }

    IEnumerator SceneLoad(int trigger)
    {
        if(trigger == 0)
        {
            //yield return new WaitForSeconds(0.2f);
            //LoadingUI.SetActive(true);
            //yield return new WaitForSeconds(1);
            //async = SceneManager.LoadSceneAsync("Title");
            SceneManager.LoadScene("Title");
        }
        else if(trigger == 1)
        {
            yield return new WaitForSeconds(0.2f);
            //LoadingUI.SetActive(true);
            //yield return new WaitForSeconds(1);
            //async = SceneManager.LoadSceneAsync("CharacterSelect");
            SceneManager.LoadScene("CharacterSelect");
        }
        else if(trigger == 2)
        {
            yield return new WaitForSeconds(0.5f);
            //LoadingUI.SetActive(true);
            //yield return new WaitForSeconds(1);
            //async = SceneManager.LoadSceneAsync("StageSelect");
            SceneManager.LoadScene("StageSelect");
        }
        else if(trigger == 3)
        {
            yield return new WaitForSeconds(0.2f);
            //LoadingUI.SetActive(true);
            //yield return new WaitForSeconds(1);
            //async = SceneManager.LoadSceneAsync(sManager.StageName());
            SceneManager.LoadScene(sManager.StageName());
        }
        else if(trigger == 4)
        {
            yield return new WaitForSeconds(0.2f);
            //LoadingUI.SetActive(true);
            //yield return new WaitForSeconds(1);

            //async = SceneManager.LoadSceneAsync("ResultScene");
            SceneManager.LoadScene("ResultScene");
        }

        //while (load && slider.value < 1)
        //{
        //    slider.value += 0.2f;
        //    yield return null;
        //}
    }

   
}
