using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region êÈåæ

    //[SerializeField]
    //private CommonParam.UnitType _unitType = CommonParam.UnitType.Player1;

    [SerializeField]
    public CommonParam.UnitState _state = CommonParam.UnitState.Normal;

    PlayerInputs _inputs;

    [SerializeField]
    private float _moveSpeed;

    bool _isJump;

    Rigidbody _rb;

    [SerializeField]
    float _upForce;

    private Vector3 _prevPosition;
    private Transform _transform;

    [SerializeField]
    Camera _camera;

    #endregion

    void Move()
    {
        Vector3 camForward = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = camForward * _inputs._move.z + _camera.transform.right * _inputs._move.x;
        transform.position += moveForward * _moveSpeed;
        if (moveForward != Vector3.zero)
        {
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
    }

    void Jump()
    {
        if (_isJump && _inputs._jump)
        {
            _rb.AddForce(new Vector3(0, _upForce, 0));
            _isJump = false;
        }
    }

    void Fire()
    {
        if(_inputs._fire)
        {
            Debug.Log("çUåÇ");
        }
    }

    void Lift()
    {
        if (_inputs._lift)
        {
            Debug.Log("éùÇøè„Ç∞ÇÈ");
        }
    }

    void LeftGrab()
    {
        if (_inputs._leftGrab)
        {
            Debug.Log("âEéËÇ≈éùÇ¬");
        }
    }

    void RightGrab()
    {
        if (_inputs._rightGrab)
        {
            Debug.Log("ç∂éËÇ≈éùÇ¬");
        }
    }
    
    void CursorNone()
    {
        if (_inputs._cursorNone)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void CursorLook()
    {
        if (_inputs._cursorLock)
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
            _isJump = true;
        }
    }

    private void Start()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();
        if(_inputs == null) _inputs = GetComponentInParent<PlayerInputs>();

        _transform = transform;
        _prevPosition = _transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
        Lift();
        Fire();
        LeftGrab();
        RightGrab();
        CursorLook();
        CursorNone();
        //PlayerLookDirection();
    }

    private void FixedUpdate()
    {
        Move();
    }
}
