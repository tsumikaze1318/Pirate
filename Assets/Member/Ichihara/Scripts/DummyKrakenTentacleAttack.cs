using System.Collections;
using System.Threading.Tasks;
using UnityEngine;

public class DummyKrakenTentacleAttack : MonoBehaviour
{
    //[SerializeField]
    //private GameObject Marker = null;
    [SerializeField]
    private float distance = 200.0f;

    private float hitColliderRadius = 5f;

    [SerializeField, Header("爆風半径")]
    private float explosionRadius = 10f;
    [SerializeField, Header("爆風の強さ")]
    private float explosionForce = 10f;
    [SerializeField, Header("爆風の上ベクトルの力")]
    private float explosionUpwards = 0f;

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

    /// <summary>
    /// クラーケンの触手が振り降ろされた後に周辺プレイヤーが吹っ飛ばされる
    /// </summary>
    /// <param name="playerTransform">一番近いプレイヤーのトランスフォーム</param>
    /// <returns></returns>
    public async Task AttackTentacle(Transform playerTransform)
    {
        // 触手が海から飛び出るアニメーションを挿入

        var KrakenTentacleManagement = transform.parent.GetComponentInParent<KrakenTentacleManagement>();
        //var centerObject = KrakenTentacleManagement.GenerateMarker(playerTransform);
        // 要待機時間調整
        await Task.Delay(500);
        //Vector3 explosionCenter = centerObject.transform.position;
        //Destroy(centerObject);
        // 触手を振り降ろすアニメーションを挿入
        // 要待機時間調整
        await Task.Yield();
        // center を中心にヒットしたコライダーを格納する
        Collider[] hitColliders = Physics.OverlapSphere(playerTransform.position, hitColliderRadius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            var rb = hitColliders[i].GetComponent<Rigidbody>();
            if (rb)
            {
                // 周囲のオブジェクトに爆風の影響を与える
                rb.AddExplosionForce(explosionForce
                                   , playerTransform.position
                                   , explosionRadius
                                   , explosionUpwards
                                   , ForceMode.Impulse);
            }
        }

        // 触手が海に戻るアニメーションを挿入
        gameObject.SetActive(false);
    }
}
