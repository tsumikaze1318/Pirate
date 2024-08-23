using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class PlayerAssign : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _playerList = new List<GameObject>();

    [SerializeField]
    List<Vector3> _spawnPos = new List<Vector3>();

    PlayerInput _playerInput;

    Player _player;

    public static int _playerIndex;
    [SerializeField]
    private float _respwanTimer;
    private float _timer;

    [SerializeField]
    ParticleSystem _respawnPrefab;


    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerIndex = this._playerInput.user.index;
        Assign();
        _player = GetComponentInChildren<Player>();
        GameManager.Instance.AddPlayer(gameObject);
    }

    void Assign()
    {
        switch (_playerIndex)
        {
            case 0:
                Instantiate(_playerList[_playerIndex], _spawnPos[_playerIndex], Quaternion.identity, transform);
                ((DualShockGamepad)DualShock4GamepadHID.all[_playerIndex]).SetLightBarColor(Color.cyan);
                    break;
            case 1:
                Instantiate(_playerList[_playerIndex], _spawnPos[_playerIndex], Quaternion.identity, transform);
                ((DualShockGamepad)DualShock4GamepadHID.all[_playerIndex]).SetLightBarColor(Color.red);
                break;
            case 2:
                Instantiate(_playerList[_playerIndex], _spawnPos[_playerIndex], Quaternion.identity, transform);
                ((DualShockGamepad)DualShock4GamepadHID.all[_playerIndex]).SetLightBarColor(Color.green);
                break;
            case 3:
                Instantiate(_playerList[_playerIndex], _spawnPos[_playerIndex], Quaternion.identity, transform);
                ((DualShockGamepad)DualShock4GamepadHID.all[_playerIndex]).SetLightBarColor(Color.yellow);
                break;
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.GameStart) return;

        if (_player._respawn)
        {
            _timer += Time.deltaTime;
            if (_respwanTimer >= _timer) return;
            switch ( _playerIndex)
            {
                case 0:
                    _player.transform.position = _spawnPos[_playerIndex];
                    RespawnEffect(Color.cyan);
                    _player._respawn = false;
                    _player._state = CommonParam.UnitState.Normal;
                    break;
                case 1:
                    _player.transform.position = _spawnPos[_playerIndex];
                    RespawnEffect(Color.red);
                    _player._respawn = false;
                    _player._state = CommonParam.UnitState.Normal;
                    break;
                case 2:
                    _player.transform.position = _spawnPos[_playerIndex];
                    RespawnEffect(Color.green);
                    _player._respawn = false;
                    _player._state = CommonParam.UnitState.Normal;
                    break;
                case 3:
                    _player.transform.position = _spawnPos[_playerIndex];
                    RespawnEffect(Color.yellow);
                    _player._respawn = false;
                    _player._state = CommonParam.UnitState.Normal;
                    break;
            }
            _timer = 0;
        }
        
    }
    IEnumerator EffectDestroy(ParticleSystem ps)
    {
        yield return new WaitForSeconds(1f);
        Destroy(ps.gameObject,ps.main.duration);
    }

    void RespawnEffect(Color color)
    {
        _player._animator.SetTrigger("Respawn");
        ParticleSystem playerPs = Instantiate(_respawnPrefab, _spawnPos[_playerIndex] + new Vector3(0, -1.5f, 0), Quaternion.identity);
        foreach(ParticleSystem ps in playerPs.GetComponentsInChildren<ParticleSystem>())
        {
            var particleMain = ps.main;
            particleMain.startColor = color;
        }
        StartCoroutine(EffectDestroy(playerPs));
    }
}
