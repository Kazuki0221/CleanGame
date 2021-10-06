using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleManager : MonoBehaviour
{
    AudioSource source;
    [SerializeField] AudioClip sound;
    [SerializeField] AudioClip start; 
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return) || Input.GetButtonDown("Fire1"))
        {
            source.PlayOneShot(start);
        }
    }

}
