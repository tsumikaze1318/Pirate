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

    PlayerInput _playerInput;

    Player _player;

    private Player[] _players;

    public static int _playerIndex;

    private int _playerNum;
    [SerializeField]
    private float _respwanTimer;
    private float _timer;

    [SerializeField]
    ParticleSystem _respawnPrefab;

    private List<PlayerInput> _playerInputs = new List<PlayerInput>();

    private Dictionary<int, GameObject> _numToPlayerObj = new Dictionary<int, GameObject>();


    void Start()
    {
        Assign();
        _player = GetComponentInChildren<Player>();

        _players = GetComponentsInChildren<Player>();
    }



    void Assign()
    {
        foreach (int key in DeviceManager.Instance.Gamepads.Keys)
        {
            var player = Instantiate(_playerList[key - 1], _spawnPos[key - 1], Quaternion.identity, transform);
            GameManager.Instance.AddPlayer(player);
            _playerInputs.Add(player.GetComponentInChildren<PlayerInput>());
            _numToPlayerObj.Add(key, player);
            Debug.Log(_numToPlayerObj.Count);
        }
    }

   

    private void Update()
    {
        if (!GameManager.Instance.GameStart) return;

        //if (_player._respawn)
        //{
        //    _timer += Time.deltaTime;
        //    if (_respwanTimer >= _timer) return;
        //    switch ( _playerIndex)
        //    {
        //        case 0:
        //            Respawn(_playerIndex, Color.cyan);
        //            break;
        //        case 1:
        //            Respawn( _playerIndex, Color.red);
        //            break;
        //        case 2:
        //            Respawn(_playerIndex, Color.green);
        //            break;
        //        case 3:
        //            Respawn(_playerIndex, Color.yellow);
        //            break;
        //    }
        //    _timer = 0;
        //}
        
    }
    
    void Respawn(int num, Color color)
    {
        _players[num].transform.position = _spawnPos[num];
        RespawnEffect(num,color);
        _players[num]._state = CommonParam.UnitState.Normal;
        _players[num]._respawn = false;
    }

    IEnumerator EffectDestroy(ParticleSystem ps)
    {
        yield return new WaitForSeconds(1f);
        Destroy(ps.gameObject,ps.main.duration);
    }

    void RespawnEffect(int num,Color color)
    {
        _player._animator.SetTrigger("Respawn");
        ParticleSystem playerPs = Instantiate(_respawnPrefab, _spawnPos[num] + new Vector3(0, -1.5f, 0), Quaternion.identity);
        foreach(ParticleSystem ps in playerPs.GetComponentsInChildren<ParticleSystem>())
        {
            var particleMain = ps.main;
            particleMain.startColor = color;
        }
        StartCoroutine(EffectDestroy(playerPs));
    }

    public async void SetRespawnPlayer(GameObject plObj)
    {
        await Task.Delay((int)_respwanTimer * 1000);

        _playerNum = _numToPlayerObj.FirstOrDefault(x => x.Value == plObj).Key - 1;

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
