using System.Collections;
using System.Collections.Generic;
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
    //[SerializeField]
    //private GameObject _marker = null;

    [SerializeField, Header("同時に出現する触手の数")]
    private int _krakenSpawnCount = 2;
    [SerializeField, Header("触手が出現するインターバル")]
    private float _krakenSpawnDuration = 30f;
    [SerializeField, Header("船が修復するまでのインターバル")]
    private float _repairShipDuration = 5f;

    // Start is called before the first frame update
    async void Start()
    {
        for (int i = 0; i < _tentacleAndShipPartsTables.Count; i++)
        {
            var obj = Instantiate(_krakenTentacle, _tentacleAndShipPartsTables[i].TentacleSpawnPoint);
            obj.transform.LookAt(_tentacleAndShipPartsTables[i].BreakShipParts.transform);
            // 触手を海賊船の方向に向ける
            obj.transform.Rotate(Vector3.up);
            //obj.SetActive(false);
        }
        await UpdateTask();
    }

    private async Task UpdateTask()
    {
        float krakenSpawnCount = 0f, repairShipCount = 0f;
        TentacleAndShipPartsTable[] tables = new TentacleAndShipPartsTable[_krakenSpawnCount];
        DummyKrakenTentacleAttack[] tentacles = new DummyKrakenTentacleAttack[_krakenSpawnCount];
        int[] randomNumbers = new int[_krakenSpawnCount];
        do
        {
            await Task.Yield();
            // ゲームが始まらない限り、後の処理が走らないようにする
            if (GameManager.Instance.GameStart == false)
                continue;

            krakenSpawnCount += Time.deltaTime;
            repairShipCount += Time.deltaTime;
            if (krakenSpawnCount >= _krakenSpawnDuration)
            {
                // クラーケンの触手を出す座標を2ヵ所選定
                // 触手の出現座標が被らないようにする
                do
                {
                    randomNumbers[0] = Random.Range(0, _tentacleAndShipPartsTables.Count);
                    randomNumbers[1] = Random.Range(0, _tentacleAndShipPartsTables.Count);
                } while (randomNumbers[0] == randomNumbers[_krakenSpawnCount - 1]);
                // 触手を出現させる
                for (int i = 0; i < _krakenSpawnCount; i++)
                {
                    tables[i] = _tentacleAndShipPartsTables[randomNumbers[i]];
                    tentacles[i] = tables[i].TentacleSpawnPoint.GetChild(0).GetComponent<DummyKrakenTentacleAttack>();
                }

                // カウントダウンのリセット
                krakenSpawnCount = 0f;
                repairShipCount = 0f;
                // 全ての触手が攻撃を終えるまで待機
                await Task.WhenAll(PreparationForAttackTentacle(tentacles[0], tables[0])
                                 , PreparationForAttackTentacle(tentacles[_krakenSpawnCount - 1], tables[_krakenSpawnCount - 1]));
            }
            // 船が修復される
            if (repairShipCount > _repairShipDuration)
            {
                foreach (TentacleAndShipPartsTable table in _tentacleAndShipPartsTables)
                {
                    if (table.BreakShipParts.activeSelf == false)
                    {
                        table.BreakShipParts.SetActive(true);
                    }
                }
            }
        }
        while (GameManager.Instance.GameEnd == false);
    }

    /// <summary>
    /// クラーケンの触手の攻撃処理を呼び出す関数
    /// </summary>
    /// <param name="dummyKrakenTentacleAttack">攻撃する触手にアタッチされているスクリプト</param>
    /// <param name="table">クラーケンに破壊される船のパーツ</param>
    /// <returns></returns>
    private async Task PreparationForAttackTentacle(DummyKrakenTentacleAttack dummyKrakenTentacleAttack
                                                   , TentacleAndShipPartsTable table)
    {
        GameObject tentacle = dummyKrakenTentacleAttack.gameObject;
        //tentacle.SetActive(true);
        Transform playerTransform = tentacle.GetComponent<DummyTargetPlayer>().GetPlayerPositionTransform();
        // 選出されたクラーケンの触手と一番近いプレイヤーの位置を計算する
        float distance = Vector3.Distance(table.TentacleSpawnPoint.position, playerTransform.position);
        Debug.Log(distance);
        // クラーケンが監督している箇所にプレイヤーがいない場合は攻撃しない;
        if (distance > 35f) return;
        // 触手の攻撃処理を呼び出す
        await dummyKrakenTentacleAttack.AttackTentacle(playerTransform);
        // 船が破壊されたテクスチャを表示
        table.BreakShipParts.SetActive(false);
    }

    /*
    /// <summary>
    /// 触手を振り下ろす座標にマーカーUIを表示
    /// </summary>
    /// <returns></returns>
    public GameObject GenerateMarker(Transform transform)
    {
        // 攻撃マーカーのUIが地面に埋まってしまう為、マーカーオブジェクトの大きさの半分の値分、正y軸方向に上げる
        GameObject markerObject = Instantiate(_marker
                                            , transform.position + (Vector3.up * _marker.transform.localScale.y / 2)
                                            , Quaternion.identity);
        return markerObject;
    }
    */
}
