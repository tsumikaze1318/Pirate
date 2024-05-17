using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAssign : MonoBehaviour
{
    [SerializeField]
    List<GameObject> _player = new List<GameObject>();

    [SerializeField]
    List<Vector3> _spawnPos = new List<Vector3>();

    PlayerInput _playerInput;

    int _playerIndex;

    void Start()
    {
        _playerInput = GetComponent<PlayerInput>();
        _playerIndex = this._playerInput.user.index;
        Assign();
    }

    void Assign()
    {
        switch (_playerIndex)
        {
            case 0:
                Instantiate(_player[_playerIndex], _spawnPos[_playerIndex], Quaternion.identity, transform);
                break;
            case 1:
                Instantiate(_player[_playerIndex], _spawnPos[_playerIndex], Quaternion.identity, transform);
                break;
            case 2:
                Instantiate(_player[_playerIndex], _spawnPos[_playerIndex], Quaternion.identity, transform);
                break;
            case 3:
                Instantiate(_player[_playerIndex], _spawnPos[_playerIndex], Quaternion.identity, transform);
                break;
        }
    }
}
