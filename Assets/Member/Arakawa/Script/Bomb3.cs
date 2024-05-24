using UnityEngine;

public class Bomb3 : MonoBehaviour
{
    [SerializeField] 
    private ParticleSystem explosionParticleSystemPrefab;
    [SerializeField]
    private float explosionForce;
    [SerializeField]
    private float explosionRadius;

    private bool _isGrounded = false;

    private bool hasDetonated = false;

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !hasDetonated && !_isGrounded) _isGrounded = true;
        // �Փ˂��������Player�^�O���t���Ă���Ƃ�
        else if (collision.gameObject.CompareTag("Player") && !hasDetonated && _isGrounded)
        {
            hasDetonated = true;
            Invoke(nameof(Detonate), 2f);
            //Detonate();
            Destroy(gameObject,2f);
        }
        else if (collision.gameObject.CompareTag("Player") && !hasDetonated && !_isGrounded)
        {
            hasDetonated = true;
            Detonate();
            Destroy(gameObject);
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
        HitCount hitCount = targetCollider.GetComponent<HitCount>();

        if(hitCount != null) { hitCount._count = 0; }

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
