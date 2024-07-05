using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
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

    //[SerializeField]
    //private float _repeatSpan = 6;

    [SerializeField]
    private CanonController canonController;

    [SerializeField]
    private float _FierStartTime;

    public float _repeatSpan;
    private float _timeElapsed;
    //private int count;
    //private GameObject newBullet;

    void Start()
    {
        //_repeatSpan = 1;
        //aw_timeElapsed = _FierStartTime;
        //StartCoroutine("BallSpawn");
    }

    //private void Update()
    void Update()
    {
        Debug.Log("�������Ă邩�H");
        if(!GameManager.Instance.GameStart) return;
        if(GameManager.Instance.GameEnd) return;
        _timeElapsed += Time.deltaTime;

        //�J�E���g���T�����ł���ԁA�p���I�ɒe�ۂ𐶐�����
        //while (count < 1)
        //{
        // Check if it's time to spawn a new bullet
        if (_timeElapsed >= _repeatSpan)
        {
            //�V�����e�𐶐����鎞�Ԃ��ǂ������m�F����
            Vector3 bulletPosition = firingPoint.transform.position;
            //�v�Z���ꂽ�ʒu�ɒe�𐶐�����
            GameObject newBullet = Instantiate(bullet, bulletPosition, transform.rotation);
            SoundManager.Instance.PlaySe(SEType.SE3);
            Debug.Log("�������Ă邩�P�H");

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

            canonController.CollectBullet(newBullet);

            //���₷
            //count++;
        }
        //else
        //{
        //    break;
        //    ���Ԃ̌o�߂��\�肳�ꂽ���ԂɒB���Ă��Ȃ��ꍇ�A���[�v�𔲂���
        //    }
        // }

        //���ׂĂ̒e���j�󂳂ꂽ�ꍇ�́A�J�E���g�����Z�b�g���܂�
        //if (GameObject.FindGameObjectsWithTag("Ball").Length == 0)
        //{
        // count = 0;
        //}

    }

    /// <summary>
    /// �o�ߎ��Ԃ�����������֐�
    /// </summary>
    /// <param name="time">����������l</param>
    public void SetInitialTime(float time)
    {
        _timeElapsed = time;
    }

    //public GameObject Fire()
    //{

    //    //return newBullet;
    //}
}
