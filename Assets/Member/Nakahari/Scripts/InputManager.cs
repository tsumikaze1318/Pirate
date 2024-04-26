using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    #region データ保持用のクラス
    public class InputParam
    {
        public float MoveX = 0;
        public float MoveZ = 0;
        public float CamX = 0;
        public float CamY = 0;
        public bool Attack = false;
        public bool Jamp = false;
        public bool Lift = false;
        public bool Grab = false;
        public bool CursorLock = false;
    }
    public class InputTypeString
    {
        public string Horizontal;
        public string Vertical;
        public string CamHorizontal;
        public string CamVertical;
        public string Attack;
        public string Jamp;
        public string Lift;
        public string Grab;
        public string CursorLock;
    }

    #endregion

    #region データテーブル

    /// <summary>
    /// 入力値を保存するテーブル
    /// </summary>
    private Dictionary<CommonParam.UnitType, InputParam> _unitInputParams = new Dictionary<CommonParam.UnitType, InputParam>()
    {
        //PL1インプット
        {CommonParam.UnitType.Player1, new InputParam(){ MoveX = 0, MoveZ = 0, CamX = 0, CamY = 0, Attack = false, Jamp = false, Lift = false, Grab = false, CursorLock = false} },
        //PL2インプット
        {CommonParam.UnitType.Player2, new InputParam(){ MoveX = 0, MoveZ = 0, CamX = 0, CamY = 0, Attack = false, Jamp = false, Lift = false, Grab = false, CursorLock = false} },
        //PL3インプット
        {CommonParam.UnitType.Player3, new InputParam(){ MoveX = 0, MoveZ = 0, CamX = 0, CamY = 0, Attack = false, Jamp = false, Lift = false, Grab = false, CursorLock = false} },
        //STインプット
        {CommonParam.UnitType.Player4, new InputParam(){ MoveX = 0, MoveZ = 0, CamX = 0, CamY = 0, Attack = false, Jamp = false, Lift = false, Grab = false, CursorLock = false} },
    };
    /// <summary>
    /// 入力値の外部参照用
    /// </summary>
    public Dictionary<CommonParam.UnitType, InputParam> UnitInputParams => _unitInputParams;

    private Dictionary<CommonParam.UnitType, InputTypeString> _inputKinds = new Dictionary<CommonParam.UnitType, InputTypeString>()
    {
        {CommonParam.UnitType.Player1, new InputTypeString()    { Horizontal = "PL1Horizontal", Vertical = "PL1Vertical", CamHorizontal = "PL1CamHori", CamVertical = "PL1CamVer", Attack = "PL1Attack", Jamp = "PL1Jamp", Lift = "PL1Lift", CursorLock = "PL1CursorLock", Grab = "PL1Grab"}},
        {CommonParam.UnitType.Player2, new InputTypeString()    { Horizontal = "PL2Horizontal", Vertical = "PL2Vertical", CamHorizontal = "PL2CamHori", CamVertical = "PL2CamVer", Attack = "PL2Attack", Jamp = "PL2Jamp", Lift = "PL2Lift", CursorLock = "PL2CursorLock", Grab = "PL2Grab"}},
        {CommonParam.UnitType.Player3, new InputTypeString()    { Horizontal = "PL3Horizontal", Vertical = "PL3Vertical", CamHorizontal = "PL3CamHori", CamVertical = "PL3CamVer", Attack = "PL3Attack", Jamp = "PL3Jamp", Lift = "PL3Lift", CursorLock = "PL3CursorLock", Grab = "PL3Grab"}},
        {CommonParam.UnitType.Player4, new InputTypeString()    { Horizontal = "PL4Horizontal", Vertical = "PL4Vertical", CamHorizontal = "PL4CamHori", CamVertical = "PL4CamVer", Attack = "PL4Attack", Jamp = "PL4Jamp", Lift = "PL4Lift", CursorLock = "PL4CursorLock", Grab = "PL4Grab"}},
    };

    #endregion

    // Update is called once per frame
    void Update()
    {
        InputCheck(CommonParam.UnitType.Player1);
        InputCheck(CommonParam.UnitType.Player2);
        InputCheck(CommonParam.UnitType.Player3);
        InputCheck(CommonParam.UnitType.Player4);
    }

    public void InputCheck(CommonParam.UnitType unitType)
    {
        var input = _inputKinds[unitType];
        var inputParam = _unitInputParams[unitType];
        inputParam.MoveX = Input.GetAxisRaw(input.Horizontal);
        inputParam.MoveZ = Input.GetAxisRaw(input.Vertical);
        inputParam.CamX = Input.GetAxisRaw(input.CamHorizontal);
        inputParam.CamY = Input.GetAxisRaw(input.CamVertical);
        inputParam.Attack = Input.GetButton(input.Attack);
        //if (inputParam.Attack) inputParam.AttackFlag = true;
        inputParam.Jamp = Input.GetButton(input.Jamp);
        //if (inputParam.Jamp) inputParam.JampFlag = true;
        inputParam.Lift = Input.GetButton(input.Lift);
        //if (inputParam.Lift) inputParam.LiftFlag = true;
        //if(inputParam.Select) inputParam.SelectFlag = true;
        inputParam.CursorLock = Input.GetButton(input.CursorLock);
        inputParam.Grab = Input.GetButton(input.Grab);
    }
}
