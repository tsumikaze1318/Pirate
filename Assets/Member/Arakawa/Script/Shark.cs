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

    [SerializeField]
    private float ThroeingAngle;

    //private float time;
    private Vector3 halfExtens = new Vector3(0.5f, 0.5f, 0.5f);


    // Start is called before the first frame update
    void Start()
    {
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }

        //TargetObjectPrefab = GetComponentInChildren<GameObject>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ThrowingBall();
 
        }



    }

    private void ThrowingBall()
    {
        if (ThrowingObjectPrefab != null && TargetObjectPrefab != null)
        {
            // �p�[�e�B�N���V�X�e���𐶐����Ĕ����G�t�F�N�g���Đ�
            ParticleSystem explosionParticleSystem = Instantiate(explosionParticleSystemPrefab, transform.position, Quaternion.identity);
            explosionParticleSystem.Play();

            // �p�[�e�B�N���Đ����Ԃ��I��������p�[�e�B�N���V�X�e����j��
            Destroy(explosionParticleSystem.gameObject, explosionParticleSystem.main.duration);

            //�I�u�W�F�N�g�̐���
            GameObject ball = Instantiate(ThrowingObjectPrefab, this.transform.position, Quaternion.identity);
            //�W�I�̍��W
            Vector3 targetPrefabPosition = TargetObjectPrefab.transform.position;
            //�ˏo�p�x
            float angle = ThroeingAngle;
            //�ˏo���x���Y�o
            Vector3 velocity = CalculateVelocity(this.transform.position, targetPrefabPosition, angle);
            //�ˏo
            Rigidbody rid = ball.GetComponent<Rigidbody>();
            rid.AddForce(velocity * rid.mass, ForceMode.Impulse);
        }

        Vector3 CalculateVelocity(Vector3 pointA, Vector3 pointB, float angle)
        {
            Debug.Log("�^�[�Q�b�g���Ă�H");
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

