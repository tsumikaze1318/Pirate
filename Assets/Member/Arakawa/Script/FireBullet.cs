using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Unity.VisualScripting;

public class FireBullet : MonoBehaviour
{
    [SerializeField]
    //�e�̔��ˏꏊ
    private GameObject firingPoint;

    [SerializeField]
    //����
    private GameObject bullet;

    [SerializeField]
    //�e�̑���
    private float speed = 30f;

    //private float _repeatSpan;
    //private float _timeElapsed;
    public int maxInstance = 1;
    private int currentInstance = 0;

    void Start()
    {
        //_repeatSpan = 3;
        //_timeElapsed = 0;
    }

    private void Update()
    {
        //_timeElapsed += Time.deltaTime;

        if(currentInstance < maxInstance)
        {
            Vector3 bulletPosition = firingPoint.transform.position;
            currentInstance++;
            //�e�𔭎˂���ꏊ
            //Vector3 bulletPosition = firingPoint.transform.position;
            //��Ŏ擾�����ꏊ��"bullet"��prefab���o��������ABullet�̌���
            GameObject newBullet = Instantiate(bullet, bulletPosition, this.gameObject.transform.rotation);
            //�o���������e��up(y)���擾
            Vector3 direction = newBullet.transform.up;
            //�e�̔��˕�����newBall��Y���������A�e�̃I�u�W�F�N�g��rigidoby�ɏՌ��͂�������
            newBullet.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
            //�o���������e�̂Ȃ܂�"bullet"�ɕύX
            newBullet.name = bullet.name;
            //�o���������e��5�b��ɏ���
            Destroy(newBullet, 5f);

            //_timeElapsed = 0;
        }
        
    }
    
}
