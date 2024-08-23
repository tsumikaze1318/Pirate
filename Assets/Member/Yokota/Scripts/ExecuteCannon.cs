using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteCannon : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem explosionParticleSystemPrefab;

    [SerializeField]
    //�e�̔��ˏꏊ
    private GameObject firingPoint;

    [SerializeField]
    //����
    private GameObject bullet;

    [SerializeField]
    //�e�̑���
    private float speed = 10f;

    public void Fire()
    {
        Vector3 bulletPosition = firingPoint.transform.position;

        //�v�Z���ꂽ�ʒu�ɒe�𐶐�����
        GameObject newBullet = Instantiate(bullet, bulletPosition, transform.rotation);
        //SoundManager.Instance.PlaySe(SEType.SE3);
        //Debug.Log("�������Ă邩�P�H");

        //�e�̕������擾����
        Vector3 direction = newBullet.transform.up;

        //�w�肳�ꂽ�����ɗ͂����܂ɉ�����
        newBullet.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);

        // �p�[�e�B�N���V�X�e���𐶐����Ĕ����G�t�F�N�g���Đ�
        ParticleSystem explosionParticleSystem = Instantiate(explosionParticleSystemPrefab, transform.position, Quaternion.identity);
        //SoundManager.Instance.PlaySe(SEType.SE2);
        explosionParticleSystem.Play();

        // �p�[�e�B�N���Đ����Ԃ��I��������p�[�e�B�N���V�X�e����j��
        Destroy(explosionParticleSystem.gameObject, explosionParticleSystem.main.duration);
    }
}
