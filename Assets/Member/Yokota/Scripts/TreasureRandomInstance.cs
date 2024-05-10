using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreasureRandomInstance : MonoBehaviour
{
    [SerializeField, Header("宝箱のゲームオブジェクト")]
    private GameObject TreasureBox;

    [SerializeField, Header("生成範囲の始点")]
    private Transform rangeA;
    
    [SerializeField, Header("生成範囲の終点")]
    private Transform rangeB;

    // オブジェクトサイズの半分
    private Vector3 halfExtens = new Vector3(0.5f, 0.5f, 0.5f);

    private void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            RandomInstance();
        }
    }

    /// <summary>
    /// 宝箱を指定された範囲内でほかのオブジェクトに干渉しないよう
    /// ランダムに生成する関数
    /// </summary>
    public void RandomInstance()
    {
        // 宝箱が生成されたか確認するフラグ
        bool instanced = false;

        // 宝箱が生成されるまでループ
        while (!instanced)
        {
            // x, y, z座標を指定された範囲内で決定する
            float x = Random.Range(rangeA.position.x, rangeB.position.x);
            float y = Random.Range(rangeA.position.y, rangeB.position.y);
            float z = Random.Range(rangeA.position.z, rangeB.position.z);

            Vector3 pos = new Vector3(x, y, z);

            // 宝箱がほかのオブジェクトと干渉しないとき
            if (!Physics.CheckBox(pos, halfExtens, Quaternion.identity))
            {
                // 宝箱を生成する
                Instantiate(TreasureBox, pos, Quaternion.identity);
                // 生成されたことを確認する
                instanced = true;
            }
        }
    }
}
