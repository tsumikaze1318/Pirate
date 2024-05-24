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
    private List<TreasureInstanceRanges> rangeStruct;

    // オブジェクトサイズの半分
    private Vector3 halfExtens = new Vector3(0.5f, 0.5f, 0.5f);

    private void Start()
    {
        for (int i = 0; i < 2;  i++)
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
            int section = Random.Range(0, rangeStruct.Count);

            float x = Random.Range
                (rangeStruct[section].Ranges[(int)RangeType.MIN].position.x,
                 rangeStruct[section].Ranges[(int)RangeType.MAX].position.x);
            float y = Random.Range
                (rangeStruct[section].Ranges[(int)RangeType.MIN].position.y,
                 rangeStruct[section].Ranges[(int)RangeType.MAX].position.y);
            float z = Random.Range
                (rangeStruct[section].Ranges[(int)RangeType.MIN].position.z,
                 rangeStruct[section].Ranges[(int)RangeType.MAX].position.z);

            Vector3 pos = new Vector3(x, y, z);

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

[System.Serializable]
public struct TreasureInstanceRanges
{
    [SerializeField, EnumIndex(typeof(RangeType))]
    public List<Transform> Ranges;
}

public enum RangeType
{
    MIN,
    MAX
}
