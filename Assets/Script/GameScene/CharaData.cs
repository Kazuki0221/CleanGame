using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Character", menuName = "Characters/Character")]
public class CharaData : ScriptableObject
{
    public string Name;
    public Sprite image;
    public Sprite charaBack;
    public GameObject Adventure;
    public GameObject Game;
    public AudioClip voice;
}
