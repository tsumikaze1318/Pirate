using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;
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

[RequireComponent(typeof(DummyTargetPlayer))]
public class KrakenTentacleManagement : MonoBehaviour
{
    // 触手のゲームオブジェクト
    [SerializeField]
    private GameObject _krakenTentacle = null;
    // 触手が出現する座標によって、船が壊れる箇所が異なる為、座標と破損個所を関連付けた構造体を作成し、リスト化
    [SerializeField]
    private List<TentacleAndShipPartsTable> _tentacleAndShipPartsTable = new List<TentacleAndShipPartsTable>();
    [SerializeField]
    private DummyTargetPlayer _targetPlayer = null;
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
        _targetPlayer = GetComponent<DummyTargetPlayer>();
        for (int i = 0; i < _tentacleAndShipPartsTable.Count; i++)
        {
            var obj = Instantiate(_krakenTentacle, _tentacleAndShipPartsTable[i].TentacleSpawnPoint);
            obj.SetActive(false);
        }
        await UpdateTask();
    }

    private async Task UpdateTask()
    {
        float krakenSpawnCount = 0f;
        float repairShipCount = 0f;
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
                    randomNumbers[0] = Random.Range(0, _tentacleAndShipPartsTable.Count);
                    randomNumbers[1] = Random.Range(0, _tentacleAndShipPartsTable.Count);
                } while (randomNumbers[0] == randomNumbers[_krakenSpawnCount - 1]);
                // 触手が出現、攻撃
                for (int i = 0; i < _krakenSpawnCount; i++)
                {
                    await Task.Yield();
                    tables[i] = _tentacleAndShipPartsTable[randomNumbers[i]];
                    tentacles[i] = tables[i].TentacleSpawnPoint.GetChild(0).GetComponent<DummyKrakenTentacleAttack>();
                }

                await Task.WhenAll(PreparationForAttackTentacle(tentacles[0], tables[0])
                                 , PreparationForAttackTentacle(tentacles[_krakenSpawnCount - 1], tables[_krakenSpawnCount - 1]));

                // カウントダウンのリセット
                krakenSpawnCount = 0f;
                repairShipCount = 0f;
            }
            // 船が修復される
            if (repairShipCount > _repairShipDuration)
            {
                foreach (TentacleAndShipPartsTable table in _tentacleAndShipPartsTable)
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

    private async Task PreparationForAttackTentacle(DummyKrakenTentacleAttack dummyKrakenTentacleAttack
                                                   , TentacleAndShipPartsTable table)
    {
        dummyKrakenTentacleAttack.gameObject.SetActive(true);
        Transform playerTransform = _targetPlayer.GetPlayerPositionTransform();
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
