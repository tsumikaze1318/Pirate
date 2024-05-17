using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destolr : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        // 衝突した相手にPlayerタグが付いているとき
        if (collision.gameObject.tag == "Player")
        {
            // 0.2秒後に消える
            Destroy(gameObject, 5f);
        }
    }
}
