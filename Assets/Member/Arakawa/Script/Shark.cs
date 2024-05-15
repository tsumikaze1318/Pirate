using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shark : MonoBehaviour
{
    [SerializeField]
    private GameObject ThrowingObject;

    [SerializeField]
    private GameObject TargetObject;

    [SerializeField]
    private float ThroeingAngle;


    // Start is called before the first frame update
    void Start()
    {
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ThrowingBall();
        }
    }

    private void ThrowingBall()
    {
        if(ThrowingObject != null && TargetObject != null)
        {
            //Ballオブジェクトの生成
            GameObject ball = Instantiate(ThrowingObject, this.transform.position, Quaternion.identity);
            //標的の座標
            Vector3 targetPosition = TargetObject.transform.position;
            //射出角度
            float angle = ThroeingAngle;
            //射出速度を産出
            Vector3 velocity = CalculateVelocity(this.transform.position, targetPosition, angle);
            //射出
            Rigidbody rid = ball.GetComponent<Rigidbody>();
            rid.AddForce(velocity * rid.mass, ForceMode.Impulse);

        }
        else
        {
            throw new System.Exception("射出するオブジェクト");
        }
    }

    private Vector3 CalculateVelocity(Vector3 pointA, Vector3 pointB, float angle)
    {
        float rad = angle * Mathf.PI / 100;

        float x = Vector2.Distance(new Vector2(pointA.x, pointA.z), new Vector2(pointB.x, pointB.z));
        float y = pointA.y - pointB.y;

        float speed = Mathf.Sqrt(-Physics.gravity.y * Mathf.Pow(x, 2) / (2 * Mathf.Pow(Mathf.Cos(rad), 2) * (x * Mathf.Tan(rad) + y)));

        if(float.IsNaN(speed))
        {
            return Vector3.zero;
        }
        else
        {
            return (new Vector3(pointB.x - pointA.x, x * Mathf.Tan(rad), pointB.z - pointA.z).normalized * speed);
        }
    }
}
