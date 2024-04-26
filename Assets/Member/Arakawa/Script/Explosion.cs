using System.Collections;
using UnityEngine;

    public class Explosion : MonoBehaviour
    {
        //爆風に当たった時に吹き飛ぶ力
        [SerializeField]
        private float _futtobiPower  = 30;

        //爆風の判定が実際に発生するまでのデイレイ
        [SerializeField]
        private float _startDelaySeconds = 0.1f;

        //爆風の持続フレームすう
        [SerializeField] 
        private int _durationFrameCount = 1;

        //エフェクトを含めてすべての再生が終了するまでの時間
        [SerializeField]
        private float _stopSeconds = 2f;

        [SerializeField]
        private ParticleSystem _effect;

        [SerializeField] 
        private SphereCollider _collider;

        private void Awake()
        {
            _effect.Stop();
            _collider.enabled = false;
        }

        /// <summary>
        /// 爆破する
        /// </summary>
        public void Explode()
        {
            // 当たり判定管理のコルーチン
            StartCoroutine(ExplodeCoroutine());
            // 爆発エフェクト含めてもろもろを消すコルーチン
            StartCoroutine(StopCoroutine());

            _effect.Play();
        }

        private IEnumerator ExplodeCoroutine()
        {
            // 指定秒数が経過するまでFixedUpdate上で待つ
            var delayCount = Mathf.Max(0, _startDelaySeconds);
            while (delayCount > 0)
            {
                yield return new WaitForFixedUpdate();
                delayCount -= Time.fixedDeltaTime;
            }

            // 時間経過したらコライダを有効化して爆発の当たり判定が出る
            _collider.enabled = true;

            // 一定フレーム数有効化
            for (var i = 0; i < _durationFrameCount; i++)
            {
                yield return new WaitForFixedUpdate();
            }

            // 当たり判定無効化
            _collider.enabled = false;
        }

        private IEnumerator StopCoroutine()
        {
            // 時間経過後に消す
            yield return new WaitForSeconds(_stopSeconds);
            _effect.Stop();
            _collider.enabled = false;

            Destroy(gameObject);
        }

        /// <summary>
        /// 爆風にヒットしたときに相手をふっとばす処理
        /// </summary>
        private void OnTriggerEnter(Collider other)
        {
            // 衝突対象がRigidbodyの配下であるかを調べる
            var rigidBody = other.GetComponentInParent<Rigidbody>();

            // Rigidbodyがついてないなら吹っ飛ばないの終わり
            if (rigidBody == null) return;

            // 爆風によって爆発中央から吹き飛ぶ方向のベクトルを作る
            var direction = (other.transform.position - transform.position).normalized;

            // 吹っ飛ばす
            // ForceModeを変えると挙動が変わる（今回は質量無視）
            rigidBody.AddForce(direction * _futtobiPower, ForceMode.VelocityChange);
        }
    }
