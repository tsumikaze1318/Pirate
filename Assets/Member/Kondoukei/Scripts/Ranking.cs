using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    [SerializeField]
    private List<PlayerTest> testList = new List<PlayerTest>();
    private int[] points = { };

    // Start is called before the first frame update
    void Start()
    {
        points = new int[testList.Count];
        for (int i = 0; i < testList.Count; i++)
        {
            points[i] = testList[i].Point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < testList.Count; i++)
        //{
        //    points[i] = testList[i].Point;
        //}
        ChangeRank();
        Debug.Log($"{points[0]},{points[1]},{points[2]}");
    }

    private void ChangeRank()
    {
        var list = new List<int>();
        // listに要素を追加
        list.AddRange(points);
        // listをラムダ式でソート
        list.Sort((a, b) => b - a);
        for (int i = 0; i < list.Count; i++)
        {
            points[i] = list[i];
        }
    }
}

//カウント増減でお宝の数
//他プレイヤーのお宝の数が上位の数を越えたら順位が上に入れ変わる
//他プレイヤーのお宝の数が下位の数を下回ったら順位が下に入れ変わる