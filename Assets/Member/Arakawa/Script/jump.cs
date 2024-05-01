using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jump : MonoBehaviour
{
    [SerializeField]
    float jumpForce = 220f;
    float speed;
    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        speed = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            this.rb.AddForce(transform.up * this.jumpForce);
        }
        transform.Translate(0f, speed * Time.deltaTime, 0f);
        transform.Translate(speed * Time.deltaTime, 0f, 0f);

    }
}