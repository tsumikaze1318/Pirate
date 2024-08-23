using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Up : MonoBehaviour
{
    // Start is called before the first frame update

    void OnCollisionStar(Collision collision)
    {
        //衝突した相手にPlayerタグが付いているとき
        if (collision.gameObject.CompareTag("Ball"))
        {
            //Detonate();
            Destroy(gameObject, 2f);

            Vector3 forceDirection = new Vector3(1.0f, 1.0f, 0f);

            // 上の向きに加わる力の大きさを定義
            float forceMagnitude = 100.0f;

            // 向きと大きさからSphereに加わる力を計算する
            Vector3 force = forceMagnitude * forceDirection;

            // SphereオブジェクトのRigidbodyコンポーネントへの参照を取得
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();

            // 力を加えるメソッド
            // ForceMode.Impulseは撃力
            rb.AddForce(force, ForceMode.Impulse);


        }
    }
}
