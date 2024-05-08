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
        // �Փ˂��������Player�^�O���t���Ă���Ƃ�
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
        // �p�[�e�B�N���V�X�e���𐶐����Ĕ����G�t�F�N�g���Đ�
        ParticleSystem explosionParticleSystem = Instantiate(explosionParticleSystemPrefab, transform.position, Quaternion.identity);
        explosionParticleSystem.Play();

        // �p�[�e�B�N���Đ����Ԃ��I��������p�[�e�B�N���V�X�e����j��
        Destroy(explosionParticleSystem.gameObject, explosionParticleSystem.main.duration);

        // �����͈͓̔��̃I�u�W�F�N�g�����o
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            ApplyExplosionForce(collider);
        }

    }

    // ������΂��̏���
    void ApplyExplosionForce(Collider targetCollider)
    {
        Rigidbody targetRigidbody = targetCollider.GetComponent<Rigidbody>();

        if (targetRigidbody != null)
        {
            // ���S����̋����ɉ����ė͂��v�Z
            Vector3 explosionDirection = targetCollider.transform.position - transform.position;
            float distance = explosionDirection.magnitude;
            float normalizedDistance = distance / explosionRadius;
            float force = Mathf.Lerp(explosionForce, 0f, normalizedDistance);

            // �͂�������
            targetRigidbody.AddForce(explosionDirection.normalized * force, ForceMode.Impulse);
        }
    }
}
