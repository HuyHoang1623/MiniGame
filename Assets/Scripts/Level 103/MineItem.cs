using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineItem : MonoBehaviour
{
    public MineItemType type;
    public int score;
    public float weight;

    public void Start()
    {
        switch (type)
        {
            case MineItemType.Rock:
                score = 5;
                weight = 3f;
                break;
            
            case MineItemType.SmallGold:
                score = 5;
                weight = 1f;
                break;
            
            case MineItemType.MediumGold:
                score = 10;
                weight = 2f;
                break;
            
            case MineItemType.BigGold:
                score = 20;
                weight = 3f;
                break;
            
            case MineItemType.Diamond:
                score = 25;
                weight = 1f;
                break;
            
            case MineItemType.MoveDiamond:
                score = 30;
                weight = 1f;
                break;
        }
    }
}
