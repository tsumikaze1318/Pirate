using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAssign : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _playerList = new List<GameObject>();

    [SerializeField]
    List<Vector3> _spawnPos = new List<Vector3>();

    PlayerInput _playerInput;

    Player _player;

    int _playerIndex;

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
                break;
            case 1:
                Instantiate(_playerList[_playerIndex], _spawnPos[_playerIndex], Quaternion.identity, transform);
                break;
            case 2:
                Instantiate(_playerList[_playerIndex], _spawnPos[_playerIndex], Quaternion.identity, transform);
                break;
            case 3:
                Instantiate(_playerList[_playerIndex], _spawnPos[_playerIndex], Quaternion.identity, transform);
                break;
        }
    }

    private void Update()
    {
        if (!GameManager.Instance.GameStart) return;

        if (_player._respawn)
        {
            switch ( _playerIndex)
            {
                case 0:
                    _player.transform.position = _spawnPos[_playerIndex];
                    _player._respawn = false;
                    _player._state = CommonParam.UnitState.Normal;
                    break;
                case 1:
                    _player.transform.position = _spawnPos[_playerIndex];
                    _player._respawn = false;
                    _player._state = CommonParam.UnitState.Normal;
                    break;
                case 2:
                    _player.transform.position = _spawnPos[_playerIndex];
                    _player._respawn = false;
                    _player._state = CommonParam.UnitState.Normal;
                    break;
                case 3:
                    _player.transform.position = _spawnPos[_playerIndex];
                    _player._respawn = false;
                    _player._state = CommonParam.UnitState.Normal;
                    break;
            }
        }
    }
}
