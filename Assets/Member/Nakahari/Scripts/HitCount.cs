using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitCount : MonoBehaviour
{
    public int _count = 3;

    Player _player;

    float _time = 0;

    private void Start()
    {
        if( _player == null ) _player = GetComponent<Player>();
    }

    private void Update()
    {
        if (_player._state == CommonParam.UnitState.Stun)
        {
            _time += Time.deltaTime;
            if (_time >= 4)
            {
                _player._state = CommonParam.UnitState.Normal;
                _count = 3;
                _time = 0;
            }
        }
        HitCountor();
        Debug.Log(_count);
    }
    public void HitCountor()
    {
        if (_count == 0)
        {
            _player._state = CommonParam.UnitState.Stun;
        }
    }
}
