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

        if (_playerInput.currentControlScheme == "Keyboard&Mouse")
        {
            _camera.targetDisplay = _playerInput.user.index;
        }
        else
        {
            var ctrl = Input.GetJoystickNames();
            _camera.targetDisplay = ctrl.Length;
        }
        
    }

}
