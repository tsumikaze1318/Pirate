using System.Collections.Generic;
using System.Collections;
using UnityEngine;
//using Unity.VisualScripting;

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
    private float speed = 10f;


    public float _repeatSpan;
    private float _timeElapsed;
    private int count;
    //public int maxInstance = 1;
    //private int currentInstance = 0;

    void Start()
    {
        _repeatSpan = 1;
        _timeElapsed = 0;
        //StartCoroutine("BallSpawn");
    }

    //private void Update()
    void Update()
    {
        _timeElapsed += Time.deltaTime;

        //�J�E���g���T�����ł���ԁA�p���I�ɒe�ۂ𐶐�����
        while (count < 1)
        {
            // Check if it's time to spawn a new bullet
            if (_timeElapsed >= _repeatSpan)
            {
                //�V�����e�𐶐����鎞�Ԃ��ǂ������m�F����
                Vector3 bulletPosition = firingPoint.transform.position;

                //�v�Z���ꂽ�ʒu�ɒe�𐶐�����
                GameObject newBullet = Instantiate(bullet, bulletPosition, transform.rotation);

                //�e�̕������擾����
                Vector3 direction = newBullet.transform.up;

                //�w�肳�ꂽ�����ɗ͂����܂ɉ�����
                newBullet.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);

                //�e�̖��O��ݒ肷��
                newBullet.name = bullet.name;

                //��莞�Ԍ�ɒe��j������
                //Destroy(newBullet, 2f);

                //���Z�b�g����
                _timeElapsed = 0;

                //���₷
                count++;
            }
            else
            {
                break; 
                //���Ԃ̌o�߂��\�肳�ꂽ���ԂɒB���Ă��Ȃ��ꍇ�A���[�v�𔲂���
            }
        }

        //���ׂĂ̒e���j�󂳂ꂽ�ꍇ�́A�J�E���g�����Z�b�g���܂�
        if (GameObject.FindGameObjectsWithTag("Ball").Length == 0)
        {
            count = 0;
        }
    }

    //public GameObject Fire()
    //{
        //return newBullet;

    //}
}
