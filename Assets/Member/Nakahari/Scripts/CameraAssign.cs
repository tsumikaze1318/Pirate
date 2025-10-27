using UnityEngine;
using UnityEngine.InputSystem;

public class CameraAssign : MonoBehaviour
{
    [SerializeField]
    private PlayerInput _playerInput;

    Camera _camera;

    // Start is called before the first frame update
    void Start()
    {
        if (_camera == null) _camera = GetComponent<Camera>();

        // 接続されている Gamepad の数だけループする
        for(int i = 0; i < DeviceManager.Instance.GamepadsDic.Count; i++)
        {
            // カメラのディスプレイを接続されたコントローラーと同じ数字に変更
            _camera.targetDisplay = _playerInput.user.index;
        }
    }

}
