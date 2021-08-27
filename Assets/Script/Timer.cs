using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] float countTime = 0;
    [SerializeField] Color baseColor = new Color(0, 0, 0, 1);
    [SerializeField] Color dgColor = new Color(1, 0, 0, 1);
    Text time_text;
    void Start()
    {
        time_text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        countTime -= Time.deltaTime;
        time_text.text = countTime.ToString("F0");
        if(countTime <= 10)
        {
            time_text.color = dgColor;
        }
        else
        {
            time_text.color = baseColor;
        }
        if(countTime < 0)
        {
            Time.timeScale = 0;
        }
    }
}
