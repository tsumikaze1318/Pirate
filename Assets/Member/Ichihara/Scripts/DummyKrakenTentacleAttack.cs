using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class DummyKrakenTentacleAttack : MonoBehaviour
{
    [SerializeField, Header("爆風の半径")]
    private float explosionRadius = 10f;
    [SerializeField, Header("爆風の強さ")]
    private float explosionForce = 10f;
    [SerializeField, Header("爆風の上ベクトルの力")]
    private float explosionUpwards = 0f;

    // 爆風の影響を受けるオブジェクトを取得する範囲
    private float hitColliderRadius = 2f;
    // クラーケンのアニメーション管理
    private Animator krakenAnimation = null;

    private void Start()
    {
        if (krakenAnimation == null)
            krakenAnimation = GetComponent<Animator>();
    }

    /// <summary>
    /// クラーケンの触手が振り降ろされた後に周辺プレイヤーが吹っ飛ばされる
    /// </summary>
    /// <param name="playerTransform">攻撃対象のプレイヤーの座標</param>
    /// <returns></returns>
    public async Task AttackTentacle(Transform playerTransform)
    {
        // 攻撃の範囲にプレイヤーがいない場合はアニメーションの再生、攻撃の処理をしない
        if (playerTransform == null) return;
        Vector3 playerPosition = playerTransform.position;

        await Task.Yield();
        // 触手を振り降ろすアニメーションを挿入
        krakenAnimation.SetTrigger("Attack");
        // 要待機時間調整
        await Task.Delay(4000);
        // center を中心にヒットしたコライダーを格納する
        Collider[] hitColliders = Physics.OverlapSphere(playerPosition, hitColliderRadius);
        if (hitColliders.Length > 0)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                var rb = hitColliders[i].GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // 周囲のオブジェクトに爆風の影響を与える
                    rb.AddExplosionForce(explosionForce
                                       , playerPosition
                                       , explosionRadius
                                       , explosionUpwards
                                       , ForceMode.Impulse);
                }
            }
        }
        // クラーケン攻撃時のSE再生
        SoundManager.Instance.PlaySe(SEType.SE7);
        await Task.Delay(4000);
        // 待機アニメーションに切り替え
        krakenAnimation.SetTrigger("Attack");
        // TODO: β版後適用
        //gameObject.SetActive(false);
    }

    /// <summary>
    /// 触手を振り下ろす座標にマーカーUIを表示
    /// </summary>
    /// <returns></returns>
    //private GameObject GenerateMarker(Transform transform)
    //{
    //Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
    //RaycastHit hit;
    //GameObject markerObject = null;
    //if (Physics.Raycast(ray, out hit, distance))
    //{
    //    markerObject = Instantiate(Marker, hit.point, Quaternion.identity);
    //}

    //}
}
