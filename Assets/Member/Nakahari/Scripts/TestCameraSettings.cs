using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestCameraSettings : MonoBehaviour
{
    private Camera _camera;
    private PlayerInput _playerInput;

    private Vector2 _look = Vector2.zero;

    public float _minCameraAngle = -45;
    public float _maxCameraAngle = 75;

    private void OnLook(InputValue value)
    {
        // LookAction ‚Ì“ü—Í’l‚ÌŽæ“¾
        var axis = value.Get<Vector2>();
        _look = new Vector2(axis.x, axis.y);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(_camera == null) _camera = GetComponent<Camera>();
        _playerInput = GetComponentInParent<PlayerInput>();

        _camera.targetDisplay = _playerInput.user.index;
    }

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(_look.y, -_look.x, 0f));
    }
}
