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

    private Transform _preTransform;

    private float _currentAngle;

    [SerializeField]
    private float _maxAngleX;

    [SerializeField]
    private float _minAngleX;

    // Start is called before the first frame update
    void Start()
    {
        if(_camera == null) _camera = GetComponent<Camera>();
        if(_playerInput == null) _playerInput = GetComponentInParent<PlayerInput>();
        if(_inputs == null) _inputs = GetComponentInParent<PlayerInputs>();
        _pastPos = _player.transform.position;
        _camera.targetDisplay = _playerInput.user.index;
        _currentAngle = transform.rotation.eulerAngles.x;
    }

    private void CameraControl()
    {
        var x = _inputs._look.y * _cameraMoveSpeed;
        var y = _inputs._look.x * _cameraMoveSpeed;
        transform.RotateAround(_targetTransfrom.position, Vector3.up, y);
        _currentAngle = transform.rotation.eulerAngles.x;
        if(_currentAngle <= _maxAngleX && _currentAngle >= _minAngleX)
        {
            transform.RotateAround(_targetTransfrom.position, -transform.right, x);
        }
        if(_currentAngle >= _maxAngleX || _currentAngle <= _minAngleX)
        {

        }
        _preTransform = this.transform;
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
    }
}
