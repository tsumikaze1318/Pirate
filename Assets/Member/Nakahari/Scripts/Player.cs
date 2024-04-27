using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour
{
    [SerializeField]
    private CommonParam.UnitType _unitType = CommonParam.UnitType.Player1;

    [SerializeField]
    InputManager _inputManager;

    public GameObject Cam;

    bool cursorLock = true;

    private InputManager.InputParam _inputParam;

    private Animation _animation;

    float Speed;

    bool _jump;

    Rigidbody _rb;

    [SerializeField]
    public CommonParam.UnitState _state = CommonParam.UnitState.Normal;

    HitCount _hitCount;

    // Start is called before the first frame update
    void Start()
    {
        if (_inputManager == null)
        {
            _inputManager = GetComponent<InputManager>();
        }
        if(_rb == null)
        {
            _rb = GetComponent<Rigidbody>();
        }
        _inputParam = _inputManager.UnitInputParams[_unitType];

        if(_hitCount == null)
        {
            _hitCount = GetComponent<HitCount>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCursorLock();

        if (_state == CommonParam.UnitState.Normal)
        {
            UnitMove();
            UnitJump();
        }
        //Debug.Log(_state);
        //Debug.Log(_inputParam.Attack);
        //Debug.Log(_inputParam.Jump);
        //Debug.Log(_inputParam.Lift);
        //Debug.Log(_inputParam.RightGrab);
        //Debug.Log(_inputParam.LeftGrab);
    }

    //マウスカーソルをゲーム中消し、escキーを押す事でロックを解除
    public void UpdateCursorLock()
    {
        if (_inputParam.CursorLock)
        {
            cursorLock = false;
        }
        else if (Input.GetMouseButton(0))
        {
            cursorLock = true;

        }
        if (cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;

        }
        else if (!cursorLock)
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    public void UnitMove()
    {
        Speed = 0.01f;

        Vector3 camForward = Cam.transform.forward;
        camForward.y = 0;
        transform.position += (camForward * _inputParam.MoveZ + Cam.transform.right * _inputParam.MoveX) * Speed;
    }

    public void UnitJump()
    {
        Vector3 force = new Vector3(0, 5, 0);
        if (_jump && _inputParam.Jump)
        {
            _rb.AddForce(force);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Ground"))
        {
            _jump = true;
        }
    }

    /*public void UnitAttack()
    {
        // 攻撃アニメーション(生命冒涜)
        if(_inputParam.Attack)
        {
            _hitCount._count--;
        }
    }*/
}


