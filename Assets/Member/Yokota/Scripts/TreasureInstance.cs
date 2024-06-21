using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreasureInstance : MonoBehaviour
{
    private bool _climax = false;

    [SerializeField, Header("宝箱のゲームオブジェクト")]
    private GameObject TreasureBox;

    [SerializeField]
    private List<TreasureInstanceRanges> rangeStruct = new List<TreasureInstanceRanges>();

    private List<TreasureModel> treasureModels = new List<TreasureModel>();

    // オブジェクトサイズの半分
    private Vector3 halfExtens = new Vector3(0.5f, 0.5f, 0.5f);

    private readonly Vector3 preciousBoxPos = new Vector3(0, -7.5f, 23f);

    private const uint maxInstance = 2;

    private void Start()
    {
        for (int i = 0; i < 2;  i++)
        {
            GenerateTreasure();
        }
    }

    /// <summary>
    /// 宝箱を指定された範囲内でほかのオブジェクトに干渉しないよう
    /// ランダムに生成する関数
    /// </summary>
    public void GenerateTreasure(TreasurePlace place = TreasurePlace.Null)
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

        if (!_climax) TreasureGenerate_Normal(place);
        else TreasureGenerate_Climax(place);
    }

    private void TreasureGenerate_Normal(TreasurePlace place)
    {
        bool selectSection = false;
        int section = 0;

        while (!selectSection)
        {
            float rand = Random.Range(0f, 1f);
            
            if (rand < 0.6f) section = 0;
            else if (rand < 0.8f) section = 1;
            else section = 2;

            if (treasureModels.Count == 0) { }
            else if (treasureModels[0].Place == (TreasurePlace)section && section != 0) continue;
            else if (place == (TreasurePlace)section && place != 0) continue;

            selectSection = true;
        }

        CheckOverlapping(section);
    }

    private void TreasureGenerate_Climax(TreasurePlace place)
    {
        bool selectSection = false;

        int section = 0;

        while (!selectSection)
        {
            float rand = Random.Range(0f, 1f);

            if (rand < 0.6f) section = 0;
            else if (rand < 0.75f) section = 1;
            else if (rand < 0.9f) section = 2;
            else section = 3;

            if (treasureModels.Count == 0) { }
            else if (treasureModels[0].Place == (TreasurePlace)section && section != 0) continue;
            else if (place == (TreasurePlace)section && place != 0) continue;

            selectSection = true;
        }

        if (section == 3)
        {
            PreciousBoxGenerate();
            return;
        }
        
        CheckOverlapping(section);
    }

    private void CheckOverlapping(int section)
    {
        // 宝箱が生成されたか確認するフラグ
        bool instanced = false;

        // 宝箱が生成されるまでループ
        while (!instanced)
        {
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

    private void PreciousBoxGenerate()
    {
        var treasure = Instantiate(TreasureBox, preciousBoxPos, Quaternion.identity, transform);

        treasureModels.Add(treasure.GetComponent<TreasureModel>());
        treasureModels[treasureModels.Count - 1].Place = TreasurePlace.Sprit;
    }
    
    public void SetClimax(bool climax) { _climax = climax; }
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
