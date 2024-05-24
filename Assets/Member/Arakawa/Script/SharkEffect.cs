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
            Destroy(gameObject);
        }
    }
    // Start is called before the first frame updat
    // Update is called once per frame
    void Update()
    {
        
    }
    void Detonate()
    {
        // パーティクルシステムを生成して爆発エフェクトを再生
        ParticleSystem explosionParticleSystem = Instantiate(explosionParticleSystemPrefab, transform.position, Quaternion.identity);
        explosionParticleSystem.Play();

        // パーティクル再生時間が終了したらパーティクルシステムを破棄
        Destroy(explosionParticleSystem.gameObject, explosionParticleSystem.main.duration);

    }
}
