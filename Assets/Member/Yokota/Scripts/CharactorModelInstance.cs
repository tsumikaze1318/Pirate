using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharactorModelInstance : MonoBehaviour
{
    private void Awake()
    {
        ModelInstance();
    }

    private void ModelInstance()
    {
        int count = 0;

        List<int> scores = new List<int>();

        foreach (var ranking in GameManager.ScoreToPlayer)
        {
            var key = ranking.Key;
            scores.Add(key);
        }

        for (int i = 0; i < GameManager.ScoreToPlayer.Count; i++)
        {
            for (int j = 0; j < GameManager.ScoreToPlayer[scores[i]].Count; j++)
            {
                var plObj = Instantiate(GameManager.ScoreToPlayer[scores[i]][j]
                        , new Vector3(count * 2 - 3, 0, 0)
                        , Quaternion.identity);

                count++;
            }
        }
    }
}
