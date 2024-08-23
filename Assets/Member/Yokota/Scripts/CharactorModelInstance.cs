using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharactorModelInstance : MonoBehaviour
{
    //private void Awake()
    //{
    //    ModelInstance();
    //}

    //private void ModelInstance()
    //{
    //    int count = 0;

    //    List<int> scores = new List<int>();

    //    foreach (var ranking in GameManager.ScoreToPlayerNum)
    //    {
    //        var key = ranking.Key;
    //        scores.Add(key);
    //        scores = scores.OrderByDescending(i => i).ToList();
    //    }

    //    for (int i = 0; i < GameManager.ScoreToPlayer.Count; i++)
    //    {
    //        for (int j = 0; j < GameManager.ScoreToPlayer[scores[i]].Count; j++)
    //        {
    //            var plObj = Instantiate(GameManager.ScoreToPlayer[scores[i]][j]
    //                    , new Vector3(count * 2 - 3, -3.52f, -15f)
    //                    , Quaternion.Euler(0f, 180f, 0f));

    //            count++;
    //        }
    //    }
    //}
}
