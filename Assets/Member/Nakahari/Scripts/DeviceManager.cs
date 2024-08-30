using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class DeviceManager : MonoBehaviour
{
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

    private Gamepad[] _gamepad;

    public Dictionary<int, Gamepad> Gamepads;


    // Start is called before the first frame update
    void Start()
    {
        Gamepads = new Dictionary<int,Gamepad>();
        UpdateConnectedGamepads();
        ChengeColor();
    }

    // Update is called once per frame
    void Update()
    {
        if(Gamepad.all.Count != _gamepad.Length)
        {
            UpdateConnectedGamepads();
        }
    }

    void UpdateConnectedGamepads()
    {
        _gamepad = Gamepad.all.ToArray();
        for(int i = 0; i < _gamepad.Length; i++)
        {
            Gamepads.Add(i + 1, _gamepad[i]);
            Debug.Log(Gamepads[i+1]);
            // value���N���X�ɕύX
            //Debug.Log($"Gamepad {i + 1}: {_gamepad[i].deviceId}");
        }
    }

    void ChengeColor()
    {
        var ctrlJoystick = Input.GetJoystickNames();
        switch (ctrlJoystick.Length)
        {
            case 1:
                ((DualShockGamepad)DualShock4GamepadHID.all[0]).SetLightBarColor(Color.cyan);
                break;
            case 2:
                ((DualShockGamepad)DualShock4GamepadHID.all[0]).SetLightBarColor(Color.cyan);
                ((DualShockGamepad)DualShock4GamepadHID.all[1]).SetLightBarColor(Color.red);
                break;
            case 3:
                ((DualShockGamepad)DualShock4GamepadHID.all[0]).SetLightBarColor(Color.cyan);
                ((DualShockGamepad)DualShock4GamepadHID.all[1]).SetLightBarColor(Color.red);
                ((DualShockGamepad)DualShock4GamepadHID.all[2]).SetLightBarColor(Color.green);
                break;
            case 4:
                ((DualShockGamepad)DualShock4GamepadHID.all[0]).SetLightBarColor(Color.cyan);
                ((DualShockGamepad)DualShock4GamepadHID.all[1]).SetLightBarColor(Color.red);
                ((DualShockGamepad)DualShock4GamepadHID.all[2]).SetLightBarColor(Color.green);
                ((DualShockGamepad)DualShock4GamepadHID.all[3]).SetLightBarColor(Color.yellow);
                break;
        }
        
    }
}
