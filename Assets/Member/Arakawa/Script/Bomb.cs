//using ExplosionSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //爆発までの時間
    [SerializeField]
    private float _time = 3.0f;

    //爆風のprefab
    [SerializeField]
    private Explosion _explosionPrefab;

    private void Start()
    {
        //一定時間経過後にふきとばす
        Invoke(nameof(Explode), _time);
    }

    private void Explode()
    {
        //爆発を生成
        var explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        explosion.Explode();

        Destroy(gameObject);
    }
}
