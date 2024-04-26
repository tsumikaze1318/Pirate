//using ExplosionSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //�����܂ł̎���
    [SerializeField]
    private float _time = 3.0f;

    //������prefab
    [SerializeField]
    private Explosion _explosionPrefab;

    private void Start()
    {
        //��莞�Ԍo�ߌ�ɂӂ��Ƃ΂�
        Invoke(nameof(Explode), _time);
    }

    private void Explode()
    {
        //�����𐶐�
        var explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        explosion.Explode();

        Destroy(gameObject);
    }
}
