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
    private List<TreasureInstanceRanges> rangeStruct = new List<TreasureInstanceRanges>();

    private List<TreasureModel> treasureModels = new List<TreasureModel>();

    // オブジェクトサイズの半分
    private Vector3 halfExtens = new Vector3(0.5f, 0.5f, 0.5f);

    private const uint maxInstance = 2;

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
    public void RandomInstance(TreasurePlace place = TreasurePlace.Null)
    {
        if (place != TreasurePlace.Null)
        {
            for (int i = 0; i < treasureModels.Count; i++)
            {
                if (treasureModels[i].Place == place)
                {
                    treasureModels.RemoveAt(i);
                }
            }
        }

        if (treasureModels.Count >= maxInstance) return;

        // 宝箱が生成されたか確認するフラグ
        bool instanced = false;

        // 宝箱が生成されるまでループ
        while (!instanced)
        {
            float rand = Random.Range(0f, 1f);

            int section = 0;

            if (rand < 0.6f) section = 0;
            else if (rand < 0.8f) section = 1;
            else section = 2;

            if (treasureModels.Count == 0) { }
            else if (treasureModels[0].Place == (TreasurePlace)section && section != 0) continue;
            else if (place == (TreasurePlace)section && place != 0) continue;

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
                var treasure = Instantiate(TreasureBox, pos, Quaternion.identity, transform);

                treasureModels.Add(treasure.GetComponent<TreasureModel>());
                treasureModels[treasureModels.Count - 1].Place = (TreasurePlace)section;
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
