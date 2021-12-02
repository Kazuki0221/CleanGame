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
    [SerializeField] List<Image> placesImg = new List<Image>();
    float delayInput;
    int num = 0;
  
    void Update()
    {
        if(delayInput > 0)
        {
            delayInput -= Time.deltaTime;
            return;
        }

        var v = Input.GetAxis("Vertical");

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
        placesImg[num].transform.DOScale(new Vector2(1.5f, 1.5f), 1f).OnComplete(() => placesImg[num].transform.DOScale(new Vector2(1,1), 1));

        places[num].GetComponent<Image>().color = Color.cyan;
        places.Where(go => go != tempObj).ToList().ForEach(go => go.GetComponent<Image>().color = Color.white);

    }

    public void MoveMap()
    {
        string place = places[num].name.Replace("To", "");
        SceneManager.LoadScene(place);
        
    }
}
