using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region 宣言

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

    public bool _respawn = false;

    [SerializeField]
    private GameObject uiObject;


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
            // Animationの再生
            //_animatorClip = _animator.GetCurrentAnimatorClipInfo(0);
            //_stateTime = _animatorClip.Length;
            Debug.Log("攻撃");
        }
    }

    void Lift()
    {
        if (_inputs._lift)
        {
            // アニメーション再生
            Debug.Log("持ち上げる");
        }
    }

    void LeftGrab()
    {
        if (_inputs._leftGrab)
        {
            // 動かないオブジェクトを掴んだ処理はIKで可能
            // 他のオブジェクトは子オブジェクトにする
            Debug.Log("右手で持つ");
        }
    }

    void RightGrab()
    {
        if (_inputs._rightGrab)
        {
            // 動かないオブジェクトを掴んだ処理はIKで可能
            // 他のオブジェクトは子オブジェクトにする
            Debug.Log("左手で持つ");
        }
    }
    
    void CursorNone()
    {
        if (_inputs._cursorNone)
        {
            Cursor.lockState = CursorLockMode.None;
            _inputs._cursorNone = false;

        }
    }

    void CursorLook()
    {
        if (_inputs._cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            _inputs._cursorLock = false;
        }
    }

    public void OnCollisionEnter(UnityEngine.Collision other)
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        if (other.gameObject.CompareTag("Ground"))
        {
            _isJump = true;
        }
        if (other.gameObject.CompareTag("Treasure"))
        {
            Destroy(other.gameObject);
            TreasureModel treasure = other.gameObject.GetComponent<TreasureModel>();
            treasure.GetTreasure(_playerInput.user.index);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("UnderGround"))
        {
            GameManager.Instance.SubScore(_playerInput.user.index);
            _rb.velocity = Vector3.zero;
            _respawn = true;
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
        if (GameManager.Instance.CameraChanged && !uiObject.activeInHierarchy)
            uiObject.SetActive(true);

        if (!GameManager.Instance.GameStart) return;
        if(GameManager.Instance.GameEnd) return;
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

        //Debug.Log(_state);
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        if (_state == CommonParam.UnitState.Normal)
        {
            Move();
        }
    }
}
