using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRanking : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i < GameManager.ScoreToPlayer.Count; i++)
        {
            for (int j = 0; j < GameManager.ScoreToPlayer.Count; j++)
            {
                Instantiate(GameManager.ScoreToPlayer[GameManager.ScoreRanking[i]][j]
                        , new Vector3(i * 2, 0, 0)
                        , Quaternion.identity);
            }
        }
    }
}
