
using System.Collections;
using UnityEngine;

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
    private float speed = 30f;

    private float _repeatSpan;
    private float _timeElapsed;

    private void Start()
    {
        _repeatSpan = 3;
        _timeElapsed = 0;
    }


    // Update is called once per frame
    private void Update()
    {

        _timeElapsed += Time.deltaTime;

        if(_timeElapsed >= _repeatSpan)
        {
            //弾を発射する場所
            Vector3 bulletPosition = firingPoint.transform.position;
            //上で取得した場所に"bullet"のprefabを出現させる、Bulletの向き
            GameObject newBullet = Instantiate(bullet, bulletPosition, this.gameObject.transform.rotation);
            //出現させた弾のup(y)を取得
            Vector3 direction = newBullet.transform.up;
            //弾の発射方向にnewBallのY方向を入れ、弾のオブジェクトのrigidobyに衝撃力を加える
            newBullet.GetComponent<Rigidbody>().AddForce(direction * speed, ForceMode.Impulse);
            //出現させた弾のなまを"bullet"に変更
            newBullet.name = bullet.name;
            //出現させた弾を5秒後に消す
            Destroy(newBullet, 5f);

            _timeElapsed = 0;
        }
        
    }
    
}
