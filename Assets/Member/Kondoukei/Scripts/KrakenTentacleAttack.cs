using System.Collections;
using UnityEngine;

public class KrakenTentacleAttack : MonoBehaviour
{
    [SerializeField]
    private GameObject Marker;
    [SerializeField]
    private float distance = 200.0f;

    private float hitColliderRadius = 5f;

    [SerializeField, Header("爆風半径")]
    private float explosionRadius = 10f;
    [SerializeField, Header("爆風の強さ")]
    private float explosionForce = 10f;
    [SerializeField, Header("爆風の上ベクトルの力")]
    private float explosionUpwards = 0f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // テストコード
        /*
        if (Input.GetMouseButtonUp(0))
        {
            StartCoroutine(ExplosionWind());
        }
        */

    }

    /// <summary>
    /// 触手を振り下ろす座標にマーカーUIを表示
    /// </summary>
    /// <returns></returns>
    private GameObject GenerateMarker()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        GameObject marker = null;
        if (Physics.Raycast(ray, out hit, distance))
        {
            marker = Instantiate(Marker, hit.point, Quaternion.identity);
        }
        return marker;
    }

    /// <summary>
    /// クラーケンの触手が振り降ろされた後に周辺プレイヤーが吹っ飛ばされる
    /// </summary>
    /// <returns></returns>
    private IEnumerator ExplosionWind()
    {
        GameObject centerObject = GenerateMarker();
        yield return new WaitForSeconds(3);
        Vector3 explosionCenter = centerObject.transform.position;
        Destroy(centerObject);
        yield return null;
        // center を中心にヒットしたコライダーを格納する
        Collider[] hitColliders = Physics.OverlapSphere(explosionCenter, hitColliderRadius);
        for (int i = 0; i < hitColliders.Length; i++)
        {
            var rb = hitColliders[i].GetComponent<Rigidbody>();
            if (rb)
            {
                rb.AddExplosionForce(explosionForce
                                   , explosionCenter
                                   , explosionRadius
                                   , explosionUpwards
                                   , ForceMode.Impulse);
            }
        }
    }
}
