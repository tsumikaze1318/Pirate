using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteKraken : MonoBehaviour
{
    [SerializeField]
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    public void Attack()
    {
        _animator.SetTrigger("Attack");
    }
}
