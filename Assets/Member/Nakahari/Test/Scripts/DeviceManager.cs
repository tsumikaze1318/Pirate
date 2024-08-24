using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.DualShock;
using UnityEngine.InputSystem.Users;
using UnityEngine.InputSystem.Utilities;

public class DeviceManager : MonoBehaviour
{

    [SerializeField]
    private int _maxUser;
    private InputUser _user;
    private List<InputUser> _keyMouUser;
    private List<InputUser> _users = new List<InputUser>();
    private List<InputDevice> _deviceList = new List<InputDevice>();
    [SerializeField]
    private InputActionAsset _actionAsset;

    private InputControlList<InputDevice> _inputCtrlList = new InputControlList<InputDevice>();
    private InputDevice _device;

    public static readonly ReadOnlyArray<InputDevice> all;
    [SerializeField]
    GameObject _obj;

    // Start is called before the first frame update
    void Start()
    {

        InputUser.GetUnpairedInputDevices(ref _inputCtrlList);
        var csList = _inputCtrlList.Where(device => device is Gamepad).ToList();
        var keybordDevice = _inputCtrlList.Where(device2 => device2 is Keyboard).ToList();
        var actionMap = _actionAsset;
        //Debug.Log(_inputCtrlList);

        for (int i = 0; i < _maxUser - 1; i++)
        {
            if (i > _inputCtrlList.Count) break;
            if (csList.Count > 0)
            {
                _device = csList[i];
                _user = InputUser.PerformPairingWithDevice(_device);
                _user.AssociateActionsWithUser(actionMap);
            }
            else
            {
                var keybordUser = InputUser.PerformPairingWithDevice(keybordDevice[0]);
            }
            
            
            

            /*var scheme = InputControlScheme.FindControlSchemeForDevice(user.pairedDevices[i], user.actions.controlSchemes);
            if (scheme != null)
                user.ActivateControlScheme(nameof(scheme));*/
        }
        Instantiate(_obj);

    }

    // Update is called once per frame
    void Update()
    {

    }
}
