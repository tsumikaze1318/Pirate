using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteKraken : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;
    [SerializeField]
    private Collider[] _colliders;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _colliders = GetComponentsInChildren<Collider>();
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("aaa");
    }
}
