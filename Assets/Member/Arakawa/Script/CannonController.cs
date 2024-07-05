using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    // 大砲のスクリプトを格納するリスト
    [SerializeField]
    private List<FireBullet> _fireBullets = new List<FireBullet>();

    private void Start()
    {
        // 念のためリストをクリアする
        _fireBullets.Clear();
        // 子階層に存在する大砲のスクリプトを配列で取得
        var cannons = GetComponentsInChildren<FireBullet>();
        // 配列の長さでループ
        for (int i = 0; i < cannons.Length; i++)
        {
            // リストに格納
            _fireBullets.Add(cannons[i]);
            // 経過時間をiで初期化する
            _fireBullets[i].SetInitialTime(i);
        }
    }
}
