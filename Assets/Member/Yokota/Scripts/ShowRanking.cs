using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowRanking : MonoBehaviour
{
    private void Start()
    {
        int count = 0;

        var rankTexts = GetComponentsInChildren<Text>();

        for (int i = 0; i < GameManager.ScoreToPlayer.Count; i++)
        {
            for (int j = 0; j < GameManager.ScoreToPlayer[GameManager.ScoreRanking[i]].Count; j++)
            {
                var plObj = Instantiate(GameManager.ScoreToPlayer[GameManager.ScoreRanking[i]][j]
                        , new Vector3(i * 2 - 3, 0, 0)
                        , Quaternion.identity);

                RectTransform rect = rankTexts[count].GetComponent<RectTransform>();
                rect.position = RectTransformUtility.WorldToScreenPoint(Camera.main, plObj.transform.position + new Vector3(0, 2, 0));

                count++;

                rankTexts[count - 1].text = $"{count}位 : {GameManager.ScoreRanking[i]}";
            }
        }
    }
}
