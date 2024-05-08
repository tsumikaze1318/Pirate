using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb3 : MonoBehaviour
{
    [SerializeField] 
    private ParticleSystem explosionParticleSystemPrefab;
    [SerializeField]
    private float explosionForce;
    [SerializeField]
    private float explosionRadius;

    private bool hasDetonated = false;

    void OnCollisionEnter(Collision collision)
    {
        // 衝突した相手にPlayerタグが付いているとき
        if (collision.gameObject.CompareTag("Player") && !hasDetonated)
        {
            hasDetonated = true;
            Invoke(nameof(Detonate), 5f);
            Destroy(gameObject,5f);
        }
    }

    private void Start()
    {
        //Invoke(nameof(Detonate), 5f);
    }

    void Detonate()
    {
        // パーティクルシステムを生成して爆発エフェクトを再生
        ParticleSystem explosionParticleSystem = Instantiate(explosionParticleSystemPrefab, transform.position, Quaternion.identity);
        explosionParticleSystem.Play();

        // パーティクル再生時間が終了したらパーティクルシステムを破棄
        Destroy(explosionParticleSystem.gameObject, explosionParticleSystem.main.duration);

        // 爆風の範囲内のオブジェクトを検出
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            ApplyExplosionForce(collider);
        }

    }

    // 吹き飛ばしの処理
    void ApplyExplosionForce(Collider targetCollider)
    {
        Rigidbody targetRigidbody = targetCollider.GetComponent<Rigidbody>();

        if (targetRigidbody != null)
        {
            // 爆心からの距離に応じて力を計算
            Vector3 explosionDirection = targetCollider.transform.position - transform.position;
            float distance = explosionDirection.magnitude;
            float normalizedDistance = distance / explosionRadius;
            float force = Mathf.Lerp(explosionForce, 0f, normalizedDistance);

            // 力を加える
            targetRigidbody.AddForce(explosionDirection.normalized * force, ForceMode.Impulse);
        }
    }
}
