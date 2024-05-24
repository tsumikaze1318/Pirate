using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem explosionParticleSystemPrefab;

    [SerializeField]
    private GameObject ThrowingObjectPrefab;

    [SerializeField]
    private GameObject TargetObjectPrefab;

    //private float time;
    private Vector3 halfExtens = new Vector3(0.5f, 0.5f, 0.5f);

    [SerializeField]
    private GameObject shark;

    [SerializeField]
    private GameObject taget;

    [SerializeField]
    private float ThroeingAngle;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //ThrowingBall();
 
        }

    }

    public void ThrowingBall(Vector3 pos)
    {
        if (ThrowingObjectPrefab != null && TargetObjectPrefab != null)
        {
            // パーティクルシステムを生成してエフェクトを再生
            ParticleSystem explosionParticleSystem = Instantiate(explosionParticleSystemPrefab, transform.position, Quaternion.Euler(50f, -90f, 1.0f));
            explosionParticleSystem.Play();

            // パーティクル再生時間が終了したらパーティクルシステムを破棄
            Destroy(explosionParticleSystem.gameObject, explosionParticleSystem.main.duration);

            //オブジェクトの生成
            GameObject ball = Instantiate(ThrowingObjectPrefab, this.transform.position + pos, Quaternion.identity);
            //標的の座標
            Vector3 targetPrefabPosition = TargetObjectPrefab.transform.position;
            //射出角度
            float angle = ThroeingAngle;
            //射出速度を産出
            Vector3 velocity = CalculateVelocity(this.transform.position, targetPrefabPosition, angle);
            //射出
            Rigidbody rid = ball.GetComponent<Rigidbody>();
            rid.AddForce(velocity * rid.mass, ForceMode.Impulse);
        }

        Vector3 CalculateVelocity(Vector3 pointA, Vector3 pointB, float angle)
        {
            float rad = angle * Mathf.PI / 100;

            float x = Vector2.Distance(new Vector2(pointA.x, pointA.z), new Vector2(pointB.x, pointB.z));
            float y = pointA.y - pointB.y;

            float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));

            if (float.IsNaN(speed))
            {
                return Vector3.zero;
            }
            else
            {
                return (new Vector3(pointB.x - pointA.x, x * Mathf.Tan(rad), pointB.z - pointA.z).normalized * speed);
            }
        }

    }
}

