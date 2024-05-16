using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ActionType
{
    Grab,
    Strike
}

public class Kraken : MonoBehaviour
{
    // プレイヤーのオブジェクトを格納するList
    [SerializeField]
    private GameObject[] players;

    [SerializeField]
    private List<Material> materials;

    // 列挙型から該当するマテリアルを呼び出すDictionary
    private Dictionary<ActionType, Material> actionTypeToMaterial = new Dictionary<ActionType, Material>();

    // 攻撃範囲UI
    private GameObject cautionUI;

    private void Start()
    {
        // プレイヤータグのついたオブジェクトをすべて取得
        players = GameObject.FindGameObjectsWithTag("Player");

        for (int i = 0; i < Enum.GetNames(typeof(ActionType)).Length; i++) 
        {
            actionTypeToMaterial.Add(ActionType.Strike, materials[i]);
        }
    }

    private void Update()
    {
        
    }
}
