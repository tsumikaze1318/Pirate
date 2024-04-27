using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    [SerializeField]
    private float speed = 1.0f;
    [SerializeField]
    private int point = 0;
    public int Point => point;

    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
        if (rb == null)
            rb = gameObject.AddComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        var moveY = Input.GetAxis("Vertical");
        var moveX = Input.GetAxis("Horizontal");

        Vector3 move = new Vector3(moveX, 0f, moveY);
        transform.position += speed * Time.deltaTime * move;

        rb.AddForce(move);



        /*
        // ÉvÉåÉCÉÑÅ[ÇÃà⁄ìÆ
        if (Input.GetKey(KeyCode.W))
        {
            //transform.Translate(speed * Time.deltaTime * Vector3.forward);
            transform.position += speed * Time.deltaTime * Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            //transform.Translate(speed * Time.deltaTime * Vector3.forward);
            transform.position += speed * Time.deltaTime * Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            //transform.Translate(speed * Time.deltaTime * Vector3.forward);
            transform.position += speed * Time.deltaTime * Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            //transform.Translate(speed * Time.deltaTime * Vector3.forward);
            transform.position += speed * Time.deltaTime * Vector3.right;
        }
        */
    }
}