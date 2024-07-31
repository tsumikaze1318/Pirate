using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [SerializeField]
    ParticleSystem _particlePrefab;

    Vector3 _hitPos;

    public BoxCollider _collider;
    private void Start()
    {
        this._collider = GetComponent<BoxCollider>();
        _collider.enabled = false;
    }

    void SubCount(Collision other)
    {
        HitCount hitCount = other.gameObject.GetComponent<HitCount>();
        hitCount._count--;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            foreach(ContactPoint point in other.contacts)
            {
                _hitPos = point.point;
            }
            ParticleSystem attackPs = Instantiate(_particlePrefab, _hitPos, Quaternion.identity);
            SubCount(other);
            _collider.enabled = false;
            Destroy(attackPs.gameObject, attackPs.main.duration);
        }
    }
}
