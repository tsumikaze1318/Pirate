using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExecuteCannon : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem explosionParticleSystemPrefab;

    [SerializeField]
    //弾の発射場所
    private GameObject firingPoint;

    [SerializeField]
    //たま
    private GameObject bullet;

    [SerializeField]
    //弾の速さ
    private float speed = 10f;

    public void Fire()
    {
        Vector3 bulletPosition = firingPoint.transform.position;

        //計算された位置に弾を生成する
        GameObject newBullet = Instantiate(bullet, bulletPosition, transform.rotation);
        //SoundManager.Instance.PlaySe(SEType.SE3);
        //Debug.Log("うごいてるか１？");

        //弾の方向を取得する
        Vector3 direction = newBullet.transform.up;

        //指定された方向に力をたまに加える
        newBullet.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);

        // パーティクルシステムを生成して爆発エフェクトを再生
        ParticleSystem explosionParticleSystem = Instantiate(explosionParticleSystemPrefab, transform.position, Quaternion.identity);
        //SoundManager.Instance.PlaySe(SEType.SE2);
        explosionParticleSystem.Play();

        // パーティクル再生時間が終了したらパーティクルシステムを破棄
        Destroy(explosionParticleSystem.gameObject, explosionParticleSystem.main.duration);
    }
}
