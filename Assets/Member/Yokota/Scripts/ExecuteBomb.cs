using System.Threading.Tasks;
using UnityEngine;

public class ExecuteBomb : MonoBehaviour
{
    [SerializeField] 
    private ParticleSystem explosionParticleSystemPrefab;

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
        //衝突した相手にPlayerタグが付いているとき
        if (collision.gameObject.CompareTag("Player") && !hasDetonated && _isGrounded)
        {
            hasDetonated = true;
            Detonate(2000);
            //Invoke(nameof(Detonate), 2f);
            //Detonate();
        }
        if (collision.gameObject.CompareTag("Player") && !hasDetonated && !_isGrounded)
        {
            hasDetonated = true;
            Detonate(0);
            Destroy(gameObject);
        }
    }

    async void Detonate(int waitSecond)
    {
        await Task.Delay(waitSecond);

        // パーティクルシステムを生成して爆発エフェクトを再生
        ParticleSystem explosionParticleSystem = Instantiate(explosionParticleSystemPrefab, transform.position, Quaternion.identity);
        SoundManager.Instance.PlaySe(SEType.SE2);
        explosionParticleSystem.Play();

        // パーティクル再生時間が終了したらパーティクルシステムを破棄
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
