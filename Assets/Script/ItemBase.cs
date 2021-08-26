using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
   
    public abstract void Active();

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Gomibako")
        {
            Active();
            Destroy(this.gameObject);
        }
    }
}
