using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region �錾

    //[SerializeField]
    //private CommonParam.UnitType _unitType = CommonParam.UnitType.Player1;

    [SerializeField]
    public CommonParam.UnitState _state = CommonParam.UnitState.Normal;

    PlayerInputs _inputs;

    PlayerInput _playerInput;

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

    private Animator _animator;

    private AnimatorClipInfo[] _animatorClip;
    public float _stateTime;




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
            _rb.AddForce(new Vector3(0, _upForce, 0), ForceMode.Impulse);
            _isJump = false;
        }
    }

    void Fire()
    {
        if(_inputs._fire)
        {
            // Animation�̍Đ�
            //_animatorClip = _animator.GetCurrentAnimatorClipInfo(0);
            //_stateTime = _animatorClip.Length;
            Debug.Log("�U��");
        }
    }

    void Lift()
    {
        if (_inputs._lift)
        {
            // �A�j���[�V�����Đ�
            Debug.Log("�����グ��");
        }
    }

    void LeftGrab()
    {
        if (_inputs._leftGrab)
        {
            // ��̃|�W�V�������Œ�
            // �G�ꂽ�I�u�W�F�N�g��r��warld���W�ɒǏ]
            Debug.Log("�E��Ŏ���");
        }
    }

    void RightGrab()
    {
        if (_inputs._rightGrab)
        {
            // ��̃|�W�V�������Œ�
            // �G�ꂽ�I�u�W�F�N�g��r��warld���W�ɒǏ]
            Debug.Log("����Ŏ���");
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

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isJump = true;
        }
        if (other.gameObject.CompareTag("Treasure"))
        {
            Destroy(other.gameObject);
            GameManager.Instance.AddScore(_playerInput.user.index);
            TreasureRandomInstance.Instance.RandomInstance();
        }
    }

    private void Start()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();
        if(_inputs == null) _inputs = GetComponentInParent<PlayerInputs>();
        if(_playerInput == null) _playerInput = GetComponentInParent<PlayerInput>();

        _transform = transform;
        _prevPosition = _transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (_state == CommonParam.UnitState.Normal)
        {
            Jump();
            Fire();
            Lift();
            LeftGrab();
            RightGrab();
        }
        CursorLook();
        CursorNone();
    }

    private void FixedUpdate()
    {
        if (_state == CommonParam.UnitState.Normal)
        {
            Move();
        }
    }
}
