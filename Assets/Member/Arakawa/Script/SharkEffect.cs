using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkEffect : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem explosionParticleSystemPrefab;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Detonate();
            //Destroy(gameObject);
        }
    }

    void Detonate()
    {
        // パーティクルシステムを生成して爆発エフェクトを再生
        ParticleSystem explosionParticleSystem = Instantiate(explosionParticleSystemPrefab, transform.position, Quaternion.Euler(50f, -90f, 1.0f));
        explosionParticleSystem.Play();

        // パーティクル再生時間が終了したらパーティクルシステムを破棄
        Destroy(explosionParticleSystem.gameObject, explosionParticleSystem.main.duration);
    }

}
