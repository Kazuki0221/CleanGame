using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityStandardAssets.Characters.ThirdPerson;


public class HouseManager : MonoBehaviour
{
    bool isFadeIn;
    Image target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Fade").GetComponent<Image>();

    }
    private void Update()
    {
        if (isFadeIn)
        {
            target.DOColor(new Color(0, 0, 0, 0), 3f).SetEase(Ease.Flash);
            isFadeIn = false;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFadeIn = false;
            var name = this.gameObject.name;
            PlayerControl player = other.GetComponent<PlayerControl>();
            player.SetState(State.Talk);
            if(name == "To1F")
            {
                Transform point1 = GameObject.Find("1FPoint").GetComponent<Transform>();
                target.DOColor(Color.black, 1f).SetEase(Ease.Flash).OnComplete(() =>
                {
                    MovePoint(point1);
                    player.SetState(State.Normal);
                });

            }
            if (name == "To2F")
            {
                Transform point2 = GameObject.Find("2FPoint").GetComponent<Transform>();
                target.DOColor(Color.black, 1f).SetEase(Ease.Flash).OnComplete(() =>
                {
                    MovePoint(point2);
                    player.SetState(State.Normal);
                });
            }
        }
    }

    void MovePoint(Transform point)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject cameraRig = GameObject.FindGameObjectWithTag("Camera");
        player.transform.position = point.position;
        player.transform.rotation = point.rotation;
        cameraRig.transform.position = point.position;
        cameraRig.transform.rotation = point.rotation;
        isFadeIn = true;
    }
}
