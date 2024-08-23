using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HitCount : MonoBehaviour
{
    [SerializeField]
    public int _count = 3;

    Player _player;

    float _time = 0;

    float _stunTime = 1;

    private void Start()
    {
        if( _player == null ) _player = GetComponent<Player>();
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
                _count = 3;
                _time = 0;
            }
        }
        HitCountor();
    }

    public void HitCountor()
    {
        if (_count == 0)
        {
            _player._state = CommonParam.UnitState.Stun;
        }
    }
}
