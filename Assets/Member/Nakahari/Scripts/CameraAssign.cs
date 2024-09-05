using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraAssign : MonoBehaviour
{

    private PlayerInput _playerInput;

    Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        if (_playerInput == null) _playerInput = GetComponentInParent<PlayerInput>();
        if (_camera == null) _camera = GetComponent<Camera>();

        foreach(var displayIndex in DeviceManager.Instance.Gamepads)
        {
            _camera.targetDisplay = _playerInput.user.index;
        }
    }

}
