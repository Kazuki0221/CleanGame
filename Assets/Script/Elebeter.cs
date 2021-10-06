using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elebeter : MonoBehaviour
{
    Vector3 objPos;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        objPos = transform.position;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = new Vector3(0, Mathf.Sin(Time.time) * 3f, 0);
    }
}
