using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerModelFly : MonoBehaviour
{
    private Rigidbody _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
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
            _rigidbody.AddForce(new Vector3(0, 5, 10), ForceMode.Impulse);
        }
    }
}
