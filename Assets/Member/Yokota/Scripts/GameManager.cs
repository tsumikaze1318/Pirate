using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
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

    private bool gameEnded = false;

    // シーン遷移マネージャー
    [SerializeField]
    private SceneFadeManager fadeManager;

    [SerializeField]
    // 各プレイヤーの宝箱獲得数
    private int[] scores = { 0, 0, 0, 0 };
    // 近藤追記
    public int[] Scores => scores;

    [SerializeField]
    private static int[] scoreRanking;
    public static int[] ScoreRanking => scoreRanking;

    private static Dictionary<int, GameObject> scoreToPlayer = new Dictionary<int, GameObject>();
    public static Dictionary<int, GameObject> ScoreToPlayer => scoreToPlayer;

    [SerializeField]
    private List<GameObject> playerObjects;

    // 宝箱獲得数のUI表示クラス
    //[SerializeField, EnumIndex(typeof(CommonParam.UnitType))]
    //private List<GameSystemManager> gameSystems = new List<GameSystemManager>();

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            playerObjects.Add((GameObject)Resources.Load($"Prefab/Yokota/Player{i + 1}"));
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SetGameEnded();
        }

        if (gameEnded) GameEnded();
    }

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
        //gameSystems[plNum].Score = scores[plNum];
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
        //gameSystems[plNum].Score = scores[plNum];
    }

    public async void GameEnded() 
    {
        gameEnded = false;

        scoreRanking = RankingSort();

        await Task.Delay(3000);

        fadeManager.FadeOut(SceneNameClass.SceneName.Result);
    }

    public void SetGameEnded() { gameEnded = true; }

    #endregion

    private int[] RankingSort()
    {
        //for (int i = 0; i < scores.Length; i++)
        //{
        //    ScoreToPlayer.Add(scores[i], playerObjects[i]);
        //}
        
        for (int i = 0; i < 1; i++)
        {
            ScoreToPlayer.Add(scores[i], playerObjects[i]);
        }

        int[] ranking = scores;

        Array.Sort(ranking);
        Array.Reverse(ranking);

        return ranking;
    }
}
