using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Users;

public class PlayerAssign : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _playerList = new List<GameObject>();

    [SerializeField]
    List<Vector3> _spawnPos = new List<Vector3>();

    private Player[] _players;

    public static int _playerIndex;

    private int _playerNum;
    [SerializeField]
    private float _respwanTimer;
    private float _timer;

    [SerializeField]
    ParticleSystem _respawnPrefab;

    private GameObject _playerObj;
    // private List<PlayerInput> _playerInputs = new List<PlayerInput>();

    private Dictionary<int, GameObject> _numToPlayerObj = new Dictionary<int, GameObject>();



    void Start()
    {
        Assign();
        _players = GetComponentsInChildren<Player>();
    }




    /// <summary>
    /// 最初のプレイヤー出現処理
    /// </summary>
    void Assign()
    {
        for(int i = 0;i < GameManager.Instance.Attendance; i++)
        {
            _playerObj = Instantiate(_playerList[i], _spawnPos[i], Quaternion.identity, transform);
            Camera[] cameras = _playerObj.GetComponentsInChildren<Camera>(true);
            _numToPlayerObj.Add(i + 1, _playerObj);
            Debug.Log($"カメラの数{cameras.Length}");
            for(int j = 0; j < cameras.Length; j++)
            {
                cameras[j].rect = PlayerViewportRect(GameManager.Instance.Attendance - 1, i);
            }
            
        }

        // プレイヤーの数分繰り返す
        /*foreach (int key in DeviceManager.Instance.Gamepads.Keys)
        {
            // 指定した場所にプレイヤーを生成
            _playerObj = Instantiate(_playerList[key - 1], _spawnPos[key - 1], Quaternion.identity, transform);
            // 取得した Input を要素に追加
            // _playerInputs.Add(player.GetComponentInChildren<PlayerInput>());
            // プレイヤーとコントローラーの番号を紐づけ
            _numToPlayerObj.Add(key, _playerObj);
        }*/
    }
    
    private Rect PlayerViewportRect(int playerCount, int index)
    {
        switch (playerCount)
        {
            case 0: return new Rect(0, 0, 1, 1);
            case 1:
                return (index == 0) ? new Rect(0, 0.5f, 1, 0.5f) : new Rect(0, 0, 1, 0.5f);
            case 2:
                if (index == 0) return new Rect(0, 0.5f, 1, 0.5f);
                if (index == 1) return new Rect(0, 0, 0.5f, 0.5f);
                return new Rect(0.5f, 0, 0.5f, 0.5f);
            case 3:
                return new Rect((index % 2) * 0.5f, 0.5f - (index / 2) * 0.5f, 0.5f, 0.5f);
            default:
                return new Rect(0, 0, 1, 1);

        }
    }

    /// <summary>
    /// リスポーン処理
    /// </summary>
    /// <param name="num">プレイヤー番号</param>
    /// <param name="color">パーティクルの色</param>
    void Respawn(int num, Color color)
    {
        // プレイヤーの番号と同じスポーンポイントに移動
        _players[num].transform.position = _spawnPos[num];
        RespawnEffect(num,color);
        // プレイヤーの状態を Normal に指定
        _players[num]._state = CommonParam.UnitState.Normal;
        // リスポーン状態を解除
        _players[num]._respawn = false;
    }

    /// <summary>
    /// 再生が終わるまで待つ
    /// </summary>
    /// <param name="ps"></param>
    /// <returns></returns>
    IEnumerator EffectDestroy(ParticleSystem ps)
    {
        yield return new WaitForSeconds(1f);
        Destroy(ps.gameObject,ps.main.duration);
    }

    /// <summary>
    /// エフェクト出現処理
    /// </summary>
    /// <param name="num">パーティクルを表示するプレイヤーの番号</param>
    /// <param name="color">パーティクルの色</param>
    void RespawnEffect(int num,Color color)
    {
        // リスポーンアニメーションを流す
        _players[num]._animator.SetTrigger("Respawn");
        // プレイヤーのコライダーの Trigger を false に戻す
        _players[num]._playerCollider.isTrigger = false;
        // リスポーン場所にパーティクルを生成する
        ParticleSystem playerPs = Instantiate(_respawnPrefab, _spawnPos[num] + new Vector3(0, -1.5f, 0), Quaternion.identity);
        // パーティクルの色を全て変える
        foreach(ParticleSystem ps in playerPs.GetComponentsInChildren<ParticleSystem>())
        {
            var particleMain = ps.main;
            particleMain.startColor = color;
        }
        StartCoroutine(EffectDestroy(playerPs));
    }

    /// <summary>
    /// リスポーンさせる処理
    /// </summary>
    /// <param name="plObj">リスポーンさせるプレイヤーを指定</param>
    public async void SetRespawnPlayer(GameObject plObj)
    {
        // _respwanTimer 秒待つ
        await Task.Delay((int)_respwanTimer * 1000);

        // plObj と一致する Value を探し key を取得
        _playerNum = _numToPlayerObj.FirstOrDefault(x => x.Value == plObj).Key - 1;
        Debug.Log(_playerNum);

        // 取得した key を参照して生成する
        switch (_playerNum)
        {
            case 0:
                Respawn(_playerNum, Color.cyan);
                break;
            case 1:
                Respawn(_playerNum, Color.red);
                break;
            case 2:
                Respawn(_playerNum, Color.green);
                break;
            case 3:
                Respawn(_playerNum, Color.yellow);
                break;
        }
    }
}
