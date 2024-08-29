using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;

public class DeviceManager : MonoBehaviour
{
    public static DeviceManager Instance { get; private set; }

    public List<int> InputDeviceIndex { get; private set; } = new List<int>();

    // Start is called before the first frame update
    void Start()
    {
        var ctrlIndex = Input.GetJoystickNames();
        InputDeviceIndex.Add(ctrlIndex.Length);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
