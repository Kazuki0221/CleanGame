using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    [SerializeField] List<GameObject> cars = new List<GameObject>();
    float seconds = 0;
    [SerializeField] float delayTime = 5f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        seconds += Time.deltaTime;

        if(seconds > delayTime)
        {
            int kind = Random.Range(0, cars.Count);
            Instantiate(cars[kind], transform.position, transform.rotation);
            seconds = 0;
        }
    }
}
