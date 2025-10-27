using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSettings : MonoBehaviour
{
    // 自身のプレイヤーオブジェクト
    [SerializeField]
    private GameObject _playerObj;

    // X軸の最大角度
    [SerializeField]
    private float _maxAngleX;

    // Y軸の最大角度
    [SerializeField]
    private float _minAngleX;

    // 自身のカメラ
    [SerializeField]
    private Camera _camera;

    // カメラ感度
    [SerializeField]
    [Header("感度")]
    private float _cameraMoveSpeed;

    // カメラの初期回転位置
    [SerializeField]
    private Vector3 _axisRot;

    // 自身のPlayerスクリプト
    private Player _player;

    // カメラの回転
    private Quaternion _cameraRot;

    // コントローラーの入力の値
    private Vector2 _axis;

    // Start is called before the first frame update
    void Start()
    {
        // Component の取得。それぞれの初期化
        if (_player == null) _player = _playerObj.GetComponent<Player>();
        // _camera.targetDisplay = _playerInput.user.index;
        _camera.transform.localPosition = new Vector3(0, 2, -5);
        _camera.transform.localRotation = transform.rotation;
        _cameraRot = _camera.transform.localRotation;
        transform.eulerAngles = _axisRot;


        // カメラの rotation を設定
        _camera.transform.localRotation = _cameraRot;

        // 自身の position を PlayerPosition と axisPosition を足した数値に変更
        transform.position = _player.transform.position;
        // 自身を入力された値にカメラの移動速度を掛けた速度で動かす
        transform.eulerAngles += new Vector3(-_axis.y * _cameraMoveSpeed, _axis.x * _cameraMoveSpeed, 0);

        // 現在の X 軸を代入する
        float angleX = transform.eulerAngles.x;

        // 180 度を超える角度を -180 ～ 180 の範囲に変更する
        if (angleX >= 180) { angleX = angleX - 360; }

        // Clamp を使い X 軸を制御
        transform.eulerAngles = new Vector3(Mathf.Clamp(angleX, _minAngleX, _maxAngleX), transform.eulerAngles.y, transform.eulerAngles.z);
    }


    /// <summary>
    /// プレイヤーを中心にカメラの操作
    /// </summary>
    public void OnLook(InputAction.CallbackContext ctx)
    {
        // 入力を受け取る
        _axis = ctx.ReadValue<Vector2>();
    }

    /// <summary>
    /// 落下した際にプレイヤーに向けてカメラを動かす
    /// </summary>
    void StopCameraControl()
    {
        _camera.transform.LookAt(_playerObj.transform);
    }

    private void Update()
    {
        if (_player._respawn)
        {
            StopCameraControl();
        }
        else
        {
            // カメラの rotation を設定
            _camera.transform.localRotation = _cameraRot;

            // 自身の position を PlayerPosition と axisPosition を足した数値に変更
            transform.position = _player.transform.position;
            if (!GameManager.Instance.GameStart) return;
            
            // 自身を入力された値にカメラの移動速度を掛けた速度で動かす
            transform.eulerAngles += new Vector3(-_axis.y * _cameraMoveSpeed, _axis.x * _cameraMoveSpeed, 0);

            // 現在の X 軸を代入する
            float angleX = transform.eulerAngles.x;

            // 180 度を超える角度を -180 ～ 180 の範囲に変更する
            if (angleX >= 180) { angleX = angleX - 360; }

            // Clamp を使い X 軸を制御
            transform.eulerAngles = new Vector3(Mathf.Clamp(angleX, _minAngleX, _maxAngleX), transform.eulerAngles.y, transform.eulerAngles.z);
        }
    }
}
