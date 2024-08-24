using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class TestMove : MonoBehaviour
{
    private Rigidbody _rb;
    private Vector3 _move;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log()
    }

    void OnMove(InputValue value)
    {
        var axis = value.Get<Vector2>();
        _move = new Vector3(axis.x, 0, axis.y);
        _rb.velocity = _move * 5f;
    }
}
