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

    public CapsuleCollider _playerCollider;

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

    [SerializeField]
    ParticleSystem _particlePrefab;

    private bool _button;

    private PlayerAssign _playerAssign;

    Vector3 _hitPos;

    private ImageReady[] _imageColors;

    public InputAction _holdAction;

    private Vector3 _moveForward;
    private float _uiGage;


    #endregion


    private void Awake()
    {
        if (_playerInput == null) _playerInput = GetComponent<PlayerInput>();
        _holdAction = _playerInput.actions["LongPress"];
        _holdAction.Enable();
    }

    /// <summary>
    /// プレイヤーの移動処理
    /// </summary>
    void OnMove(InputValue value)
    {
        var axis = value.Get<Vector2>();
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        if (_state == CommonParam.UnitState.Normal)
        {
            if (_respawn) return;
            Vector3 camForward = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1)).normalized;
             _moveForward = camForward * axis.y + _camera.transform.right * axis.x;
            
        }
    }
    
    /// <summary>
    /// プレイヤーの行動処理
    /// </summary>
    void OnJump(InputValue value)
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        if (_state == CommonParam.UnitState.Normal)
        {
            if (!_isJump && value.isPressed)
            {
                _rb.AddForce(new Vector3(0, _upForce, 0), ForceMode.Impulse);
                _animator.SetTrigger("Jump");
                _animator.ResetTrigger("Ground");
                _isJump = true;
            }
        }
    }

    void OnFire(InputValue value)
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        if (_state == CommonParam.UnitState.Normal)
        {
            if (value.isPressed != lastFire)
            {
                _animator.SetBool("Attack", value.isPressed);

                lastFire = value.isPressed;
            }
        }
    }

    void OnLeftGrab(InputValue value)
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        if (_state == CommonParam.UnitState.Normal)
        {
            if (value.isPressed)
            {
                // 6/28　追記しました　横田
                _playerGrab.Grab();
            }
            else
            {
                _playerGrab.Release();
            }
        }
        
    }

    /// <summary>
    /// カーソル処理
    /// </summary>
    void OnCursorNone(InputValue value)
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        if (_state == CommonParam.UnitState.Normal)
        {
            if (value.isPressed)
            {
                Cursor.lockState = CursorLockMode.None;

            }
        }
    }

    void OnCursorLook(InputValue value)
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        if (_state == CommonParam.UnitState.Normal)
        {
            if (value.isPressed)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    void OnUiButton(InputValue value)
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        if (_state == CommonParam.UnitState.Normal)
        {
            if (value.isPressed)
            {
                if (_button) return;
                StartCoroutine(Ui());
            }
        }
    }

    /// <summary>
    /// ボタンを押すとuiを拡縮 それぞれの画面に適用
    /// </summary>
    /// <returns></returns>
    IEnumerator Ui()
    {
        _button = true;
        var vecX = 1f;
        var vecY = 1f;
        var vecZ = 1f;
        while (Vector3.SqrMagnitude(new Vector3(1.25f,1.25f,1.25f) - ScaleReturn(_playerInput.user.index)) > 0.001)
        {
            vecX += 0.01f;
            vecY += 0.01f;
            vecZ += 0.01f;
            Vector3 vec3 = new Vector3(vecX, vecY, vecZ);
            UiEffect(_playerInput.user.index, vec3);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.2f);
        while (Vector3.SqrMagnitude(ScaleReturn(_playerInput.user.index) - new Vector3(1f, 1f, 1f)) > 0.001)
        {
            vecX -= 0.01f;
            vecZ -= 0.01f;
            vecY -= 0.01f;
            Vector3 vec3 = new Vector3(vecX, vecY, vecZ);
            UiEffect(_playerInput.user.index,vec3);
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.2f);
        _button = false;
    }

    /// <summary>
    /// スケールの適用
    /// </summary>
    /// <param name="num"></param>
    /// <param name="vec3"></param>
    void UiEffect(int num, Vector3 vec3)
    {
        foreach(ImageReady imageColor in _imageColors)
        {
            imageColor._images[num].rectTransform.localScale = vec3;
        }
    }

    /// <summary>
    /// 現在のスケールを返す
    /// </summary>
    /// <param name="num"></param>
    /// <returns></returns>
    Vector3 ScaleReturn(int num)
    {
        Vector3 vec3 = new Vector3();
        foreach (ImageReady imageColor in _imageColors)
        {
            vec3 = imageColor._images[num].rectTransform.localScale;
        }
        return vec3;
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

    /// <summary>
    /// それぞれのtagに触れた際の判定
    /// </summary>
    /// <param name="other"></param>
    public void OnCollisionEnter(Collision other)
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        if (other.gameObject.CompareTag("Ground") || other.gameObject.layer == 6)
        {
            _isJump = false;
            _animator.SetTrigger("Ground");
        }
        if (other.gameObject.CompareTag("Treasure"))
        {
            TreasureModel treasure = other.gameObject.GetComponent<TreasureModel>();
            treasure.GetTreasure(_playerInput.user.index);
            Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// リスポーン時の画面処理
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("UnderGround"))
        {
            _animator.SetTrigger("Drown");
            _respawn = true;
            _playerCollider.isTrigger = true;
            _fallPos = other.ClosestPointOnBounds(this.transform.position);
            ParticleSystem splashPs = Instantiate(_splashPrefab, _fallPos, Quaternion.identity);
            Destroy(splashPs.gameObject, splashPs.main.duration);
            GameManager.Instance.SubScore(_playerInput.user.index);
            _rb.velocity = Vector3.zero;
            _playerAssign.SetRespawnPlayer(gameObject.transform.parent.gameObject);
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
        GameManager.Instance.AddPlayer(transform.parent.gameObject);
        if (_rb == null) _rb = GetComponent<Rigidbody>();
        if(_animator == null) _animator = GetComponent<Animator>();
        _playerGrab ??= GetComponentInChildren<PlayerGrab>();
        _swordCollider = _swordObj.GetComponent<BoxCollider>();
        _playerCollider = GetComponent<CapsuleCollider>();
        //_swordCollider.enabled = false;
        _playerAssign = GetComponentInParent<PlayerAssign>();
        _imageColors = FindObjectsOfType<ImageReady>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.CameraChanged && !uiObject.activeInHierarchy)
            uiObject.SetActive(true);

        if (GameManager.Instance.GameStart) return;
        _uiGage = _holdAction.GetTimeoutCompletionPercentage();

        GameManager.Instance.SetIconFill(_playerInput.user.index, _uiGage);
    }

    private void FixedUpdate()
    {
        transform.position += _moveForward * _moveSpeed;
        _rb.angularVelocity = _moveForward;
        if (_moveForward != Vector3.zero)
        {
            _animator.SetBool("Move", true);
            transform.rotation = Quaternion.LookRotation(_moveForward);
        }
        else if (_moveForward == Vector3.zero)
        {
            _animator.SetBool("Move", false);
        }
    }
}
