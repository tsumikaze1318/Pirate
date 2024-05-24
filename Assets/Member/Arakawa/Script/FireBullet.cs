using System.Collections.Generic;
using System.Collections;
using UnityEngine;
//using Unity.VisualScripting;

public class FireBullet : MonoBehaviour
{
    [SerializeField]
    //弾の発射場所
    private GameObject firingPoint;

    [SerializeField]
    //たま
    private GameObject bullet;

    [SerializeField]
    //弾の速さ
    private float speed = 10f;

    [SerializeField]
    private float _repeatSpan = 1;

    [SerializeField]
    private CanonController canonController;

    [SerializeField]
    private float _FierStartTime;

    //public float _repeatSpan;
    private float _timeElapsed;
    //private int count;
    //private GameObject newBullet;

    void Start()
    {
        //_repeatSpan = 1;
        _timeElapsed = _FierStartTime;
        //StartCoroutine("BallSpawn");
    }

    //private void Update()
    void Update()
    {
        if(!GameManager.Instance.GameStart) return;
        _timeElapsed += Time.deltaTime;

        //カウントが５未満である間、継続的に弾丸を生成する
        //while (count < 1)
        //{
            // Check if it's time to spawn a new bullet
            if (_timeElapsed >= _repeatSpan)
            {
                //新しい弾を生成する時間かどうかを確認する
                Vector3 bulletPosition = firingPoint.transform.position;

                //計算された位置に弾を生成する
                GameObject newBullet = Instantiate(bullet, bulletPosition, transform.rotation);

                //弾の方向を取得する
                Vector3 direction = newBullet.transform.up;

                //指定された方向に力をたまに加える
                newBullet.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);

                //弾の名前を設定する
                newBullet.name = bullet.name;

                //一定時間後に弾を破棄する
                //Destroy(newBullet, 2f);

                //リセットする
                _timeElapsed = 0;
                
                canonController.CollectBullet(newBullet);

                //増やす
                //count++;
            }
            //else
           //{
                //break; 
                //時間の経過が予定された時間に達していない場合、ループを抜ける
            //}
       // }

        //すべての弾が破壊された場合は、カウントをリセットします
        //if (GameObject.FindGameObjectsWithTag("Ball").Length == 0)
        //{
           // count = 0;
        //}
    }

    //public GameObject Fire()
    //{

    //    //return newBullet;
    //}
}
