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
            if (instance == null)
            {
                instance
                    = FindObjectOfType<GameManager>();
                //if (instance == null)
                //{
                //    var singletonObject = new GameObject();
                //    instance = singletonObject.AddComponent<GameManager>();
                //    singletonObject.name = nameof(GameManager) + "(singleton)";
                //}
            }

            return instance;
        }
    }

    // メインゲームが始まったことを伝えるフラグ
    private bool gameStart = false;
    public bool GameStart => gameStart;

    // メインゲームが終了したことを伝えるフラグ
    private bool gameEnd = false;
    public bool GameEnd => gameEnd;

    [SerializeField]
    // 各プレイヤーの宝箱獲得数
    private int[] scores = { 0, 0, 0, 0 };
    // 近藤追記
    public int[] Scores => scores;

    private static int[] scoreRanking;
    public static int[] ScoreRanking => scoreRanking;

    private static Dictionary<int, List<GameObject>> scoreToPlayer 
        = new Dictionary<int, List<GameObject>>();
    public static Dictionary<int, List<GameObject>> ScoreToPlayer => scoreToPlayer;

    [SerializeField, Header("プレイヤーのプレハブ")]
    private List<GameObject> playerPrefab = new List<GameObject>();

    // ゲーム上に表示されているプレイヤーを格納するList
    private List<GameObject> players = new List<GameObject>();

    public List<GameObject> Players => players;

    [SerializeField, Header("参加可能人数")]
    private int attendance;

    private int isActivePlayer;

    // カウントダウンを表示するスクリプトを格納するList
    private List<GameStartCountDown> gameStartCountDowns 
        = new List<GameStartCountDown>();

    // 宝箱獲得数のUI表示クラス
    //[SerializeField, EnumIndex(typeof(CommonParam.UnitType))]
    //private List<GameSystemManager> gameSystems = new List<GameSystemManager>();

    private void Start()
    {
        for (int i = 0; i < attendance; i++)
        {
            playerPrefab.Add((GameObject)Resources.Load($"Prefab/Yokota/PlayerModel{i + 1}"));
        }
    }

    private void Update()
    {
        //if (Input.GetKeyUp(KeyCode.Escape)) { GameEnded(); }
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
        if (scores[plNum] < 0)
        {
            scores[plNum] = 0;
        }
        // UIを更新1
        //gameSystems[plNum].Score = scores[plNum];
    }

    /// <summary>
    /// メインゲーム終了時にリザルトシーンへ遷移する関数
    /// </summary>
    public async void GameEnded() 
    {
        gameEnd = true;

        await Task.Delay(3000);

        scoreRanking = RankingSort();

        SceneFadeManager.Instance.FadeStart(SceneNameClass.SceneName.Result, BGMType.BGM1);
    }

    /// <summary>
    /// すべてのプレイヤーの参加が確認されたとき、カウントダウンを始める関数
    /// </summary>
    public void PlayersReady()
    {
        for (int i = 0; i < players.Count; i++)
        {
            gameStartCountDowns.Add
                (players[i].GetComponentInChildren<GameStartCountDown>());
        }

        for (int i = 0; i < gameStartCountDowns.Count; i++)
        {
            gameStartCountDowns[i].CountStart();
        }
    }

    /// <summary>
    /// プレイヤーのオブジェクトをListに格納する関数
    /// </summary>
    /// <param name="player">Listに格納するプレイヤーのGameObject</param>
    public void AddPlayer(GameObject player)
    {
        players.Add(player);

        if (players.Count == attendance)
        {
            PlayersReady();
        }
    }

    public void SetGameStart() { gameStart = true; }

    #endregion

    private int[] RankingSort()
    {
        for (int i = 0; i < players.Count; i++)
        {
            List<GameObject> list = new List<GameObject>();
            list.Add(playerPrefab[i]);

            bool sameKey = false;

            foreach (var dic in scoreToPlayer)
            {
                if (scores[i] == dic.Key)
                {
                    scoreToPlayer[scores[i]].Add(playerPrefab[i]);
                    sameKey = true;
                }
            }

            if (!sameKey) scoreToPlayer.Add(scores[i], list);
        }

        int[] ranking = scores;

        Array.Sort(ranking);
        Array.Reverse(ranking);

        return ranking;
    }
}
