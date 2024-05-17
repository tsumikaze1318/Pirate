using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

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

    private bool gameStart = false;
    public bool GameStart => gameStart;

    private bool gameEnd = false;
    public bool GameEnd => gameEnd;

    private bool ready4Player = false;

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
    private List<GameObject> playerModels;

    private List<GameObject> players;

    [SerializeField]
    private PlayerInputManager playerInputManager;

    [SerializeField]
    private int attendance;

    [SerializeField]
    private List<GameStartCountDown> gameStartCountDowns = new List<GameStartCountDown>();

    // 宝箱獲得数のUI表示クラス
    //[SerializeField, EnumIndex(typeof(CommonParam.UnitType))]
    //private List<GameSystemManager> gameSystems = new List<GameSystemManager>();

    private void Start()
    {
        for (int i = 0; i < 4; i++)
        {
            playerModels.Add((GameObject)Resources.Load($"Prefab/Yokota/Player{i + 1}"));
        }
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            SetGameEnd();
        }

        if (playerInputManager.playerCount == attendance && !ready4Player)
        {
            ready4Player = true;
        }

        if (gameEnd) GameEnded();
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
        gameEnd = false;

        scoreRanking = RankingSort();

        await Task.Delay(3000);

        SceneFadeManager.Instance.FadeStart(SceneNameClass.SceneName.Result);
    }

    public void AddPlayer(GameObject player)
    {
        players.Add(player);
    }

    public void SetGameStart() { gameStart = true; }
    public void SetGameEnd() { gameEnd = true; }

    #endregion

    private int[] RankingSort()
    {
        //for (int i = 0; i < scores.Length; i++)
        //{
        //    ScoreToPlayer.Add(scores[i], playerObjects[i]);
        //}
        
        for (int i = 0; i < 1; i++)
        {
            ScoreToPlayer.Add(scores[i], playerModels[i]);
        }

        int[] ranking = scores;

        Array.Sort(ranking);
        Array.Reverse(ranking);

        return ranking;
    }
}
