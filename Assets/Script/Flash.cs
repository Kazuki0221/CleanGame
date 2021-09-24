using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Flash : MonoBehaviour
{
    [SerializeField] Text start_text;
    bool trigger = true;
    Color c;
    float alpha;
    private void Start()
    {
        c = start_text.color;
        alpha = 1;
    }
    void Update()
    {
        start_text.color = new Color(c.r, c.g, c.b, alpha);

        if (trigger)
        {
            alpha -= Time.deltaTime * 0.5f;
        }
        else
        {
            alpha += Time.deltaTime * 0.5f;
        }

        if (alpha > 1)
        {
            alpha = 1;
            trigger = true;
        }
        else if (alpha < 0)
        {
            alpha = 0;
            trigger = false;
        }
    }
}
