using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShowRanking : MonoBehaviour
{
    private void Start()
    {
        int count = 0;

        int rank = 1;

        var rankTexts = GetComponentsInChildren<Text>();

        List<int> scores = new List<int>();

        foreach (var ranking in GameManager.ScoreToPlayer)
        {
            var key = ranking.Key;
            scores.Add(key);
            scores = scores.OrderByDescending(i => i).ToList();
        }

        for (int i = 0; i < GameManager.ScoreToPlayer.Count; i++)
        {
            int same = 0;

            for (int j = 0; j < GameManager.ScoreToPlayer[scores[i]].Count; j++)
            {
                RectTransform rect = rankTexts[count].GetComponent<RectTransform>();
                rect.position = RectTransformUtility.WorldToScreenPoint(Camera.main, new Vector3(count * 2 - 3, 2, 0));

                count++;
                same++;

                rankTexts[count - 1].text = $"{rank}位 : {scores[i]}";
            }

            rank += same;
        }
    }
}
