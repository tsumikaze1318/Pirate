using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSettings : MonoBehaviour
{
    private Camera _camera;
    private PlayerInput _playerInput;

    [SerializeField]
    private Transform _targetTransfrom;

    private Vector2 _look = Vector2.zero;
    
    public float _minCameraAngle = -45;
    public float _maxCameraAngle = 75;

    private void OnLook(InputValue value)
    {
        // LookAction ‚Ì“ü—Í’l‚ÌŽæ“¾
        var axis = value.Get<Vector2>();
        _look = new Vector2(axis.x, 0);
    }

    // Start is called before the first frame update
    void Start()
    {
        if(_camera == null) _camera = GetComponent<Camera>();
        if(_playerInput == null) _playerInput = GetComponentInParent<PlayerInput>();
        _camera.targetDisplay = _playerInput.user.index;
    }

    private void CameraControl()
    {
        transform.RotateAround(_targetTransfrom.position, new Vector3(_look.y, -_look.x, 0f), 1);
    }

    private void Update()
    {
        CameraControl();


    }        //this.transform.position = new Vector3(_targetTransfrom.position.x-4, _targetTransfrom.position.y+4, _targetTransfrom.position.z-4);

    private void FixedUpdate()
    {
        //transform.Rotate(new Vector3(_look.y, -_look.x, 0f));

        //transform.RotateAround(_targetTransfrom.position, new Vector3(_look.y, -_look.x, 0f), 0);
    }
}
