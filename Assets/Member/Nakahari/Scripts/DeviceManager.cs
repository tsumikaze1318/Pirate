using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class DeviceManager : MonoBehaviour
{
    // インスタンス化
    private static DeviceManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static DeviceManager Instance => instance;

    // 接続されてるゲームパッドの数
    private Gamepad[] _gamepad = new Gamepad[0];
    public Gamepad[] Gamepads => _gamepad;

    // ゲームパッドに番号を付けて管理
    public Dictionary<int, Gamepad> GamepadsDic;

    // 接続されているJoystickの名前
    public string[] JoystickNames = new string[0];

    // 接続されているコントローラーの数
    private int CurrentConnectionCount = 0;

    // コントローラーに表示するカラーの指定
    private Color[] _lightColors = { Color.cyan, Color.red, Color.green, Color.yellow };


    // Start is called before the first frame update
    void Start()
    {
        GamepadsDic = new Dictionary<int,Gamepad>();
        UpdateConnectedGamepads();
    }

    // Update is called once per frame
    void Update()
    {
        if(Gamepad.all.Count != _gamepad.Length)
        {
            UpdateConnectedGamepads();
        }
    }

    /// <summary>
    /// ゲームパッドのと番号を紐づけ
    /// </summary>
    void UpdateConnectedGamepads()
    {
        // 一度初期化させる
        Array.Clear(_gamepad, _gamepad.Length, _gamepad.Length);
        // _gamepad の数だけサイズを調整する
        Array.Resize(ref _gamepad, 0);
        // 配列に変更
        _gamepad = Gamepad.all.ToArray();
        // 要素の数だけ走る
        for(int i = 0; i < _gamepad.Length; i++)
        {
            // 配列に要素を追加
            GamepadsDic.Add(i + 1, _gamepad[i]);
            Debug.Log(GamepadsDic[i+1]);
            // valueをクラスに変更
            //Debug.Log($"Gamepad {i + 1}: {_gamepad[i].deviceId}");
        }
        ChengeColor();
    }

    /// <summary>
    /// ゲームパッドのライトカラーを変更
    /// </summary>
    void ChengeColor()
    {
        // 接続されているJoystickの名前を取得
        JoystickNames = Input.GetJoystickNames();
        // 配列文繰り返す
        for(int i = 0; i < JoystickNames.Length; i++)
        {
            // Joystickの名前があればカウントを追加する
            if (JoystickNames[i] != "")
            {
                CurrentConnectionCount++;
            }
        }

        // 接続されている数と色数、コントローラー数の最小値だけループ
        int count = Mathf.Min(CurrentConnectionCount, DualShock4GamepadHID.all.Count, _lightColors.Length);

        for(int i = 0; i < count; i++)
        {
            ((DualShockGamepad)DualShock4GamepadHID.all[i]).SetLightBarColor(_lightColors[i]);
        }
        
    }
}
