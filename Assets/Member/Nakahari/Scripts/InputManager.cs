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
        public bool Jump = false;
        public bool Lift = false;
        public bool LeftGrab = false;
        public bool RightGrab = false;
        public bool CursorLock = false;
    }
    public class InputTypeString
    {
        public string Horizontal;
        public string Vertical;
        public string CamHorizontal;
        public string CamVertical;
        public string Attack;
        public string Jump;
        public string Lift;
        public string LeftGrab;
        public string RightGrab;
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
        {CommonParam.UnitType.Player1, new InputParam(){ MoveX = 0, MoveZ = 0, CamX = 0, CamY = 0, Attack = false, Jump = false, Lift = false, LeftGrab = false, RightGrab = false, CursorLock = false} },
        //PL2インプット                                                                                             u
        {CommonParam.UnitType.Player2, new InputParam(){ MoveX = 0, MoveZ = 0, CamX = 0, CamY = 0, Attack = false, Jump = false, Lift = false, LeftGrab = false, RightGrab = false,CursorLock = false} },
        //PL3インプット                                                                                             u
        {CommonParam.UnitType.Player3, new InputParam(){ MoveX = 0, MoveZ = 0, CamX = 0, CamY = 0, Attack = false, Jump = false, Lift = false, LeftGrab = false, RightGrab = false,CursorLock = false} },
        //STインプット                                                                                              u
        {CommonParam.UnitType.Player4, new InputParam(){ MoveX = 0, MoveZ = 0, CamX = 0, CamY = 0, Attack = false, Jump = false, Lift = false, LeftGrab = false, RightGrab = false, CursorLock = false} },
    };
    /// <summary>
    /// 入力値の外部参照用
    /// </summary>
    public Dictionary<CommonParam.UnitType, InputParam> UnitInputParams => _unitInputParams;

    private Dictionary<CommonParam.UnitType, InputTypeString> _inputKinds = new Dictionary<CommonParam.UnitType, InputTypeString>()
    {
        {CommonParam.UnitType.Player1, new InputTypeString()    { Horizontal = "PL1Horizontal", Vertical = "PL1Vertical", CamHorizontal = "PL1CamHori", CamVertical = "PL1CamVer", Attack = "PL1Attack", Jump = "PL1Jump", Lift = "PL1Lift", CursorLock = "PL1CursorLock", LeftGrab = "PL1LeftGrab", RightGrab = "PL1RightGrab"}},
        {CommonParam.UnitType.Player2, new InputTypeString()    { Horizontal = "PL2Horizontal", Vertical = "PL2Vertical", CamHorizontal = "PL2CamHori", CamVertical = "PL2CamVer", Attack = "PL2Attack", Jump = "PL2Jump", Lift = "PL2Lift", CursorLock = "PL2CursorLock", LeftGrab = "PL2LeftGrab", RightGrab = "PL2RightGrab"}},
        {CommonParam.UnitType.Player3, new InputTypeString()    { Horizontal = "PL3Horizontal", Vertical = "PL3Vertical", CamHorizontal = "PL3CamHori", CamVertical = "PL3CamVer", Attack = "PL3Attack", Jump = "PL3Jump", Lift = "PL3Lift", CursorLock = "PL3CursorLock", LeftGrab = "PL3LeftGrab", RightGrab = "PL3RightGrab"}},
        {CommonParam.UnitType.Player4, new InputTypeString()    { Horizontal = "PL4Horizontal", Vertical = "PL4Vertical", CamHorizontal = "PL4CamHori", CamVertical = "PL4CamVer", Attack = "PL4Attack", Jump = "PL4Jump", Lift = "PL4Lift", CursorLock = "PL4CursorLock", LeftGrab = "PL4LeftGrab", RightGrab = "PL4RightGrab"}},
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
        inputParam.Jump = Input.GetButton(input.Jump);
        //if (inputParam.Jamp) inputParam.JampFlag = true;
        inputParam.Lift = Input.GetButton(input.Lift);
        //if (inputParam.Lift) inputParam.LiftFlag = true;
        //if(inputParam.Select) inputParam.SelectFlag = true;
        inputParam.CursorLock = Input.GetButton(input.CursorLock);
        inputParam.LeftGrab = Input.GetButton(input.LeftGrab);
        inputParam.RightGrab = Input.GetButton(input.RightGrab);
    }
}
