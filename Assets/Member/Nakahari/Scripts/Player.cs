using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region 宣言

    [SerializeField]
    public CommonParam.UnitState _state = CommonParam.UnitState.Normal;

    PlayerInputs _inputs;

    PlayerInput _playerInput;

    [SerializeField]
    private float _moveSpeed;

    bool _isJump = false;

    Rigidbody _rb;

    [SerializeField]
    float _upForce;

    [SerializeField]
    Camera _camera;

    public Animator _animator;

    [SerializeField]
    private GameObject _swordObj;

    public bool _respawn = false;

    private BoxCollider _swordCollider;

    private CapsuleCollider _playerCollider;

    [SerializeField]
    private GameObject uiObject;

    // 6/28　追記しました　横田
    [SerializeField]
    private PlayerGrab _playerGrab;

    bool lastFire = false;

    private Vector3 _fallPos;

    [SerializeField]
    ParticleSystem _splashPrefab;

    [SerializeField]
    ParticleSystem _ripplesPrefab;

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
        if (!_isJump && _inputs._jump)
        {
            _rb.AddForce(new Vector3(0, _upForce, 0), ForceMode.Impulse);
            _isJump = true;
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
            Physics.IgnoreCollision(_playerGrab.GrabObject.GetComponent<Collider>(), _playerCollider, true);
        }
        else
        {
            Physics.IgnoreCollision(_playerGrab.GrabObject.GetComponent<Collider>(), _playerCollider, false);
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
        // 作りかけ
        if (_inputs._movieSkip)
        {
            GameManager.Instance.FinishMovie();
            Debug.Log("aaa");
        }
    }

    void ColliderEnabled()
    {
        _swordCollider.enabled = true;
    }

    void ColliderDisabled()
    {
        _swordCollider.enabled = false;
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
            _isJump = false;
        }
        if (other.gameObject.CompareTag("Treasure"))
        {
            Destroy(other.gameObject);
            TreasureModel treasure = other.gameObject.GetComponent<TreasureModel>();
            treasure.GetTreasure(_playerInput.user.index);
        }

        if (_swordCollider ==  other.gameObject.CompareTag("Player"))
        {
            SubCount(other);
            _swordCollider.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("UnderGround"))
        {
            _respawn = true;
            _fallPos = other.ClosestPointOnBounds(this.transform.position);
            ParticleSystem splashPs = Instantiate(_splashPrefab, _fallPos, Quaternion.identity);
            Destroy(splashPs.gameObject, splashPs.main.duration);
            GameManager.Instance.SubScore(_playerInput.user.index);
            _rb.velocity = Vector3.zero;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("UnderGround"))
        {
            ParticleSystem fallPs = Instantiate(_ripplesPrefab, _fallPos, Quaternion.Euler(-90, 0, 0));
            Destroy(fallPs.gameObject, fallPs.main.duration);
        }
    }

    private void Start()
    {
        if (_rb == null) _rb = GetComponent<Rigidbody>();
        if(_inputs == null) _inputs = GetComponentInParent<PlayerInputs>();
        if(_playerInput == null) _playerInput = GetComponentInParent<PlayerInput>();
        if(_animator == null) _animator = GetComponentInParent<Animator>();
        _playerGrab ??= GetComponentInChildren<PlayerGrab>();
        _swordCollider = _swordObj.GetComponent<BoxCollider>();
        _playerCollider = GetComponent<CapsuleCollider>();
        _swordCollider.enabled = false;
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
