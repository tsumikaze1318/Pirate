using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    #region 宣言

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

    private bool cameraChanged = false;
    public bool CameraChanged => cameraChanged;

    private bool fiftySecondsLeft = false;
    public bool FiftySecondsLeft => fiftySecondsLeft;

    // 各プレイヤーの宝箱獲得数
    [SerializeField] private int[] scores = { 0, 0, 0, 0 };
    // 近藤追記
    public int[] Scores => scores;

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

    [SerializeField]
    private TreasureInstance treasureInstance;

    // カウントダウンを表示するスクリプトを格納するList
    private List<GameStartCountDown> gameStartCountDowns 
        = new List<GameStartCountDown>();

    [SerializeField]
    private List<Camera> attendCameras = new List<Camera>();

    [SerializeField]
    private List<Canvas> attendCanvass = new List<Canvas>();

    [SerializeField]
    private GameObject _videoPlayersObj;
    [SerializeField]
    private MovieController _movieController;

    // 宝箱獲得数のUI表示クラス
    //[SerializeField, EnumIndex(typeof(CommonParam.UnitType))]
    //private List<GameSystemManager> gameSystems = new List<GameSystemManager>();

    #endregion

    private void Start()
    {
        for (int i = 0; i < attendance; i++)
        {
            playerPrefab.Add((GameObject)Resources.Load($"Prefab/Character_D{i + 1}"));
        }
        

        _movieController ??= _videoPlayersObj.GetComponent<MovieController>();

        treasureInstance ??= FindObjectOfType<TreasureInstance>();
    }

    private void Update()
    {
        if (gameStart) return;
        if (SceneFadeManager.IsFade) return;
        if (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.JoystickButton1) && Input.GetKeyDown(KeyCode.JoystickButton2)) FinishMovie();

    }

    #region 外部参照関数

    /// <summary>
    /// プレイヤー番号に応じた宝箱獲得数を加算しUIに反映する関数
    /// </summary>
    /// <param name="plNum">プレイヤー番号</param>
    public void AddScore(int plNum, bool precious)
    {
        // スコアを加算
        if (precious) scores[plNum] += 3;
        else scores[plNum]++;
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

        scoreToPlayer.Clear();

        RankingSort();

        SceneFadeManager.Instance.FadeStart(SceneNameClass.SceneName.Result, BGMType.BGM1);
    }

    /// <summary>
    /// すべてのプレイヤーの参加が確認されたとき、カウントダウンを始める関数
    /// </summary>
    public void PlayersReady()
    {
        SceneFadeManager.Instance.RegisterAction_Assign(null ,null, MovieStart, MovieSet);
        SceneFadeManager.Instance.FadeStart(SceneNameClass.SceneName.Null, BGMType.Null);
        SoundManager.Instance.PlayBgm(BGMType.Null);
    }

    /// <summary>
    /// オープニングムービーが終了したときに呼ばれる関数
    /// </summary>
    public void FinishMovie()
    {
        SceneFadeManager.Instance.RegisterAction_Assign(ChangeCamera ,CountStart, null, null);
        SceneFadeManager.Instance.FadeStart(SceneNameClass.SceneName.Null, BGMType.Null);
        SoundManager.Instance.PlayBgm(BGMType.BGM2);
        _movieController.MovieEnd();
    }

    /// <summary>
    /// プレイヤーのオブジェクトをListに格納する関数
    /// </summary>
    /// <param name="player">Listに格納するプレイヤーのGameObject</param>
    public void AddPlayer(GameObject player)
    {
        players.Add(player);
        SoundManager.Instance.PlaySe(SEType.SE1);

        if (players.Count == attendance)
        {
            StandbyPlayersReady();
        }
    }

    private async void StandbyPlayersReady()
    {
        if (SceneFadeManager.IsFade)
        {
            await Task.Delay(100);
            StandbyPlayersReady();
            return;
        }
        else
        {
            PlayersReady();
        }
    }

    

    public void SetGameStart() { gameStart = true; }

    public void InvokeKraken()
    {
        Debug.Log("!!!CLIMAX!!!");
        treasureInstance.SetClimax(true);
    }

    #endregion

    private void RankingSort()
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
    }

    private void CountStart()
    {
        for (int i = 0; i < players.Count; i++)
        {
            gameStartCountDowns.Add
                (players[i].GetComponentInChildren<GameStartCountDown>());
        }

        for (int i = 0; i < gameStartCountDowns.Count; i++)
        {
            gameStartCountDowns[i].CountDown();
        }
    }

    private void ChangeCamera()
    {
        foreach (var camera in attendCameras)
        {
            camera.enabled = false;
        }

        cameraChanged = true;
    }

    private void MovieStart()
    {
        _movieController.StartMovie();
    }

    private void MovieSet()
    {
        _movieController.SetMovie();

        foreach (var canvas in attendCanvass)
        {
            canvas.enabled = false;
        }
    }
}
