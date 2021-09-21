﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bin : ItemBase
{
    [SerializeField] int m_score = 10;
    public override void Active(string name)
    {
        if (name == "BinBox") m_score = 30;
        else m_score = -10;
        FindObjectOfType<GameController>().AddScore(m_score);
    }
}
