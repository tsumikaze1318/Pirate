using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Player _player;
    private void Start()
    {
        _player = GetComponentInParent<Player>();
    }

    void SubCount(UnityEngine.Collision other)
    {
        HitCount hitCount = other.gameObject.GetComponent<HitCount>();
        hitCount._count--;
    }

    private void OnCollisionEnter(UnityEngine.Collision other)
    {
        if(other.gameObject.CompareTag("Player") && _player._stateTime >= 1)
        {
            SubCount(other);
        }
    }
}
