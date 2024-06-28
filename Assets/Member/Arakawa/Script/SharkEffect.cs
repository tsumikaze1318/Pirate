using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkEffect : MonoBehaviour
{
    [SerializeField]
    private GameObject effectObject;

    [SerializeField]
    private ParticleSystem explosionParticleSystemPrefab;

    //エフェクトが消えるまでの時間（秒
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
        //instanciate後にGameObjectにキャストする
        GameObject instantiateEffect = GameObject.Instantiate(effectObject,
                                                    transform.position + new Vector3(0f, 1f, 0f),
                                                    //エフェクトがデフォルトで縦になっているので、
                                                    //角度を９０度傾ける
                                                    Quaternion.Euler(50f, -90f, 1f)) as GameObject;
        //指定時間後に削除する
        Destroy(instantiateEffect, deleteTime);



    }

    void Detonate()
    {
        // パーティクルシステムを生成して爆発エフェクトを再生
        ParticleSystem explosionParticleSystem = Instantiate(explosionParticleSystemPrefab, transform.position, Quaternion.Euler(150f, -90f, 1f));
        explosionParticleSystem.Play();
    }



}
