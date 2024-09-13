using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputs : MonoBehaviour
{
    private static PlayerInputs instance;
    public static PlayerInputs Instance { get { return instance; } }

    public Vector3 _move;
    public Vector3 _look;
    public bool _fire;
    public bool _jump;
    public bool _lift;
    public bool _leftGrab;
    public bool _rightGrab;
    public bool _cursorNone;
    public bool _cursorLock;
    public bool _uiButton;
    public bool _longPress;

    private void OnMove(InputValue value)
    {
        // MoveAction の入力値の取得
        var axis = value.Get<Vector2>();
        // 移動速度の保持
        _move = new Vector3(axis.x, 0, axis.y);
    }

    private void OnLook(InputValue value)
    {
        // LookAction の入力値の取得
        var axis = value.Get<Vector2>();
        _look = new Vector3(axis.x, axis.y, 0f);
    }

    private void OnFire(InputValue value)
    {
        _fire = value.isPressed;
    }

    private void OnJump(InputValue value)
    {
        _jump = value.isPressed;
    }

    private void OnLift(InputValue value)
    {
        _lift = value.isPressed;
    }

    private void OnLeftGrab(InputValue value)
    {
        _leftGrab = value.isPressed;
    }

    private void OnRightGrab(InputValue value)
    {
        _rightGrab = value.isPressed;
    }

    private void OnCursorNone(InputValue value)
    {
        _cursorNone = value.isPressed;
        
    }

    private void OnCursorLock(InputValue value)
    {
        _cursorLock = value.isPressed;
        
    }

    private void OnUiButton(InputValue value)
    {
        _uiButton = value.isPressed;
    }

    private void OnLongPress(InputValue value)
    {
        _longPress = value.isPressed;
    }
}
