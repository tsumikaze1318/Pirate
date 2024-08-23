using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShowRanking : MonoBehaviour
{
    //private void Start()
    //{
    //    // ループした回数をカウント
    //    int count = 0;
    //    // 順位
    //    int rank = 1;
    //    // 順位を表示するテキスト
    //    Text[] rankTexts = GetComponentsInChildren<Text>();
    //    // 獲得スコアのリスト
    //    List<int> scores = new List<int>();

    //    // プレイヤーの数だけループ
    //    foreach (var ranking in GameManager.ScoreToPlayerNum)
    //    {
    //        // スコアを取得
    //        var key = ranking.Key;
    //        // スコアを追加
    //        scores.Add(key);
    //    }

    //    // スコアを降順にソートする
    //    scores = scores.OrderByDescending(i => i).ToList();

    //    // スコアの数だけループ
    //    for (int i = 0; i < GameManager.ScoreToPlayerNum.Count; i++)
    //    {
    //        int same = 0;

    //        for (int j = 0; j < GameManager.ScoreToPlayerNum[scores[i]].Count; j++)
    //        {
    //            RectTransform rect = rankTexts[count].GetComponent<RectTransform>();
    //            rect.position = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(count * 2 - 3, -1.5f, -15f));

    //            count++;
    //            same++;

    //            rankTexts[count - 1].text = $"{rank}位 : {scores[i]}";
    //        }

    //        rank += same;
    //    }
    //}
}
