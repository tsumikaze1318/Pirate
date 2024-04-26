using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitMove : MonoBehaviour
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

    // Start is called before the first frame update
    void Start()
    {
        if (_inputManager == null)
        {
            _inputManager = GetComponent<InputManager>();
        }
        _inputParam = _inputManager.UnitInputParams[_unitType];
    }

    // Update is called once per frame
    void Update()
    {
        UpdateCursorLock();
        //Dash��dash()���ĂԂ悤�ɕύX���肢���܂�.
    }

    //�J�����̌����𐳖ʂɂ��ē���
    private void FixedUpdate()
    {
        Speed = 5f;

        Vector3 camForward = Cam.transform.forward;
        camForward.y = 0;
        transform.position += (camForward * _inputParam.MoveZ + Cam.transform.right * _inputParam.MoveX) * Speed;
    }

    //�}�E�X�J�[�\�����Q�[���������Aesc�L�[���������Ń��b�N������
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
}
