using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestInputSystem : MonoBehaviour
{
    #region �錾

    [SerializeField]
    private CommonParam.UnitType _unitType = CommonParam.UnitType.Player1;

    [SerializeField]
    public CommonParam.UnitState _state = CommonParam.UnitState.Normal;

    private Vector3 _move;
    private Vector3 _look;

    private Camera _camera;
    [SerializeField]
    private float _moveSpeed;
    [SerializeField]
    private float _lookSpeed;

    //private float _maxCamX;
    //private float _minCamX;

    private GameObject _targetObject;

    bool cursorLock = true;

    bool _jump;

    Rigidbody _rb;

    [SerializeField]
    float _upForce;

    #endregion

    private void OnMove(InputValue value)
    {
        // MoveAction �̓��͒l�̎擾
        var axis = value.Get<Vector2>();
        // �ړ����x�̕ێ�
        _move = new Vector3(axis.x, 0, axis.y) * _moveSpeed;
    }

    private void OnLook(InputValue value)
    {
        // LookAction �̓��͒l�̎擾
        var axis = value.Get<Vector2>();
        _look = new Vector3(axis.x, axis.y, 0);
    }

    private void OnFire(InputValue value)
    {
        var button = value.isPressed;
        Debug.Log("�U��");
    }

    private void OnJump(InputValue value)
    {
        var button = value.isPressed;

        if (_jump && button)
        {
            _rb.AddForce(new Vector3(0, _upForce, 0));
            _jump = false;
        }
    }

    private void OnLift(InputValue value)
    {
        var button = value.isPressed;
        Debug.Log("�����グ��");
    }

    private void OnLeftGrab(InputValue value)
    {
        var button = value.isPressed;
        Debug.Log("�E��Ŏ���");
    }

    private void OnRightGrab(InputValue value)
    {
        var button = value.isPressed;
        Debug.Log("����Ŏ���");
    }

    private void OnCursorNone(InputValue value)
    {
        var button = value.isPressed;
        if (button)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void OnCursorLook(InputValue value)
    {
        var button = value.isPressed;
        if (button)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            _jump = true;
        }
    }

    private void Start()
    {
        _targetObject = this.gameObject;
        if(_camera == null) _camera = GetComponentInChildren<Camera>();
        if (_rb == null) _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // �ړ����W�����Z����
        transform.localPosition += _move;
        //_camera.transform.RotateAround(_targetObject.transform.position, new Vector3(_look.y, _look.x, 0), _lookSpeed);
        //_camera.transform.localEulerAngles += _look;
    }
}
