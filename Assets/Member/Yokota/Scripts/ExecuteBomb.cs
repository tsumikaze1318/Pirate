using System.Threading.Tasks;
using UnityEngine;

public class ExecuteBomb : MonoBehaviour
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

    async void Detonate(bool isGrounded, int waitSecond)
    {
        await Task.Delay(waitSecond);

        // �p�[�e�B�N���V�X�e���𐶐����Ĕ����G�t�F�N�g���Đ�
        ParticleSystem explosionParticleSystem = Instantiate(explosionParticleSystemPrefab, transform.position, Quaternion.identity);
        SoundManager.Instance.PlaySe(SEType.SE2);
        explosionParticleSystem.Play();

        // �p�[�e�B�N���Đ����Ԃ��I��������p�[�e�B�N���V�X�e����j��
        Destroy(explosionParticleSystem.gameObject, explosionParticleSystem.main.duration);
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
