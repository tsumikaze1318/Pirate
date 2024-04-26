//using ExplosionSample;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    //”š”­‚Ü‚Å‚ÌŠÔ
    [SerializeField]
    private float _time = 3.0f;

    //”š•—‚Ìprefab
    [SerializeField]
    private Explosion _explosionPrefab;

    private void Start()
    {
        //ˆê’èŠÔŒo‰ßŒã‚É‚Ó‚«‚Æ‚Î‚·
        Invoke(nameof(Explode), _time);
    }

    private void Explode()
    {
        //”š”­‚ğ¶¬
        var explosion = Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        explosion.Explode();

        Destroy(gameObject);
    }
}
