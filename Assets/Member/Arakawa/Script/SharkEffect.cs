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
        // �p�[�e�B�N���V�X�e���𐶐����Ĕ����G�t�F�N�g���Đ�
        ParticleSystem explosionParticleSystem = Instantiate(explosionParticleSystemPrefab, transform.position, Quaternion.identity);
        explosionParticleSystem.Play();

        // �p�[�e�B�N���Đ����Ԃ��I��������p�[�e�B�N���V�X�e����j��
        Destroy(explosionParticleSystem.gameObject, explosionParticleSystem.main.duration);

    }
}
