using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Up : MonoBehaviour
{
    // Start is called before the first frame update

    void OnCollisionStar(Collision collision)
    {
        //�Փ˂��������Player�^�O���t���Ă���Ƃ�
        if (collision.gameObject.CompareTag("Ball"))
        {
            //Detonate();
            Destroy(gameObject, 2f);

            Vector3 forceDirection = new Vector3(1.0f, 1.0f, 0f);

            // ��̌����ɉ����͂̑傫�����`
            float forceMagnitude = 100.0f;

            // �����Ƒ傫������Sphere�ɉ����͂��v�Z����
            Vector3 force = forceMagnitude * forceDirection;

            // Sphere�I�u�W�F�N�g��Rigidbody�R���|�[�l���g�ւ̎Q�Ƃ��擾
            Rigidbody rb = gameObject.GetComponent<Rigidbody>();

            // �͂������郁�\�b�h
            // ForceMode.Impulse�͌���
            rb.AddForce(force, ForceMode.Impulse);


        }
    }
}
