using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSettings : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerInputs _inputs;

    //[SerializeField]
    //private Transform _targetTransfrom;

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

    [SerializeField]
    private Camera _camera;

    [SerializeField]
    [Header("Š´“x")]
    private float _cameraMoveSpeed;

    [SerializeField]
    Vector3 _axisPos;

    // Start is called before the first frame update
    void Start()
    {
        if (_playerInput == null) _playerInput = GetComponentInParent<PlayerInput>();
        if (_inputs == null) _inputs = GetComponentInParent<PlayerInputs>();
        _pastPos = _player.transform.position;
        _camera.targetDisplay = _playerInput.user.index;
        _camera.transform.localPosition = new Vector3(0, 0, -5);
        _camera.transform.localRotation = transform.rotation;
    }

    private void CameraControl()
    {
        transform.position = _player.transform.position + _axisPos;

        transform.eulerAngles += new Vector3(_inputs._look.y * _cameraMoveSpeed, _inputs._look.x * _cameraMoveSpeed, 0);
        
        float angleX = transform.eulerAngles.x;

        if(angleX >= 180) { angleX = angleX - 360; }

        transform.eulerAngles = new Vector3(Mathf.Clamp(angleX, _minAngleX, _maxAngleX), transform.eulerAngles.y, transform.eulerAngles.z);

    }

    private void Update()
    {
        if (!GameManager.Instance.GameStart) return;
        CameraControl();
    }
}
