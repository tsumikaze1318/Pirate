﻿using UnityEngine;

public class TreasureModel : MonoBehaviour
{
    // この宝箱の生成区画
    public TreasurePlace Place;

    private TreasureInstance treasureInstance;

    private void Start()
    {
        // 親オブジェクトから宝箱生成クラスを取得
        treasureInstance = GetComponentInParent<TreasureInstance>();
    }

    /// <summary>
    /// 宝箱を獲得した時に呼ぶ関数
    /// </summary>
    /// <param name="plNum">宝箱を獲得したプレイヤー番号</param>
    public void GetTreasure(int plNum)
    {
        // この宝箱が船の先端に生成されていたとき
        if (Place == TreasurePlace.Sprit) GameManager.Instance.AddScore(plNum, true);
        else GameManager.Instance.AddScore(plNum, false);

        
        // 宝箱生成関数を呼び出す
        treasureInstance.GenerateTreasure(Place);
    }
}

/// <summary>
/// 宝箱の生成区画
/// Main    : 船中央
/// Fore    : 船首
/// Mizzen  : 船尾
/// Sprit   : 船先端
/// Null    : null
/// </summary>
public enum TreasurePlace
{
    Main,
    Fore,
    Mizzen,
    Sprit,
    Null
}
