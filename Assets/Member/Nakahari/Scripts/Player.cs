using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEditor.Experimental.GraphView.GraphView;

public class Player : MonoBehaviour
{
    #region 宣言

    [SerializeField]
    private CommonParam.UnitType _unitType = CommonParam.UnitType.Player1;

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

    public Animator _animator;

    [SerializeField]
    private GameObject _obj;

    public bool _respawn = false;

    private BoxCollider _collider;

    [SerializeField]
    private GameObject uiObject;

    // 6/28　追記しました　横田
    [SerializeField]
    private PlayerGrab _playerGrab;

    bool lastFire = false;
    #endregion

    void Move()
    {
        Vector3 camForward = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 moveForward = camForward * _inputs._move.z + _camera.transform.right * _inputs._move.x;
        transform.position += moveForward * _moveSpeed;
        _rb.angularVelocity = moveForward;
        if (moveForward != Vector3.zero)
        {
            _animator.SetBool("Move", true);
            transform.rotation = Quaternion.LookRotation(moveForward);
        }
        else if(moveForward ==  Vector3.zero)
        {
            _animator.SetBool("Move", false);
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
        if(_inputs._fire != lastFire)
        {
            // Animationの再生
            _animator.SetBool("Attack", _inputs._fire);

            lastFire = _inputs._fire;
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
            // 6/28　追記しました　横田
            _playerGrab.Grab();
        }
        else
        {
            _playerGrab.Release();
        }
        
    }

    void RightGrab()
    {
        if (_inputs._rightGrab)
        {
            // 動かないオブジェクトを掴んだ処理はIKで可能
            // 他のオブジェクトは子オブジェクトにする
            
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

    void SkipMovie()
    {
        if (_inputs._movieSkip)
        {
            GameManager.Instance.FinishMovie();
            Debug.Log("aaa");
        }
    }

    void ColliderEnabled()
    {
        _collider.enabled = true;
    }

    void ColliderDisabled()
    {
        _collider.enabled = false;
    }

    void SubCount(Collision other)
    {
        HitCount hitCount = other.gameObject.GetComponent<HitCount>();
        hitCount._count--;
    }

    public void OnCollisionEnter(UnityEngine.Collision other)
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        if (other.gameObject.CompareTag("Ground") || other.gameObject.layer == 6)
        {
            _isJump = true;
        }
        if (other.gameObject.CompareTag("Treasure"))
        {
            Destroy(other.gameObject);
            TreasureModel treasure = other.gameObject.GetComponent<TreasureModel>();
            treasure.GetTreasure(_playerInput.user.index);
        }

        if (_collider ==  other.gameObject.CompareTag("Player"))
        {
            SubCount(other);
            _collider.enabled = false;
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
        if(_animator == null) _animator = GetComponentInParent<Animator>();
        _playerGrab ??= GetComponentInChildren<PlayerGrab>();
        _collider = _obj.GetComponent<BoxCollider>();
        _collider.enabled = false;

        _transform = transform;
        _prevPosition = _transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CameraChanged && !uiObject.activeInHierarchy)
            uiObject.SetActive(true);
        
        CursorLook();
        CursorNone();
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        if (_state == CommonParam.UnitState.Normal)
        {
            Jump();
            Fire();
            Lift();
            LeftGrab();
            RightGrab();
        }
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        if (_state == CommonParam.UnitState.Normal)
        {
            if (_respawn) return;
            Move();
        }
    }
}
