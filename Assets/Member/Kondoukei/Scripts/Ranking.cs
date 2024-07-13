﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ranking : MonoBehaviour
{
    //プレイヤーの得点を格納するリスト
    private int[] points = { };
    [SerializeField]
    private List<Text> rankTexts = new List<Text>();

    // Start is called before the first frame update
    void Start()
    {
        var gameManager = GameManager.Instance;
        points = new int[gameManager.Scores.Length];
        for (int i = 0; i < gameManager.Scores.Length; i++)
        {
            points[i] = gameManager.Scores[i];
        }
    }

    // Update is called once per frame
    void Update()
    {
        ChangeRank();
        //Debug.Log($"{points[0]},{points[1]},{points[2]}");
    }

    private void ChangeRank()
    {
        var list = new List<int>();
        // listの要素を更新
        for (int i = 0; i < GameManager.Instance.Scores.Length; i++)
        {
            //list[i] = testList[i].Point;
            list.Add(GameManager.Instance.Scores[i]);
        }
        // listをラムダ式でソート
        list.Sort((a, b) => b - a);
        for (int i = 0; i < list.Count; i++)
        {
            points[i] = list[i];
            rankTexts[i].text = $"{i + 1} : {points[i]}";
        }
    }
}

//カウント増減でお宝の数
//他プレイヤーのお宝の数が上位の数を越えたら順位が上に入れ変わる
//他プレイヤーのお宝の数が下位の数を下回ったら順位が下に入れ変わる