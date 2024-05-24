using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSettings : MonoBehaviour
{
    private Camera _camera;
    private PlayerInput _playerInput;
    private PlayerInputs _inputs;

    [SerializeField]
    [Header("Š´“x")]
    private float _cameraMoveSpeed;

    [SerializeField]
    private Transform _targetTransfrom;

    [SerializeField]
    GameObject _player;

    Vector3 _currentPos;
    Vector3 _pastPos;

    Vector3 _diff;

    private Vector2 _look = Vector2.zero;

    

    // Start is called before the first frame update
    void Start()
    {
        if(_camera == null) _camera = GetComponent<Camera>();
        if(_playerInput == null) _playerInput = GetComponentInParent<PlayerInput>();
        if(_inputs == null) _inputs = GetComponentInParent<PlayerInputs>();
        _pastPos = _player.transform.position;
        _camera.targetDisplay = _playerInput.user.index;
    }

    private void CameraControl()
    {
        transform.RotateAround(_targetTransfrom.position, new Vector3(_inputs._look.z, _inputs._look.x, 0f), _cameraMoveSpeed);
    }

    private void CameraMove()
    {
        _currentPos = _player.transform.position;

        _diff = _currentPos - _pastPos;

        transform.position = Vector3.Lerp(transform.position, transform.position + _diff, 1.0f);

        _pastPos = _currentPos;
    }

    private void Update()
    {
        CameraMove();
        CameraControl();
        //Debug.Log(_inputs._look);
    }
}
