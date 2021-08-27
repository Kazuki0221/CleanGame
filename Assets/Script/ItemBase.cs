using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public abstract class ItemBase : MonoBehaviour
{
    PlayerControl playerControl;

    public abstract void Active();

    private void Start()
    {
        playerControl = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();   
    }

    private void OnCollisionEnter(Collision collision)//仮置き、拾ったらスコア追加
    {
        if(collision.gameObject.tag == "Player")
        {
            if (playerControl.Catch())
            {
                Active();
                Destroy(this.gameObject);
            }
        }
    }
}
