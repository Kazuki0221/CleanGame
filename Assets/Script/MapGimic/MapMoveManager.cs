using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MapMoveManager : MonoBehaviour
{
    Image target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Fade").GetComponent<Image>();
        target.color = Color.black;
        target.DOColor(new Color(0, 0, 0, 0), 3f).SetEase(Ease.Flash);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player") )
        {
            var name = this.gameObject.tag;
            if (name == "HouseArea") {
                target.DOColor(Color.black, 3f).SetEase(Ease.Flash).OnComplete(() => SceneManager.LoadScene("House"));
            }
            else if(name == "OutArea")
            {
                target.DOColor(Color.black, 3f).SetEase(Ease.Flash).OnComplete(() => SceneManager.LoadScene("City"));
            }
            else if(name == "MapOutArea")
            {
                target.DOColor(Color.black, 3f).SetEase(Ease.Flash).OnComplete(() => SceneManager.LoadScene("Map"));
                
            }
        }

    }
}
