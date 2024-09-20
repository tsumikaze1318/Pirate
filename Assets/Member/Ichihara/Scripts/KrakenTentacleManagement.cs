using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

/// <summary>
/// クラーケンの触手が出てくる座標と船が壊れる
/// </summary>
[System.Serializable]
public struct TentacleAndShipPartsTable
{
    public Transform TentacleSpawnPoint;
    public GameObject BreakShipParts;
}

public class KrakenTentacleManagement : SingletonMonoBehaviour<KrakenTentacleManagement>
{
    // 触手のゲームオブジェクト
    [SerializeField]
    private GameObject _krakenTentacle = null;
    // 触手が出現する座標によって、船が壊れる箇所が異なる為、座標と破損個所を関連付けた構造体を作成し、リスト化
    [SerializeField]
    private List<TentacleAndShipPartsTable> _tentacleAndShipPartsTables = new List<TentacleAndShipPartsTable>();

    private List<DummyKrakenTentacleAttack> _dummyKrakenTentacleAttacks = new List<DummyKrakenTentacleAttack>();

    [SerializeField, Header("同時に出現する触手の数")]
    private int _krakenSpawnCount = 2;
    [SerializeField, Header("触手が攻撃するまでのインターバル")]
    private float _krakenAttackDuration = 30f;
    [SerializeField, Header("船が修復するまでのインターバル")]
    private float _repairShipDuration = 5f;
    [SerializeField, Range(0f, 50f), Header("クラーケンの攻撃範囲の上限、下限")]
    private float _krakenAttackMaxRagge = 35f, _krakenAttackMinRange = 30f;
    [SerializeField, Header("触手が出現する残り時間")]
    private float _tentacleSpawnCount = 50f;
    [SerializeField, Header("クラーケンが海中から出現する時間")]
    private float _krakenAwakeTime = 5f;
    [SerializeField, Header("クラーケンの初期位置\n（親オブジェクトからY軸がどれだけ離れているか）（海から上がってくる表現）")]
    private float _krakenStartPoint = 5f;

    private void Awake()
    {
        base.Awake();
        for (int i = 0; i < _tentacleAndShipPartsTables.Count; i++)
        {
            var spawnPoint = _tentacleAndShipPartsTables[i].TentacleSpawnPoint;
            var obj = Instantiate(_krakenTentacle, spawnPoint);
            obj.transform.position += Vector3.down * _krakenStartPoint;
            // 触手が生成される親オブジェクトの名前で分類する
            string name = spawnPoint.name;
            if (name.Contains("Left") == true)
                // 触手を海賊船の方向に向ける
                obj.transform.eulerAngles = Vector3.up * -90f;
            if (name.Contains("Right") == true)
                // 触手を海賊船の方向に向ける
                obj.transform.eulerAngles = Vector3.up * 90f;
            _dummyKrakenTentacleAttacks.Add(obj.GetComponent<DummyKrakenTentacleAttack>());
            obj.SetActive(false);
        }
    }

    // Start is called before the first frame update
    async void Start()
    {
        CancellationTokenSource cts = new CancellationTokenSource();
        await UpdateKrakenTask(cts);
    }

    /// <summary>
    /// クラーケン全般の処理を監督、実行する
    /// </summary>
    /// <param name="cts"></param>
    /// <returns></returns>
    private async Task UpdateKrakenTask(CancellationTokenSource cts)
    {
        bool doOnce = false;
        // クラーケンの出現、攻撃間隔　// 船が修復されるまでのインターバル
        float krakenSpawnCountDown = 0f, repairShipCountDown = 0f;
        TentacleAndShipPartsTable[] tables = new TentacleAndShipPartsTable[_krakenSpawnCount];
        DummyKrakenTentacleAttack[] tentacles = new DummyKrakenTentacleAttack[_krakenSpawnCount];
        // クラーケンの触手の出現箇所をランダムに決め、その値を格納する
        int[] randomNumbers = new int[_krakenSpawnCount];
        TimeCount timeCount = null;
        do
        {
            await Task.Yield();
            // ゲームが始まらない限り、後の処理が走らないようにする
            if (GameManager.Instance.GameStart == false) continue;
            if (timeCount == null)
            {
                var prefab = GameManager.Instance.Players[0];
                timeCount = prefab.GetComponentInChildren<TimeCount>();
            }
            if (timeCount == null) continue;
            // 残り時間が50秒を切ったらクラーケンを出現、攻撃させる
            if (timeCount.Timer < _tentacleSpawnCount + _krakenAwakeTime && doOnce == false)
            {
                foreach (var table in _tentacleAndShipPartsTables)
                {
                    var tentacle = table.TentacleSpawnPoint.GetChild(0);
                    Animator animator = tentacle.GetComponent<Animator>();
                    tentacle.gameObject.SetActive(true);
                    // 触手のアニメーション再生にばらつきを持たせる
                    float randomPlayTime = Random.Range(0f, 1f);
                    Debug.Log(animator.GetCurrentAnimatorStateInfo(0));
                    animator.Play(animator.GetCurrentAnimatorStateInfo(0).shortNameHash, 0, randomPlayTime);
                }
                // クラーケンの触手が海から出てくる演出
                await Task.WhenAll(AwakeKrakenTentacle(_dummyKrakenTentacleAttacks[0])
                                 , AwakeKrakenTentacle(_dummyKrakenTentacleAttacks[1])
                                 , AwakeKrakenTentacle(_dummyKrakenTentacleAttacks[2])
                                 , AwakeKrakenTentacle(_dummyKrakenTentacleAttacks[3])
                                 , AwakeKrakenTentacle(_dummyKrakenTentacleAttacks[4])
                                 , AwakeKrakenTentacle(_dummyKrakenTentacleAttacks[5]));

                doOnce = true;
            }
            else if (timeCount.Timer > _tentacleSpawnCount)
                continue;

            krakenSpawnCountDown += Time.deltaTime;
            repairShipCountDown += Time.deltaTime;
            if (krakenSpawnCountDown >= _krakenAttackDuration)
            {
                // クラーケンの触手を出す座標を2ヵ所選定
                // 攻撃する触手が被らないようにする
                do
                {
                    randomNumbers[0] = Random.Range(0, _tentacleAndShipPartsTables.Count);
                    randomNumbers[1] = Random.Range(0, _tentacleAndShipPartsTables.Count);
                } while (randomNumbers[0] == randomNumbers[_krakenSpawnCount - 1]);
                for (int i = 0; i < _krakenSpawnCount; i++)
                {
                    tables[i] = _tentacleAndShipPartsTables[randomNumbers[i]];
                    tentacles[i] = tables[i].TentacleSpawnPoint.GetChild(0).GetComponent<DummyKrakenTentacleAttack>();
                }

                // カウントダウンのリセット
                krakenSpawnCountDown = 0f;
                repairShipCountDown = 0f;
                // 全ての触手が攻撃を終えるまで待機
                await Task.WhenAll(PreparationForAttackTentacle(tentacles[0], tables[0], cts)
                                 , PreparationForAttackTentacle(tentacles[_krakenSpawnCount - 1], tables[_krakenSpawnCount - 1], cts));
            }

            // 船が修復される
            if (repairShipCountDown > _repairShipDuration)
            {
                foreach (TentacleAndShipPartsTable table in _tentacleAndShipPartsTables)
                {
                    if (table.BreakShipParts?.activeSelf == false)
                        table.BreakShipParts?.SetActive(true);
                }
            }
        }
        while (GameManager.Instance.GameEnd == false);
    }

    /// <summary>
    /// クラーケンの触手が海から出現する処理
    /// </summary>
    /// <param name="dummyKrakenTentacleAttack">出現させる触手</param>
    /// <returns></returns>
    private async Task AwakeKrakenTentacle(DummyKrakenTentacleAttack dummyKrakenTentacleAttack)
    {
        var krakenTransform = dummyKrakenTentacleAttack.transform;
        var spawnPoint = dummyKrakenTentacleAttack.transform.parent;
        float awakeTime = _krakenStartPoint;
        do
        {
            await Task.Yield();
            // クラーケンの触手が海中から出現
            krakenTransform.position += Vector3.up * awakeTime / _krakenAwakeTime * Time.deltaTime;
        } while (krakenTransform.position.y <= spawnPoint.position.y);
    }

    /// <summary>
    /// クラーケンの触手の攻撃処理を呼び出す関数
    /// </summary>
    /// <param name="dummyKrakenTentacleAttack">攻撃する触手にアタッチされているスクリプト</param>
    /// <param name="table">クラーケンに破壊される船のパーツ</param>
    /// <param name="cts"></param>
    /// <returns></returns>
    private async Task PreparationForAttackTentacle(DummyKrakenTentacleAttack dummyKrakenTentacleAttack
                                                   , TentacleAndShipPartsTable table
                                                   , CancellationTokenSource cts)
    {
        GameObject tentacle = dummyKrakenTentacleAttack.gameObject;
        Transform playerTransform = tentacle.GetComponent<DummyTargetPlayer>().GetPlayerPositionTransform();
        // 選出されたクラーケンの触手と一番近いプレイヤーの位置を計算する
        float distance = Vector3.Distance(table.TentacleSpawnPoint.position, playerTransform.position);
        // クラーケンが監督している箇所にプレイヤーがいない場合は攻撃しない;
        if (distance > _krakenAttackMaxRagge || distance < _krakenAttackMinRange) return;
        // 触手の攻撃処理を呼び出す
        await dummyKrakenTentacleAttack.AttackTentacle(playerTransform, cts);
        // 船が破壊されたテクスチャを表示
        table.BreakShipParts.SetActive(false);
    }
}
