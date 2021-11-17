using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TitleManager : MonoBehaviour
{
    [SerializeField] List<Image> mode = new List<Image>();
    int num = 0;
    float delayInput;
    public int modeTrigger = 0;

    //UI
    //[SerializeField] Text start_text;
    //bool flashTrigger = true;
    //Color c;
    //float alpha;

    //public bool trigger = false;

    //Audio
    AudioSource source;
    [SerializeField] AudioClip sound;
    [SerializeField] AudioClip start; 
    void Start()
    {
        source = GetComponent<AudioSource>();
        //transform.position = Mode[num].position;

    //    c = start_text.color;
    //    alpha = 1;
    //
    }

    void Update()
    {
        if (delayInput > 0f)
        {
            delayInput -= Time.deltaTime;
            return;
        }
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Fire1"))
        {
            //if (!trigger)
            //{              
            //    start_text.gameObject.SetActive(false);
            //    mode.ForEach(img => img.gameObject.SetActive(true));
            //    trigger = true;
            //}
            //if(trigger) 
            source.PlayOneShot(start);
        }


        float v = Input.GetAxis("Vertical");


        if (v > 0)
        {
            num--;
            if (num < 0) num = mode.Count - 1;
            //Sound(0);
            delayInput += 0.2f;
        }
        else if (v < 0)
        {
            num++;
            if (num > mode.Count - 1 ) num = 0;
            //Sound(0);
            delayInput += 0.2f;
        }
        transform.position = mode[num].transform.position;

        if (num == 0)
        {
            mode[0].color = Color.cyan;
            mode[1].color = Color.white;
            modeTrigger = 0;
        }
        else if (num == 1)
        {
            mode[1].color = Color.cyan;
            mode[0].color = Color.white;
            modeTrigger = 1;
        }


        //UI
        /*
        if (!trigger)
        {
            start_text.color = new Color(c.r, c.g, c.b, alpha);

            if (flashTrigger)
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
                flashTrigger = true;
            }
            else if (alpha < 0)
            {
                alpha = 0;
                flashTrigger = false;
            }
        }
        */
    }
}
