using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrakenTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SwitchUnitState(Player player)
    {
        player._state = CommonParam.UnitState.Immovable;
    }
}


//todo:
/*
 SwitchUnitSttateの呼び出すところを決める
クラーケンの攻撃時周りのプレイヤーの吹き飛ぶ処理を作る
クラーケンの攻撃先のUI作成表示
UnityleaRningMaterial
 */