using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class TreasureInstance : MonoBehaviour
{
    // レア宝箱生成フラグ
    private bool _climax = false;
    [SerializeField, Header("お宝のオブジェクトリスト")]
    private GameObject[] Treasure;
    // 宝箱生成範囲のリスト
    [SerializeField, EnumIndex(typeof(RangeSection))]
    private List<TreasureInstanceRanges> rangeStruct 
        = new List<TreasureInstanceRanges>();
    // 宝箱クラスのリスト
    private List<TreasureModel> treasureModels 
        = new List<TreasureModel>();
    // 宝箱の大きさの半分
    private Vector3 halfExtens 
        = new Vector3(0.5f, 0.5f, 0.5f);
    // 高ポイント宝箱生成位置
    private readonly Vector3 preciousBoxPos 
        = new Vector3(0, -5f, 27f);
    // 宝箱を生成する数の上限
    private const uint maxInstance = 2;

    private void Start()
    {
        // シーンが呼び出されたときに宝箱を二つ生成
        for (int i = 0; i < 2;  i++)
        {
            GenerateTreasure();
        }
    }

    /// <summary>
    /// 宝箱生成関数
    /// </summary>
    /// <param name="place">直近に獲得された宝箱の生成区画</param>
    public void GenerateTreasure(TreasurePlace place = TreasurePlace.Null)
    {
        // 生成区画が指定されているとき
        if (place != TreasurePlace.Null)
        {
            // 宝箱のリストの数だけ繰り返す
            for (int i = 0; i < treasureModels.Count; i++)
            {
                // リスト内の呼び出した宝箱をリストから消す
                if (treasureModels[i].Place == place)
                {
                    treasureModels.RemoveAt(i);
                }
            }
        }
        
        // 宝箱の数が生成上限に達していたら終了
        if (treasureModels.Count >= maxInstance) return;

        // レア宝箱生成フラグが立っていないとき
        if (!_climax) TreasureGenerate_Normal(place);
        else TreasureGenerate_Climax(place);
    }

    /// <summary>
    /// 通常時に呼び出される宝箱生成関数
    /// </summary>
    /// <param name="place">直近に獲得された宝箱の生成区画</param>
    private void TreasureGenerate_Normal(TreasurePlace place)
    {
        // 生成区画確定フラグ
        bool selectSection = false;
        // 生成区画を指定する番号
        int section = 0;

        // フラグが真になるまでループ
        while (!selectSection)
        {
            // 0~1までのランダムなフロート値
            float rand = Random.Range(0f, 1f);
            
            // randが0.6未満のときは中央
            if (rand < 0.6f) section = 0;
            // randが0.6以上0.8未満のときは船首
            else if (rand < 0.8f) section = 1;
            // それ以外のときは船尾に生成
            else section = 2;

            // 宝箱がない時はスルー
            if (treasureModels.Count == 0) { }
            // 宝箱がその生成位置に既に存在してかつ生成位置が中央以外のときもう一度ループ
            else if (treasureModels[0].Place == (TreasurePlace)section && section != 0) continue;
            // 直近に獲得された宝箱の区画と生成区画が同じかつ生成位置が中央以外のときもう一度ループ
            else if (place == (TreasurePlace)section && place != 0) continue;

            // フラグを上げてループを終了
            selectSection = true;
        }

        // 重なりチェック
        CheckOverlapping(section);
    }

    /// <summary>
    /// レア宝箱生成フラグが上がっている時に呼び出される宝箱生成関数
    /// </summary>
    /// <param name="place">直近に獲得された宝箱の生成区画</param>
    private void TreasureGenerate_Climax(TreasurePlace place)
    {
        // 生成区画確定フラグ
        bool selectSection = false;
        // 生成区画を指定する番号
        int section = 0;

        // フラグが真になるまでループ
        while (!selectSection)
        {
            // 0~1までのランダムなフロート値
            float rand = Random.Range(0f, 1f);

            // randが0.6未満のときは中央
            if (rand < 0.6f) section = 0;
            // randが0.6以上0.75未満のときは船首
            else if (rand < 0.75f) section = 1;
            // randが0.75以上0.9未満のときは船尾
            else if (rand < 0.9f) section = 2;
            // それ以外のときは船の先端に生成
            else section = 3;

            // 宝箱がない時はスルー
            if (treasureModels.Count == 0) { }
            // 宝箱がその生成位置に既に存在してかつ生成位置が中央以外のときもう一度ループ
            else if (treasureModels[0].Place == (TreasurePlace)section && section != 0) continue;
            // 直近に獲得された宝箱の区画と生成区画が同じかつ生成位置が中央以外のときもう一度ループ
            else if (place == (TreasurePlace)section && place != 0) continue;

            // フラグを上げてループを終了する
            selectSection = true;
        }

        // 船の先端に生成するとき
        if (section == 3)
        {
            // レア宝箱を生成して終了
            PreciousBoxGenerate();
            return;
        }

        // 重なりチェック
        CheckOverlapping(section);
    }

    /// <summary>
    /// 宝箱にほかのオブジェクトが重なっていないか確認する関数
    /// </summary>
    /// <param name="section">宝箱生成区画を指定する番号</param>
    private async void CheckOverlapping(int section)
    {
        // 宝箱が生成されたか確認するフラグ
        bool instanced = false;

        // 宝箱が生成されるまでループ
        while (!instanced)
        {
            // x,y,zを指定された場所の範囲内でランダムに決定
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

            // 宝箱が生成される場所にオブジェクトがないとき
            if (!Physics.CheckBox(pos, halfExtens, Quaternion.identity))
            {
                await Task.Delay(2000);

                // 宝箱を生成
                var treasure = Instantiate(Treasure[0], pos, Quaternion.identity, transform);

                // リストに追加
                treasureModels.Add(treasure.GetComponent<TreasureModel>());
                // 追加した宝箱の区画を設定
                treasureModels[treasureModels.Count - 1].Place = (TreasurePlace)section;
                // 生成されたことを確認し、ループを終了
                instanced = true;
            }
        }
    }

    /// <summary>
    /// レア宝箱生成関数
    /// </summary>
    private void PreciousBoxGenerate()
    {
        // 指定の場所に生成
        var treasure = Instantiate(Treasure[1], preciousBoxPos, Quaternion.identity, transform);

        // リストに追加
        treasureModels.Add(treasure.GetComponent<TreasureModel>());
        // 追加した宝箱の区画を設定
        treasureModels[treasureModels.Count - 1].Place = TreasurePlace.Sprit;
    }
    
    /// <summary>
    /// レア宝箱生成フラグを設定する関数
    /// </summary>
    /// <param name="climax"></param>
    public void SetClimax(bool climax) { _climax = climax; }
}

/// <summary>
/// 宝箱生成範囲を格納する構造体
/// </summary>
[System.Serializable]
public struct TreasureInstanceRanges
{
    // 生成範囲の始点、終点の位置情報のリスト
    [SerializeField, EnumIndex(typeof(RangeType))]
    public List<Transform> Ranges;
}

/// <summary>
/// 生成範囲の名前
/// </summary>
public enum RangeSection
{
    Mid,
    Fore,
    Back
}

/// <summary>
/// 生成範囲の始点、終点
/// </summary>
public enum RangeType
{
    MIN,
    MAX
}
