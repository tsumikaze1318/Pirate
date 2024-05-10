using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowRanking : MonoBehaviour
{
    private void Start()
    {
        for (int i = 0; i < GameManager.ScoreRanking.Length; i++)
        {
            Instantiate(GameManager.ScoreToPlayer[GameManager.ScoreRanking[i]]
                        , new Vector3(i * 2, 0, 0)
                        , Quaternion.identity);
        }
    }
}
