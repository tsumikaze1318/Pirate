using System.Threading.Tasks;
using UnityEngine;

public class Bomb3 : MonoBehaviour
{
    [SerializeField] 
    private ParticleSystem explosionParticleSystemPrefab;

    [SerializeField]
    private float explosionForce;

    [SerializeField]
    private float explosionRadius;

    [SerializeField]
    private Renderer _target;

    [SerializeField]
    private float _cycle = 1;

    private double _time;

    private bool _isGrounded = false;

    private bool hasDetonated = false;

    void OnCollisionEnter(UnityEngine.Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground") && !hasDetonated && !_isGrounded) _isGrounded = true;
        //�Փ˂��������Player�^�O���t���Ă���Ƃ�
        if (collision.gameObject.CompareTag("Player") && !hasDetonated && _isGrounded)
        {
            hasDetonated = true;
            Detonate(_isGrounded, 2000);
            //Invoke(nameof(Detonate), 2f);
            //Detonate();
        }
        if (collision.gameObject.CompareTag("Player") && !hasDetonated && !_isGrounded)
        {
            hasDetonated = true;
            Detonate(_isGrounded, 0);
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        //Invoke(nameof(Detonate), 5f);
    }

    async void Detonate(bool isGrounded, int waitSecond)
    {
        await Task.Delay(waitSecond);

        // �p�[�e�B�N���V�X�e���𐶐����Ĕ����G�t�F�N�g���Đ�
        ParticleSystem explosionParticleSystem = Instantiate(explosionParticleSystemPrefab, transform.position, Quaternion.identity);
        SoundManager.Instance.PlaySe(SEType.SE2);
        explosionParticleSystem.Play();

        // �p�[�e�B�N���Đ����Ԃ��I��������p�[�e�B�N���V�X�e����j��
        Destroy(explosionParticleSystem.gameObject, explosionParticleSystem.main.duration);

        // �����͈͓̔��̃I�u�W�F�N�g�����o
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);
        foreach (Collider collider in colliders)
        {
            ApplyExplosionForce(collider, isGrounded);
        }

    }
    
    // ������΂��̏���
    void ApplyExplosionForce(Collider targetCollider, bool isGrounded)
    {
        Rigidbody targetRigidbody = targetCollider.GetComponent<Rigidbody>();
        HitCount hitCount = targetCollider.GetComponent<HitCount>();

        if(hitCount != null) { hitCount._count = 0; }

        if (targetRigidbody != null)
        {
            // ���S����̋����ɉ����ė͂��v�Z
            Vector3 explosionDirection
                = new Vector3(targetCollider.transform.position.x - transform.position.x
                            , 0f
                            , targetCollider.transform.position.z - transform.position.z);
            float distance = explosionDirection.magnitude;
            float normalizedDistance = distance / explosionRadius;
            float force = Mathf.Lerp(explosionForce, 5f, normalizedDistance);

            if (!isGrounded) { explosionDirection = explosionDirection.normalized + new Vector3(0f, 1f, 0f); }

            // �͂�������
            targetRigidbody.AddForce(explosionDirection.normalized * force, ForceMode.Impulse);
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (hasDetonated)
        {
            _time += Time.deltaTime;

            var repeatValue = Mathf.Repeat((float)_time, _cycle);

            _target.enabled = repeatValue >= _cycle * 0.5f;

            //if(hasDetonated)
            //{
            //    _time++;

            //    _target.enabled = repeatValue >= _cycle * 0.2f;
            //}

            if (_time > 3) { Destroy(gameObject); }

            hasDetonated = true;

            

        }


    }
}
