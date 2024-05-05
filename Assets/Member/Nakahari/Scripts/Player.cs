using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region 宣言

    //[SerializeField]
    //private CommonParam.UnitType _unitType = CommonParam.UnitType.Player1;

    [SerializeField]
    public CommonParam.UnitState _state = CommonParam.UnitState.Normal;

    private Vector3 _move;
    

    private Camera _camera;
    [SerializeField]
    private float _moveSpeed;

    bool _jump;

    Rigidbody _rb;

    [SerializeField]
    float _upForce;

    private Vector3 _prevPosition;
    private Transform _transform;

    #endregion

    private void OnMove(InputValue value)
    {
        // MoveAction の入力値の取得
        var axis = value.Get<Vector2>();
        // 移動速度の保持
        _move = new Vector3(axis.x, 0, axis.y) * _moveSpeed;
    }

    private void OnFire(InputValue value)
    {
        var button = value.isPressed;
        Debug.Log("攻撃");
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
        Debug.Log("持ち上げる");
    }

    private void OnLeftGrab(InputValue value)
    {
        var button = value.isPressed;
        Debug.Log("右手で持つ");
    }

    private void OnRightGrab(InputValue value)
    {
        var button = value.isPressed;
        Debug.Log("左手で持つ");
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

    private void PlayerLookDirection()
    {
        var position = _transform.position;
        var delta = position - _prevPosition;

        _prevPosition = position;

        if (delta == Vector3.zero) return;

        var rotation = Quaternion.LookRotation(delta, Vector3.up);

        _transform.rotation = rotation;
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
        if (_rb == null) _rb = GetComponent<Rigidbody>();

        _transform = transform;
        _prevPosition = _transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        PlayerLookDirection();
    }

    private void FixedUpdate()
    {
        // 移動座標を加算する
        transform.localPosition += _move;
    }
}
