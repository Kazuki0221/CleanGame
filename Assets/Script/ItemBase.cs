using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.ThirdPerson;

public abstract class ItemBase : MonoBehaviour
{
    PlayerControl playerControl;

    public abstract void Active(string name);

    private void Start()
    {
        playerControl = GameObject.FindWithTag("Player").GetComponent<PlayerControl>();   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Gomibako")
        {
            Active(other.gameObject.name);
            Destroy(this.gameObject);
        }
    }
}
