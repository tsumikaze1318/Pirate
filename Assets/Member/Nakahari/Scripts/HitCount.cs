using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class HitCount : MonoBehaviour
{
    [SerializeField]
    public int _count = 3;

    Player _player;

    float _time = 0;

    float _stunTime =4.5f;

    [SerializeField]
    ParticleSystem _stunPrefab;

    private bool _effect = false;

    private Animator _animator;

    private void Start()
    {
        if( _player == null ) _player = GetComponent<Player>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
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
        HitCountor();
    }

    public void HitCountor()
    {
        if (_count == 0 && !_effect)
        {
            _player._state = CommonParam.UnitState.Stun;
            //_animator.SetTrigger("Stun");
            _effect = true;
            ParticleSystem stun = Instantiate(_stunPrefab, this.transform.position + new Vector3(0, 1.75f, 0), Quaternion.identity);
            StartCoroutine(EffectDestroy(stun));
        }
    }

    IEnumerator EffectDestroy(ParticleSystem ps)
    {
        yield return new WaitForSeconds(_stunTime);
        Destroy(ps.gameObject);
    }
}
