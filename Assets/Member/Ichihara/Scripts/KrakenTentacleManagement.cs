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

[RequireComponent(typeof(TargetPlayer))]
public class KrakenTentacleManagement : MonoBehaviour
{
    [SerializeField]
    private GameObject _krakenTentacle = null;
    [SerializeField]
    private List<TentacleAndShipPartsTable> _tentacleAndShipPartsTable = new List<TentacleAndShipPartsTable>();
    [SerializeField]
    private TargetPlayer _targetPlayer = null;
    [SerializeField]
    private GameObject _marker = null;

    [SerializeField]
    private float _krakenSpawnDuration = 30f;
    [SerializeField]
    private float _repairShipDuration = 5f;

    // Start is called before the first frame update
    async void Start()
    {
        _targetPlayer = GetComponent<TargetPlayer>();
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
                // クラーケンの足を出す座標を選定
                int randomNumber = Random.Range(0, _tentacleAndShipPartsTable.Count);
                TentacleAndShipPartsTable table = _tentacleAndShipPartsTable[randomNumber];
                KrakenTentacleAttack tentacle = table.TentacleSpawnPoint.GetChild(0).GetComponent<KrakenTentacleAttack>();

                tentacle.gameObject.SetActive(true);
                await Task.Yield();
                Transform playerTransform = _targetPlayer.GetPlayerPositionTransform();
                // 触手の攻撃処理を呼び出す
                await tentacle.AttackTentacle(playerTransform);
                // 船が破壊されたテクスチャを表示
                table.BreakShipParts.SetActive(false);
                // カウントダウンのリセット
                krakenSpawnCount = 0f;
                repairShipCount = 0f;
            }
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
}
