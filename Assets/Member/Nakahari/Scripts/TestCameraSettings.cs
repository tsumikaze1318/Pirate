using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestCameraSettings : MonoBehaviour
{
    private Camera _camera;
    private PlayerInput _playerInput;
    // Start is called before the first frame update
    void Start()
    {
        if(_camera == null) _camera = GetComponent<Camera>();
        _playerInput = GetComponentInParent<PlayerInput>();

        _camera.targetDisplay = _playerInput.user.index;
    }
}
