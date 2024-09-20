using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class DummyKrakenTentacleAttack : MonoBehaviour
{
    [SerializeField, Header("船が破壊された際のエフェクト")]
    private ParticleSystem _smoke = null;

    [SerializeField, Header("爆風の半径")]
    private float _explosionRadius = 10f;
    [SerializeField, Header("爆風の強さ")]
    private float _explosionForce = 10f;
    [SerializeField, Header("爆風の上ベクトルの力")]
    private float _explosionUpwards = 0f;

    // 爆風の影響を受けるオブジェクトを取得する範囲
    private float _hitColliderRadius = 5f;
    // クラーケンのアニメーション管理
    private Animator _krakenAnimation = null;

    private void Start()
    {
        if (_krakenAnimation == null)
            _krakenAnimation = GetComponent<Animator>();
    }

    /// <summary>
    /// クラーケンの触手が振り降ろされた後に周辺プレイヤーが吹っ飛ばされる
    /// </summary>
    /// <param name="playerTransform">攻撃対象のプレイヤーの座標</param>
    /// <param name="cts"></param>
    /// <returns></returns>
    public async Task AttackTentacle(Transform playerTransform, CancellationTokenSource cts)
    {
        // 攻撃の範囲にプレイヤーがいない場合はアニメーションの再生、攻撃の処理をしない
        if (playerTransform == null) return;
        Vector3 playerPosition = playerTransform.position;

        await Task.Yield();
        // 触手を振り降ろすアニメーションを挿入
        _krakenAnimation.SetTrigger("Attack");
        // 要待機時間調整
        await Task.Delay(4000);
        // playerPositionを中心にヒットしたコライダーを格納する
        Collider[] hitColliders = Physics.OverlapSphere(playerPosition, _hitColliderRadius);
        if (hitColliders.Length > 0)
        {
            for (int i = 0; i < hitColliders.Length; i++)
            {
                var rb = hitColliders[i].GetComponent<Rigidbody>();
                if (rb != null)
                {
                    // 周囲のオブジェクトに爆風の影響を与える
                    rb.AddExplosionForce(_explosionForce
                                       , playerPosition
                                       , _explosionRadius
                                       , _explosionUpwards
                                       , ForceMode.Impulse);
                }
            }
        }
        // クラーケン攻撃時のSE再生
        SoundManager.Instance.PlaySe(SEType.SE7);
        // 爆破地点にエフェクトを再生
        PlayParticle(_smoke, playerTransform);
        await Task.Delay(4000, cts.Token);
        // 待機アニメーションに切り替え
        _krakenAnimation.SetTrigger("Attack");
    }

    /// <summary>
    /// エフェクトを再生する
    /// </summary>
    /// <param name="particle">再生するエフェクト</param>
    /// <param name="PlayPoint">エフェクトを再生する座標</param>
    private void PlayParticle(ParticleSystem particle, Transform PlayPoint)
    {
        ParticleSystem effect = Instantiate(particle, PlayPoint);
        effect.Play();
        Destroy(effect.gameObject);
    }
}
