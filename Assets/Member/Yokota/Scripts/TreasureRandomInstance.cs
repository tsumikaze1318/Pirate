using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreasureRandomInstance : MonoBehaviour
{
    private static object _lock = new object();

    private static TreasureRandomInstance instance;
    public static TreasureRandomInstance Instance
    {
        get
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    instance
                        = FindObjectOfType<TreasureRandomInstance>();
                    if (instance == null)
                    {
                        var singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<TreasureRandomInstance>();
                        singletonObject.name = nameof(TreasureRandomInstance) + "(singleton)";
                    }
                }

                return instance;
            }
        }
    }

    [SerializeField, Header("宝箱のゲームオブジェクト")]
    private GameObject TreasureBox;

    [SerializeField]
    private TreasureInstanceRanges[] instanceRanges;

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
            //float x = Random.Range(rangeMin.position.x, rangeMax.position.x);
            //float y = Random.Range(rangeMin.position.y, rangeMax.position.y);
            //float z = Random.Range(rangeMin.position.z, rangeMax.position.z);

            //Vector3 pos = new Vector3(x, y, z);

            //// 宝箱がほかのオブジェクトと干渉しないとき
            //if (!Physics.CheckBox(pos, halfExtens, Quaternion.identity))
            //{
            //    // 宝箱を生成する
            //    Instantiate(TreasureBox, pos, Quaternion.identity);
            //    // 生成されたことを確認する
            //    instanced = true;
            //}
        }
    }
}

[System.Serializable]
public struct TreasureInstanceRanges
{
    [SerializeField, EnumIndex(typeof(RangeType))]
    public Transform[] transforms;
}

public enum RangeType
{
    MAX,
    MIN
}
