using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    // つかみ範囲に入っているオブジェクト格納リスト
    private List<GameObject> canGrabObjects = new List<GameObject>();
    // つかんでいるオブジェクト
    private GameObject grabObject = null;
    // プレイヤー本体
    private GameObject playerObj = null;
    // プレイヤークラス
    private Player player = null;
    // 1フレーム前のプレイヤーのEulerAngle
    private Vector3 preRot;
    // つかんだオブジェクトとプレイヤーとのベクトル
    private Vector3 offset = Vector3.zero;
    // つかみ範囲
    private MeshCollider grabCollider;

    private void Start()
    {
        // Nullチェック
        playerObj ??= GetComponentInParent<Player>().gameObject;
        player ??= GetComponentInParent<Player>();
        grabCollider ??= GetComponent<MeshCollider>();
    }

    private void Update()
    {
        // リスポーン中のときコライダーを無効化、それ以外は有効
        if (player._respawn) grabCollider.enabled = false;
        else grabCollider.enabled = true;

        // 物をつかんでいなければreturn
        if (grabObject == null) return;

        // 今のEulerAngleを取得
        Vector3 nowRot = playerObj.transform.localEulerAngles;
        // 1フレームで変化したy軸の角度のラジアンを計算
        float rad = (nowRot.y - preRot.y) * Mathf.Deg2Rad;

        // ベクトルの回転計算
        float x = offset.x * Mathf.Cos(rad) + offset.z * Mathf.Sin(rad);
        float y = offset.y;
        float z = offset.x * -Mathf.Sin(rad) + offset.z * Mathf.Cos(rad);
        // offsetを更新
        offset = new Vector3(x, y, z);

        // つかんでいるオブジェクトの位置と回転を更新
        grabObject.transform.position = playerObj.transform.position + offset;
        grabObject.transform.localEulerAngles += nowRot - preRot;

        // preRotを更新
        preRot = nowRot;
    }

    /// <summary>
    /// 物をつかむ関数
    /// </summary>
    public void Grab()
    {
        // 物をつかんでいたらreturn
        if (grabObject != null) return;

        // リスト内のnullをすべて削除
        canGrabObjects.RemoveAll(item => item == null);
        // オブジェクトとの距離比較変数
        float minDis = float.MaxValue;

        // つかみ範囲のオブジェクトの数だけループ
        foreach (GameObject obj in canGrabObjects)
        {
            // オブジェクトとの距離を算出
            float distance = Mathf.Abs((obj.transform.position - transform.position).magnitude);
            // 距離が一番近ければ
            if (distance < minDis)
            {
                // minDisを更新
                minDis = distance;
                // つかんでいるオブジェクトを更新
                grabObject = obj;
            }
        }

        // 物をつかんでいたら
        if (grabObject != null)
        {
            // リストをクリア
            canGrabObjects.Clear();
            // プレイヤーとオブジェクトとのベクトルを算出
            offset = grabObject.transform.position - playerObj.transform.position;
            // オブジェクトのEulerAnglesを取得
            preRot = playerObj.transform.localEulerAngles;
            // 中張追記
            Physics.IgnoreCollision(grabObject.GetComponent<Collider>(), player._playerCollider, true);
        }
    }

    /// <summary>
    /// つかんでいるものを手放す関数
    /// </summary>
    public void Release()
    {
        // 物をつかんでいなければreturn
        if (grabObject == null) return;
        // 中張追記
        Physics.IgnoreCollision(grabObject.GetComponent<Collider>(), player._playerCollider, false);
        // つかんでいたオブジェクトの親オブジェクトをnull
        grabObject.transform.parent = null;
        // つかみオブジェクトをnull
        grabObject = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        // 物をつかんでいたらreturn
        if (grabObject != null) return;
        // つかめるオブジェクトでなければreturn
        if (other.gameObject.layer != 6) return;

        // リストに格納
        canGrabObjects.Add(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        // ループのカウント
        int count = 0;

        foreach (GameObject obj in canGrabObjects)
        {
            // 離れたオブジェクトと同じとき
            if (obj == other.gameObject)
            {
                // そのオブジェクトをRemoveして終了
                canGrabObjects.RemoveAt(count);
                break;
            }
            // カウントアップ
            count++;
        }
    }
}
