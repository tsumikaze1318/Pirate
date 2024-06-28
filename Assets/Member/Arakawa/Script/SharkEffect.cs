using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject effectObject;

    [SerializeField]
    private ParticleSystem explosionParticleSystemPrefab;

    //�G�t�F�N�g��������܂ł̎��ԁi�b
    [SerializeField]
    private float deleteTime;


    private void OnTriggerEnter(Collider other)
    {
        
       if(other.gameObject.tag == "Taget")
       {
            SoundManager.Instance.PlaySe(SEType.SE4);
            Detonate();
            Destroy(gameObject);
       }

    }
    // Start is called before the first frame update
    void Start()
    {
        //instanciate���GameObject�ɃL���X�g����
        GameObject instantiateEffect = GameObject.Instantiate(effectObject,
                                                    transform.position + new Vector3(0f, 1f, 0f),
                                                    //�G�t�F�N�g���f�t�H���g�ŏc�ɂȂ��Ă���̂ŁA
                                                    //�p�x���X�O�x�X����
                                                    Quaternion.Euler(50f, -90f, 1f)) as GameObject;
        //�w�莞�Ԍ�ɍ폜����
        Destroy(instantiateEffect, deleteTime);



    }

    void Detonate()
    {
        // �p�[�e�B�N���V�X�e���𐶐����Ĕ����G�t�F�N�g���Đ�
        ParticleSystem explosionParticleSystem = Instantiate(explosionParticleSystemPrefab, transform.position, Quaternion.Euler(150f, -90f, 1f));
        explosionParticleSystem.Play();
    }



}
