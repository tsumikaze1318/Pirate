using UnityEngine;

public class ExecuteShark : MonoBehaviour
{
    [SerializeField]
    private GameObject ThrowingObjectPrefab;

    [SerializeField]
    private GameObject TargetObjectPrefab;

    [SerializeField]
    private float ThroeingAngle;


    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.K)) ThrowingBall();
    }

    public void ThrowingBall()
    {
        if (ThrowingObjectPrefab != null && TargetObjectPrefab != null)
        {
            //オブジェクトの生成
            GameObject Shark = Instantiate(ThrowingObjectPrefab,
            new Vector3(transform.position.x, transform.position.y, transform.position.z), Quaternion.Euler(0f, 90, -30f));
            //標的の座標
            Vector3 targetPrefabPosition = TargetObjectPrefab.transform.position;
            //射出角度
            float angle = ThroeingAngle;
            //射出速度を産出
            Vector3 velocity = CalculateVelocity(this.transform.position, targetPrefabPosition, angle);
            //射出
            Rigidbody rid = Shark.GetComponent<Rigidbody>();
            rid.AddForce(velocity * rid.mass, ForceMode.Impulse);

        }

        Vector3 CalculateVelocity(Vector3 pointA, Vector3 pointB, float angle)
        {
            float rad = angle * Mathf.PI / 150;

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
