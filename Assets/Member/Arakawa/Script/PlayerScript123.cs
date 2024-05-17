using UnityEngine;

public class PlayerScript123 : MonoBehaviour
{
    float speed = 3.0f;

    void Update()
    {
        // W�L�[�i�O���ړ��j
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += speed * transform.forward * Time.deltaTime;
        }

        // S�L�[�i����ړ��j
        if (Input.GetKey(KeyCode.W))
        {
            transform.position -= speed * transform.forward * Time.deltaTime;
        }

        // D�L�[�i�E�ړ��j
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += speed * transform.right * Time.deltaTime;
        }

        // A�L�[�i���ړ��j
        if (Input.GetKey(KeyCode.D))
        {
            transform.position -= speed * transform.right * Time.deltaTime;
        }
    }
}