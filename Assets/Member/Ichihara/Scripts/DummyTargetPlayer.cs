using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTargetPlayer : MonoBehaviour
{
    private readonly string playerTag = "Player";

    private GameObject kraken = null;

    private Transform playerTransform = null;

    private float distanceBetweenKrakenAndPlayerposition = 0f;

    // Start is called before the first frame update
    void Start()
    {
        kraken = gameObject;
    }

    private void LateUpdate()
    {
        SetPlayerPosition();
    }

    private void SetPlayerPosition()
    {
        // プレイヤーオブジェクトをすべて検索
        var playerObjs = GameObject.FindGameObjectsWithTag(playerTag);
        Vector3 KrakenPosition = kraken.transform.position;
        for (int i = 0; i < playerObjs.Length; i++)
        {
            var obj = playerObjs[i];
            Vector3 playerPosition = obj.transform.position;
            float distance = Vector3.Distance(KrakenPosition, playerPosition);
            if (distanceBetweenKrakenAndPlayerposition == 0f ||
               distanceBetweenKrakenAndPlayerposition > distance)
            {
                distanceBetweenKrakenAndPlayerposition = distance;
                playerTransform = obj.transform;
            }
            Debug.Log(distanceBetweenKrakenAndPlayerposition);
        }
    }

    /// <summary>
    /// クラーケンのターゲットになったプレイヤーの値を返す
    /// </summary>
    /// <returns>クラーケンのターゲットになったプレイヤーとの距離</returns>
    public Transform GetPlayerPositionTransform()
    {
        //return distanceBetweenKrakenAndPlayerposition;
        return playerTransform;
    }

}

/*
{1, 3, 5, 9} -> {9, 5. 3. 1}
1 < 3 -> {3, 1, 5, 9}
3 < 5 -> {5, 3, 1, 9}

*/
