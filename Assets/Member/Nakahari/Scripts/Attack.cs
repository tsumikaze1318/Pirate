using UnityEngine;

public class Attack : MonoBehaviour
{
    private Vector3 _hitPos;

    [SerializeField]
    private ParticleSystem _particlePrefab;
    private BoxCollider _boxCollider;
    [SerializeField]
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.enabled = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            // 触れた相手の Animator を取得
            _animator = collision.gameObject.GetComponent<Animator>();
            // foreach で触れた場所を取得
            foreach (ContactPoint point in collision.contacts)
            {
                // 触れた場所を格納
                _hitPos = point.point;
            }
            // 触れた場所にパーティクルを出現
            ParticleSystem attackPs = Instantiate(_particlePrefab, _hitPos, Quaternion.identity);
            SubCount(collision);
            // 連続で判定を取らない様にコライダーを無効化
            _boxCollider.enabled = false;
            // パーティクルの再生が終わり次第削除
            Destroy(attackPs.gameObject, attackPs.main.duration);
        }
    }

    /// <summary>
    /// 触れた相手のカウントを減らす。1以下になった際に特定のアニメーションを走らせる
    /// </summary>
    /// <param name="collision"></param>
    void SubCount(Collision collision)
    {
        HitCount hitCount = collision.gameObject.GetComponent<HitCount>();
        hitCount._count--;
        if(hitCount._count >= 1)
        {
            _animator.SetTrigger("Accept");
        }
    }
}
