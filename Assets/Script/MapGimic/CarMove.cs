using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    [SerializeField] float speed = 2f;
    float time;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        transform.position += transform.right * speed * Time.deltaTime;

        if(time > 15)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "DeletePos")
        {

            Destroy(this.gameObject);
        }
    }
}
