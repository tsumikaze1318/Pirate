using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummyTargetPlayer : MonoBehaviour
{
    private readonly string playerTag = "Player";
    // クラーケンの触手のオブジェクト
    private GameObject kraken = null;
    // 攻撃のターゲットになったプレイヤーの座標を取得する為
    private Transform playerTransform = null;
    // 触手とプレイヤーの距離を外部に渡す為
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

    /// <summary>
    /// 触手から一番近いプレイヤーの距離を計る
    /// </summary>
    private void SetPlayerPosition()
    {
        // プレイヤーオブジェクトをすべて検索
        var playerObjs = GameObject.FindGameObjectsWithTag(playerTag);
        Vector3 KrakenPosition = kraken.transform.position;
        // 常に一番近いプレイヤーの情報を更新、保持する
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
        }
    }

    /// <summary>
    /// クラーケンのターゲットになったプレイヤーの値を返す
    /// </summary>
    /// <returns>クラーケンのターゲットになったプレイヤーのTransform</returns>
    public Transform GetPlayerPositionTransform()
    {
        return playerTransform;
    }

}

/*
{1, 3, 5, 9} -> {9, 5. 3. 1}
1 < 3 -> {3, 1, 5, 9}
3 < 5 -> {5, 3, 1, 9}

*/
