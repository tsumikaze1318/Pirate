using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destolr : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        // �Փ˂��������Player�^�O���t���Ă���Ƃ�
        if (collision.gameObject.tag == "Player")
        {
            // 0.2�b��ɏ�����
            Destroy(gameObject, 5f);
        }
    }
}
