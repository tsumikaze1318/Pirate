using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class HitCount : MonoBehaviour
{
    [SerializeField]
    public int _count = 3;

    Player _player;

    float _time = 0;

    float _stunTime = 5f;

    [SerializeField]
    ParticleSystem _stunPrefab;

    private bool _effect = false;

    private Animator _animator;

    Vector3 thisPos = Vector3.zero;

    PlayerInput _input;

    private Rigidbody _rb;

    private void Start()
    {
        if( _player == null ) _player = GetComponent<Player>();
        _input = GetComponentInParent<PlayerInput>();
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // プレイヤーの現在の状態によって処理を変更
        if (_player._state == CommonParam.UnitState.Stun)
        {
            _time += Time.deltaTime;
            if (_player._respawn)
            {
                _player._state = CommonParam.UnitState.Normal;
                _count = 3;
                _time = 0;
            }
            if (_time >= _stunTime)
            {
                _player._state = CommonParam.UnitState.Normal;
                _effect = false;
                _count = 3;
                _time = 0;
            }
        }
        thisPos = this.transform.position;
        HitCountor();
    }

    /// <summary>
    /// スタンした際にエフェクトと持っているダイアモンドを落とす処理
    /// </summary>
    public void HitCountor()
    {
        if (_count == 0 && !_effect)
        {
            // プレイヤーの状態を変更
            _player._state = CommonParam.UnitState.Stun;
            // プレイヤーの速度を 0 に変更
            _rb.velocity = Vector3.zero;
            // プレイヤーの回転を 0 に変更
            _rb.angularVelocity = Vector3.zero;
            // プレイヤーのスコアが 0 以上の場合
            if (GameManager.Instance.Scores[_input.user.index] > 0)
            {
                // 生成場所を指定
                Vector3 instantiatePos = thisPos + new Vector3(0, 3f, 0);
                // 生成するオブジェクトを指定
                var treasure = (GameObject)Resources.Load($"Prefab/diamond");
                // 指定した場所にオブジェクトを生成
                var diamond = Instantiate(treasure, instantiatePos, Quaternion.identity);
                // 生成したダイアモンドが持っている Component を取得
                TreasureModel treasureModel = diamond.GetComponent<TreasureModel>();
                // 取得した Script の関数を実行
                treasureModel.ThrowTreasure(instantiatePos);
            }
            // 特定のアニメーションのトリガーを入れる
            _animator.SetTrigger("Stun");
            // 処理やエフェクトが二重で走らない様に bool を true に変更
            _effect = true;
            // GameManager の関数を実行
            GameManager.Instance.SubScore(_input.user.index);
            // パーティクルを生成
            ParticleSystem stun = Instantiate(_stunPrefab, this.transform.position + new Vector3(0, 1.75f, 0), Quaternion.identity, transform);
            StartCoroutine(EffectDestroy(stun));
        }
    }

    /// <summary>
    /// スタンが終わるまで待つエフェクトの削除を待つ
    /// </summary>
    /// <param name="ps"></param>
    /// <returns></returns>
    IEnumerator EffectDestroy(ParticleSystem ps)
    {
        yield return new WaitForSeconds(_stunTime);
        Destroy(ps.gameObject);
    }
}
