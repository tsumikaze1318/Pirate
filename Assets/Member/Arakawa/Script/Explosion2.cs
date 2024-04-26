using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddForce : MonoBehaviour
{
    public float radius = 10f;
    public float power = 300f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // ���S�n
            Vector3 explosionPos = transform.position;

            // ���S�n�𒆐S�Ƃ��āA�w�肵���͈͓��ɂ���I�u�W�F�N�g��Collider���擾����B
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb)
                {
                    // �����͂̔����i�����̗́@�{�@���S�n�@�{�@�e���̋y�Ԕ͈́i���a�j�j
                    rb.AddExplosionForce(power, explosionPos, 8.0f);
                }
            }
            Destroy(gameObject, 0.01f);
        }
    }
}