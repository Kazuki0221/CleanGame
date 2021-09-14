using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : ItemBase
{
    [SerializeField] int m_score = 10;
    public override void Active()
    {
        FindObjectOfType<GameController>().AddScore(m_score);
    }
}
