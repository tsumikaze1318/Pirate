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
        for (int i = 0; i < GameManager.ScoreToPlayer.Count; i++)
        {
            for (int j = 0; j < GameManager.ScoreToPlayer[GameManager.ScoreRanking[i]].Count; j++)
            {
                var plObj = Instantiate(GameManager.ScoreToPlayer[GameManager.ScoreRanking[i]][j]
                        , new Vector3(i * 2 - 3, 0, 0)
                        , Quaternion.identity);
            }
        }
    }
}
