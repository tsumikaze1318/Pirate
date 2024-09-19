using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelVanish : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Animator _animator;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _animator = GetComponent<Animator>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Ground")
        {
            _rigidbody.AddForce(new Vector3(0, 10, 10), ForceMode.Impulse);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag != "Ground")
        {
            _rigidbody.AddForce(new Vector3(0, 5, 20), ForceMode.Impulse);
        }
    }

    public void DoWinAnimation()
    {
        _animator.SetBool("Win", true);
    }
}
