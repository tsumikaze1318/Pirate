using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Vector3 _hitPos;
    [SerializeField]
    private ParticleSystem _particlePrefab;

    private BoxCollider _boxCollider;

    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider>();
        _boxCollider.enabled = false;
        _animator = GetComponentInParent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 3)
        {
            _animator.SetTrigger("Accept");
            foreach (ContactPoint point in collision.contacts)
            {
                _hitPos = point.point;
            }
            ParticleSystem attackPs = Instantiate(_particlePrefab, _hitPos, Quaternion.identity);
            SubCount(collision);
            _boxCollider.enabled = false;
            Destroy(attackPs.gameObject, attackPs.main.duration);
        }
    }

    void SubCount(Collision collision)
    {
        HitCount hitCount = collision.gameObject.GetComponent<HitCount>();
        hitCount._count--;
    }
}
