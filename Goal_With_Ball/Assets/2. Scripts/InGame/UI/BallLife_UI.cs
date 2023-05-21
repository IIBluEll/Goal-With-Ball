using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallLife_UI : MonoBehaviour
{
    [SerializeField] private Text lifeCount;
    [SerializeField] private Ball_Mgr playerBall;
    private void Start()
    {
        lifeCount.text = InGameSystem.instance.Life.ToString();

        InGameSystem.instance.BallDie += BallDie;
        InGameSystem.instance.ChangeLife += ChangeLifeValue;
    }

    private void ChangeLifeValue()
    {
        lifeCount.text = InGameSystem.instance.Life.ToString();
    }

    private void BallDie()
    {
        InGameSystem.instance.ChangeLife -= ChangeLifeValue;
        lifeCount.text = "Player Die!";
    }

    private void OnDestroy()
    {
        InGameSystem.instance.BallDie -= BallDie;
    }
}
