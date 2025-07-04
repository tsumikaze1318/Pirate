using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

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

    private bool _stop;

    private Pirate _input;

    private bool _hold;


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
    /// <param name="ctx"></param>
    public void OnMove(InputAction.CallbackContext ctx)
    {
        // 数値が変わった際に
        if (ctx.performed)
        {
            // 値を代入
            var axis = ctx.ReadValue<Vector2>();
            if (!GameManager.Instance.GameStart) return;
            if (GameManager.Instance.GameEnd) return;
            // プレイヤーの状態が Normal だった際
            if (_state == CommonParam.UnitState.Normal)
            {
                // 移動アニメーションを流す
                _animator.SetBool("Move", true);
                // カメラの向いてる方向を取得
                Vector3 camForward = Vector3.Scale(_camera.transform.forward, new Vector3(1, 0, 1)).normalized;
                // 進む方向の入力を代入
                _moveForward = camForward * axis.y + _camera.transform.right * axis.x;
                transform.rotation = Quaternion.LookRotation(_moveForward);
            }
        }
        // 入力がなくなったら移動アニメーションを止める
        else if (ctx.canceled) _animator.SetBool("Move", false);
    }

    /// <summary>
    /// プレイヤーのジャンプ処理
    /// </summary>
    /// <param name="ctx"></param>
    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        // プレイヤーの状態が　Normal の際
        if (_state == CommonParam.UnitState.Normal)
        {
            // 入力があった際
            if (ctx.performed)
            {
                if (_respawn || _isJump) return;
                _isJump = true;
                // 上方向に力を追加
                _rb.AddForce(new Vector3(0, _upForce, 0), ForceMode.Impulse);
                // ジャンプアニメーションを流す
                _animator.SetTrigger("Jump");
                // 着地アニメーションのトリガーをリセットする
                _animator.ResetTrigger("Ground");
            }
        }
    }

    /// <summary>
    /// プレイヤーの攻撃処理
    /// </summary>
    /// <param name="ctx"></param>
    public void OnFire(InputAction.CallbackContext ctx)
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        if (_state == CommonParam.UnitState.Normal)
        {
            if (ctx.performed)
            {
                if (_respawn) return;
                // 攻撃アニメーションを流す
                _animator.SetTrigger("Attack");
            }
        }
    }

    /// <summary>
    /// プレイヤーの掴み処理
    /// </summary>
    /// <param name="ctx"></param>
    public void OnLeftGrab(InputAction.CallbackContext ctx)
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        if (_state == CommonParam.UnitState.Normal)
        {
            if (ctx.performed)
            {
                // 6/28　追記しました　横田
                _playerGrab.Grab();
            }
            else if(ctx.canceled)
            {
                _playerGrab.Release();
            }
        }
        
    }

    #region カーソルの処理

    public void OnCursorNone(InputAction.CallbackContext ctx)
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        if (_state == CommonParam.UnitState.Normal)
        {
            if (ctx.performed)
            {
                // カーソルのモードを None にする
                Cursor.lockState = CursorLockMode.None;
            }
        }
    }

    public void OnCursorLook(InputAction.CallbackContext ctx)
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        if (_state == CommonParam.UnitState.Normal)
        {
            if (ctx.performed)
            {
                // カーソルのモードを Locked にする
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }

    #endregion

    #region UI の処理

    public void OnUiButton(InputAction.CallbackContext ctx)
    {
        if (GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        if (_state == CommonParam.UnitState.Normal)
        {
            if (ctx.performed)
            {
                if (_button) return;
                // コルーチンを走らせる
                StartCoroutine(Ui());
            }
        }
    }

    /// <summary>
    /// ボタンを押すとuiを拡縮 それぞれの画面に適用
    /// </summary>
    /// <returns></returns>
    private IEnumerator Ui()
    {
        // 連続で走らない様に bool を true にする
        _button = true;
        // サイズを初期化する
        var vecX = 1f;
        var vecY = 1f;
        var vecZ = 1f;
        // 指定したスケールになるまで処理を繰り返す
        while (Vector3.SqrMagnitude(new Vector3(1.25f,1.25f,1.25f) - ScaleReturn(_playerInput.user.index)) > 0.001)
        {
            // それぞれの軸を 0.01f ずつ足していく
            vecX += 0.01f;
            vecY += 0.01f;
            vecZ += 0.01f;
            // 3軸をまとめてベクトルに変更
            Vector3 vec3 = new Vector3(vecX, vecY, vecZ);
            UiEffect(_playerInput.user.index, vec3);
            yield return new WaitForFixedUpdate();
        }
        // 0.2 秒待つ
        yield return new WaitForSeconds(0.2f);
        // 指定したスケールになるまで処理を繰り返す
        while (Vector3.SqrMagnitude(ScaleReturn(_playerInput.user.index) - new Vector3(1f, 1f, 1f)) > 0.001)
        {
            // それぞれの軸を 0.01f ずつ減らしていく
            vecX -= 0.01f;
            vecZ -= 0.01f;
            vecY -= 0.01f;
            // 3軸をまとめてベクトルに変更
            Vector3 vec3 = new Vector3(vecX, vecY, vecZ);
            UiEffect(_playerInput.user.index,vec3);
            yield return new WaitForFixedUpdate();
        }
        // 0.2 秒待つ
        yield return new WaitForSeconds(0.2f);
        // 処理が終わった際に bool を false に戻す
        _button = false;
    }

    /// <summary>
    /// スケールの適用
    /// </summary>
    /// <param name="num"> Player の番号</param>
    /// <param name="vec3">代入したい Vector3 </param>
    private void UiEffect(int num, Vector3 vec3)
    {
        foreach(ImageReady imageColor in _imageColors)
        {
            // 指定した image のスケールを変える
            imageColor._images[num].rectTransform.localScale = vec3;
        }
    }

    /// <summary>
    /// 現在のスケールを返す
    /// </summary>
    /// <param name="num"> Player の番号</param>
    /// <returns></returns>
    private Vector3 ScaleReturn(int num)
    {
        Vector3 vec3 = new Vector3();
        foreach (ImageReady imageColor in _imageColors)
        {
            // 現在のスケールを代入する
            vec3 = imageColor._images[num].rectTransform.localScale;
        }
        return vec3;
    }

    #endregion

    // AnimationEvent 用
    private void ColliderEnabled()
    {
        _swordCollider.enabled = true;
    }

    // AnimationEvent 用
    private void ColliderDisabled()
    {
        _swordCollider.enabled = false;
    }

    /// <summary>
    /// それぞれのtagに触れた際の判定
    /// </summary>
    /// <param name="other"></param>
    public void OnCollisionEnter(Collision other)
    {
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        // 地面か特定のレイヤーに触れた際に
        if (other.gameObject.CompareTag("Ground") || other.gameObject.layer == 6)
        {
            // ジャンプフラグを false にする
            _isJump = false;
            // 着地アニメーションを流す
            _animator.SetTrigger("Ground");
        }
        // Treasure タグに触れたら
        if (other.gameObject.CompareTag("Treasure"))
        {
            // Component を取得
            TreasureModel treasure = other.gameObject.GetComponent<TreasureModel>();
            // 取得した Script の関数を実行
            treasure.GetTreasure(_playerInput.user.index);
            // オブジェクトを削除
            Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// リスポーン時の画面処理
    /// </summary>
    /// <param name="other"></param>
    private void OnTriggerEnter(Collider other)
    {
        // リスポーン範囲に触れたら
        if (other.gameObject.CompareTag("UnderGround"))
        {
            // 溺れるアニメーションを流す
            _animator.SetTrigger("Drown");
            // リスポーン bool を true にする
            _respawn = true;
            // プレイヤーのコライダーの Trigger を true にし判定を変える
            _playerCollider.isTrigger = true;
            // 落下ポジションを取得
            _fallPos = other.ClosestPointOnBounds(this.transform.position);
            // 落下した場所にパーティクルを再生する
            ParticleSystem splashPs = Instantiate(_splashPrefab, _fallPos, Quaternion.identity);
            // 再生が終わり次第削除
            Destroy(splashPs.gameObject, splashPs.main.duration);
            // スコアを減らす
            GameManager.Instance.SubScore(_playerInput.user.index);
            // 指定した場所にリスポーンする
            _playerAssign.SetRespawnPlayer(gameObject.transform.parent.gameObject);
            // 速度を 0 にする
            _rb.velocity = Vector3.zero;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // リスポーン範囲に触れたら
        if (other.gameObject.CompareTag("UnderGround"))
        {
            // 溺れるパーティクルを流す
            ParticleSystem fallPs = Instantiate(_ripplesPrefab, _fallPos, Quaternion.Euler(-90, 0, 0));
            Destroy(fallPs.gameObject, fallPs.main.duration);
        }
    }

    private void Start()
    {
        // それぞれの取得と初期化
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
    private void Update()
    {
        if (GameManager.Instance.CameraChanged && !uiObject.activeInHierarchy)
            uiObject.SetActive(true);

        if (GameManager.Instance.GameStart) return;
        _uiGage = _holdAction.GetTimeoutCompletionPercentage();

        GameManager.Instance.SetIconFill(_playerInput.user.index, _uiGage);
    }

    private void FixedUpdate()
    {
        // 移動処理
        if(_state == CommonParam.UnitState.Normal)
        {
            if (_respawn) return;
            // 進む方向にスピードを掛けた値を position に代入
            transform.position += _moveForward * _moveSpeed;
            // 進む方向を向くように代入
            _rb.angularVelocity = _moveForward;
            _stop = false;
        }
        else if(_state == CommonParam.UnitState.Stun)
        {
            // スタンした際に進む方向をリセット
            _moveForward = Vector3.zero;
            if (_stop) return;
            // 速度を 0 に変更
            _rb.velocity = Vector3.zero;
            // 回転速度を 0 に変更
            _rb.angularVelocity = Vector3.zero;
            _stop = true;
        }
        else
        {
            _moveForward = Vector3.zero;
        }

        if(GameManager.Instance.GameEnd)
        {
            _moveForward = Vector3.zero;
            _rb.velocity = Vector3.zero;
            _animator.SetBool("Move", false);
        }
    }
}
