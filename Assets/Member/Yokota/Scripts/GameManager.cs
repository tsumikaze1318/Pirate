using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Linq;

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
    // カメラ切り替えフラグ
    private bool cameraChanged = false;
    public bool CameraChanged => cameraChanged;
    // 残り50秒フラグ
    private bool fiftySecondsLeft = false;
    public bool FiftySecondsLeft => fiftySecondsLeft;
    // 各プレイヤーの宝箱獲得数
    [SerializeField] private int[] scores = { 0, 0, 0, 0 };
    // 近藤追記
    public int[] Scores => scores;
    // プレイヤー番号からスコアを引くディクショナリ
    private static Dictionary<int, int> playerNumToScore 
        = new Dictionary<int, int>();
    public static Dictionary<int, int> PlayerNumToScore => playerNumToScore;
    [SerializeField, Header("プレイヤーのプレハブ")]
    private List<GameObject> playerPrefab = new List<GameObject>();
    // ゲーム上に表示されているプレイヤーを格納するList
    private List<GameObject> players = new List<GameObject>();

    public List<GameObject> Players => players;
    [SerializeField, Header("参加可能人数")]
    private int attendance;
    public int Attendance => attendance;
    [SerializeField]
    private TreasureInstance treasureInstance;
    // カウントダウンを表示するスクリプトを格納するList
    private List<GameStartCountDown> gameStartCountDowns 
        = new List<GameStartCountDown>();
    // プレイヤーアサイン時に有効になっているカメラ
    [SerializeField]
    private List<Camera> attendCameras = new List<Camera>();
    // プレイヤーアサイン時に有効になっているキャンバス
    [SerializeField]
    private List<GameObject> attendCanvass = new List<GameObject>();
    // オープニング映像を流すゲームオブジェクト
    [SerializeField]
    private GameObject _videoPlayersObj;
    [SerializeField]
    private MovieController _movieController;
    [SerializeField]
    private GameObject[] _fillIconOjbects;
    private List<FillIcon> _fillIcons = new List<FillIcon>();

    private float _timeLimit = 99f;
    public float TimeLimit => _timeLimit;

    #endregion

    private void Start()
    {
        // プレイ人数に応じてリストにプレハブを追加
        for (int i = 0; i < attendance; i++)
        {
            playerPrefab.Add((GameObject)Resources.Load($"Prefab/Character_D{i + 1}"));
        }
        // Nullチェック
        _movieController ??= _videoPlayersObj.GetComponent<MovieController>();
        treasureInstance ??= FindObjectOfType<TreasureInstance>();

        foreach (var fillIcon in _fillIconOjbects)
        {
            _fillIcons.Add(fillIcon.GetComponentInChildren<FillIcon>(true));
        }
    }

    private void Update()
    {
        if (!gameStart) return;
        if (SceneFadeManager.IsFade) return;

        if (gameStart && !gameEnd)
        {
            _timeLimit -= Time.deltaTime;
        }
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
    }

    /// <summary>
    /// メインゲーム終了時にリザルトシーンへ遷移する関数
    /// </summary>
    public async void GameEnded() 
    {
        // ゲーム終了フラグを上げる
        gameEnd = true;
        // 3秒遅延
        await Task.Delay(3000);
        // ディクショナリをクリアする
        playerNumToScore.Clear();
        // ランキングソート
        RankingSort();
        // リザルトシーンへ遷移
        SceneFadeManager.Instance.FadeStart(SceneNameClass.SceneName.Result, BGMType.BGM1);
    }

    /// <summary>
    /// すべてのプレイヤーの参加が確認されたとき、カウントダウンを始める関数
    /// </summary>
    public void PlayersReady()
    {
        // シーンが切り替わった時に呼び出す関数を登録
        //SceneFadeManager.Instance.RegisterAction_Assign(null ,null, MovieStart, MovieSet);
        // 画面を切り替える
        SceneFadeManager.Instance.FadeStart(SceneNameClass.SceneName.Null
            , BGMType.Null
            , () =>
            {
                MovieSet();
            }
            , () =>
            {
                MovieStart();
            });
        // BGMは変えない
        SoundManager.Instance.PlayBgm(BGMType.Null);
    }

    /// <summary>
    /// オープニングムービーが終了したときに呼ばれる関数
    /// </summary>
    public void FinishMovie()
    {
        // シーンが切り替わった時に呼び出す関数を登録
        //SceneFadeManager.Instance.RegisterAction_Assign(ChangeCamera ,GameStartCount, null, null);
        // 画面を切り替える
        SceneFadeManager.Instance.FadeStart(SceneNameClass.SceneName.Null
            , BGMType.Null
            , () =>
            {
                ChangeCamera();
                HideSkipIcon();
            }
            , () =>
            {
                GameStartCount();
            });
        // BGM変更
        SoundManager.Instance.PlayBgm(BGMType.BGM2);
        // 映像を止める
        _movieController.MovieEnd();
    }

    /// <summary>
    /// プレイヤーのオブジェクトをListに格納する関数
    /// </summary>
    /// <param name="player">Listに格納するプレイヤーのGameObject</param>
    public void AddPlayer(GameObject player)
    {
        players.Add(player);
    }

    /// <summary>
    /// フェード中にプレイヤー準備が終わったときに
    /// 進行不能にならないようにする再帰関数
    /// </summary>
    public async void StandbyPlayersReady()
    {
        // フェード中なら
        if (SceneFadeManager.IsFade)
        {
            // 遅延を入れて
            await Task.Delay(100);
            // もう一度この関数を呼び出す
            StandbyPlayersReady();
            return;
        }
        else
        {
            PlayersReady();
        }
    }

    /// <summary>
    /// ゲームスタートフラグを上げる関数
    /// </summary>
    public void SetGameStart() { gameStart = true; }

    /// <summary>
    /// クラーケン発動関数
    /// </summary>
    public void InvokeKraken() { treasureInstance.SetClimax(true); }

    public void SetIconFill(int num, float percentage)
    {
        if (!_fillIconOjbects[0].activeSelf) return;

        foreach (var fillIcon in _fillIcons)
        {
            fillIcon.SetIconFillPercentage(num, percentage);
        }
    }

    #endregion

    /// <summary>
    /// ランキングソート関数
    /// </summary>
    private void RankingSort()
    {
        // プレイヤーの数だけ繰り返す
        for (int i = 0; i < players.Count; i++)
        {
            // Key : プレイヤー番号、Value : スコア、要素番号 : 順位
            playerNumToScore.Add(i + 1, scores[i]);
        }

        // Valueを降順でソート
        playerNumToScore = playerNumToScore
                        .OrderByDescending(x => x.Value)
                        .ToDictionary(x => x.Key, x => x.Value);
    }

    /// <summary>
    /// ゲームスタートカウントダウンを始める関数
    /// </summary>
    private void GameStartCount()
    {
        // プレイヤーの数だけ繰り返す
        for (int i = 0; i < players.Count; i++)
        {
            // プレイヤーの子オブジェクトにあるコンポーネントを取得
            var gameStartCountDown = players[i].GetComponentInChildren<GameStartCountDown>();
            // リストに追加する
            gameStartCountDowns.Add(gameStartCountDown);
            // カウントダウンをスタートする
            gameStartCountDown.StartCountDown(true);
        }
    }

    /// <summary>
    /// ゲーム終了カウントを始める関数
    /// </summary>
    public void GameEndCount()
    {
        // カウントダウンクラスのリストの数だけカウントダウンを始める
        for (int i = 0; i < gameStartCountDowns.Count; i++)
        {
            gameStartCountDowns[i].StartCountDown(false);
        }
    }

    /// <summary>
    /// カメラ切り替え関数
    /// </summary>
    private void ChangeCamera()
    {
        // カメラリストの数だけそのカメラを無効化する
        foreach (var camera in attendCameras)
        {
            camera.enabled = false;
        }
        // カメラ切り替えフラグを上げる
        cameraChanged = true;
    }

    /// <summary>
    /// オープニング映像を流す関数
    /// </summary>
    private void MovieStart()
    {
        _movieController.StartMovie();
    }

    /// <summary>
    /// ムービーを一瞬再生してゲーム画面に表示する関数
    /// </summary>
    private void MovieSet()
    {
        // ムービーを表示する
        _movieController.SetMovie();
        // キャンバスを無効化する
        foreach (var canvas in attendCanvass)
        {
            canvas.SetActive(false);
        }

        foreach (var icon in _fillIconOjbects)
        {
            icon.SetActive(true);
        }
    }

    private void HideSkipIcon()
    {
        foreach(var obj in _fillIconOjbects)
        {
            obj.SetActive(false);
        }
    }
}
