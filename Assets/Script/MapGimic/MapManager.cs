using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MapManager : MonoBehaviour
{
    [SerializeField] List<GameObject> places = new List<GameObject>();
    [SerializeField] List<GameObject> placesImg = new List<GameObject>();
    float delayInput;
    int num = 0;
    int tempNum;

    AudioSource audioSource;
    [SerializeField] AudioClip sound;

    private void Start()
    {
        audioSource = GameObject.Find("Audio").GetComponent<AudioSource>();
    }

    void Update()
    {
        if(delayInput > 0)
        {
            delayInput -= Time.deltaTime;
            return;
        }

        var v = Input.GetAxis("Vertical");
        tempNum = num;

        if (v > 0)
        {
            num--;
            if (num < 0) num = places.Count - 1;
            delayInput += 0.2f;
        }
        else if (v < 0)
        {
            num++;
            if (num > places.Count - 1) num = 0;
            delayInput += 0.2f;
        }

        GameObject tempObj = places[num];
        EventSystem.current.SetSelectedGameObject(places[num]);
        places[num].GetComponent<Button>().OnSelect(null);        

        places[num].GetComponent<Image>().color = Color.cyan;
        places.Where(go => go != tempObj).ToList().ForEach(go => go.GetComponent<Image>().color = Color.white);

        if(v == 0 )
        {
            placesImg[num].GetComponent<Animator>().SetBool("Select", true);
            //OneShoot = false;
        }
        else
        {
            placesImg[tempNum].GetComponent<Animator>().SetBool("Select", false);
            //OneShoot = true;
        }


    }

    public void MoveMap()
    {
        audioSource.PlayOneShot(sound);
        string place = places[num].name.Replace("To", "");
        SceneManager.LoadScene(place);
    }
}
