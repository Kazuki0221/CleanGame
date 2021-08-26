using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetBottle : ItemBase
{
    [SerializeField] int m_score = 10;
    public override void Active()
    {
        FindObjectOfType<GameManager>().AddScore(m_score);
    }
}
