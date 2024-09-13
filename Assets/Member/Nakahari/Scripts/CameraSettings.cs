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
    GameObject _playerObj;

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
    [Header("感度")]
    private float _cameraMoveSpeed;

    [SerializeField]
    Vector3 _axisPos;
    [SerializeField]
    Vector3 _axisRot;

    private Player _player;

    Quaternion _cameraRot;

    // Start is called before the first frame update
    void Start()
    {
        if (_playerInput == null) _playerInput = GetComponentInParent<PlayerInput>();
        if (_inputs == null) _inputs = GetComponentInParent<PlayerInputs>();
        if (_player == null) _player = _playerObj.GetComponent<Player>();
        _pastPos = _player.transform.position;
        _camera.targetDisplay = _playerInput.user.index;
        _camera.transform.localPosition = new Vector3(0, 2, -5);
        _camera.transform.localRotation = transform.rotation;
        _cameraRot = _camera.transform.localRotation;
        transform.eulerAngles = _axisRot;
    }


    /// <summary>
    /// プレイヤーを中心にカメラの操作
    /// </summary>
    private void CameraControl()
    {
        _camera.transform.localRotation = _cameraRot;

        transform.position = _player.transform.position + _axisPos;
        
        if (!GameManager.Instance.GameStart) return;

        transform.eulerAngles += new Vector3(-_inputs._look.y * _cameraMoveSpeed, _inputs._look.x * _cameraMoveSpeed, 0);
        
        float angleX = transform.eulerAngles.x;

        if(angleX >= 180) { angleX = angleX - 360; }

        transform.eulerAngles = new Vector3(Mathf.Clamp(angleX, _minAngleX, _maxAngleX), transform.eulerAngles.y, transform.eulerAngles.z);
    }

    void StopCameraControl()
    {
        _camera.transform.LookAt(_playerObj.transform);
    }

    private void Update()
    {
        if (_player._respawn)
        {
            StopCameraControl();
        }
        else
        {
            CameraControl();
        }
    }
}
