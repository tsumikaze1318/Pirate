using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static object _lock = new object();

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    instance
                        = FindObjectOfType<GameManager>();
                    if (instance == null)
                    {
                        var singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<GameManager>();
                        singletonObject.name = nameof(GameManager) + "(singleton)";
                    }
                }

                return instance;
            }
        }
    }

    // シーン遷移マネージャー
    [SerializeField]
    private SceneFadeManager fadeManager;

    // 各プレイヤーの宝箱獲得数
    private int[] scores = { 0, 0, 0, 0 };

    // 宝箱獲得数のUI表示クラス
    [SerializeField, EnumIndex(typeof(CommonParam.UnitType))]
    private List<GameSystemManager> gameSystems = new List<GameSystemManager>();

    #region 外部参照関数

    /// <summary>
    /// プレイヤー番号に応じた宝箱獲得数を加算しUIに反映する関数
    /// </summary>
    /// <param name="plNum">プレイヤー番号</param>
    public void AddScore(int plNum)
    {
        // スコアを加算
        scores[plNum]++;
        // UIを更新
        gameSystems[plNum].Score = scores[plNum];
    }

    /// <summary>
    /// プレイヤー番号に応じた宝箱獲得数を減算しUIに反映する関数
    /// </summary>
    /// <param name="plNum">プレイヤー番号</param>
    public void SubScore(int plNum)
    {
        // スコアを減算
        scores[plNum]--;
        // UIを更新
        gameSystems[plNum].Score = scores[plNum];
    }

    #endregion
}
