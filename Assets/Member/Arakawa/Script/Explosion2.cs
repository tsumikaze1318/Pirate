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
            // 爆心地
            Vector3 explosionPos = transform.position;

            // 爆心地を中心として、指定した範囲内にあるオブジェクトのColliderを取得する。
            Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);

            foreach (Collider hit in colliders)
            {
                Rigidbody rb = hit.GetComponent<Rigidbody>();

                if (rb)
                {
                    // 爆発力の発生（爆発の力　＋　爆心地　＋　影響の及ぶ範囲（半径））
                    rb.AddExplosionForce(power, explosionPos, 8.0f);
                }
            }
            Destroy(gameObject, 0.01f);
        }
    }
}