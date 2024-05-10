using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    //[SerializeField]
    //private FireBullet _targetFireBullet;

    private List<GameObject> _bullets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        for (int i = _bullets.Count - 1; i >= 0; i--)
        {
          if (_bullets[i] != null) _bullets.RemoveAt(i);
        }

        float lagTime = 2.0f;//らぐ時間の初期値
        float deltaTime = Time.deltaTime;


        if(_bullets.Count >= 5)//今、５個以上の弾が存在しているか？
        {
            //存在しているならば、ラグる時間を減らしていく、
            lagTime += deltaTime;

        }
        else
        {
            //時間を戻す（２秒？　１秒？
            lagTime += deltaTime;
        }

        if (_bullets.Count >= 5) return;


        if(lagTime <= 0) //もし時間が０以下になったら）時間を戻す（２秒？　１秒？
        {
            lagTime = 1.0f;
            GameObject newBullet = _targetFireBullet.Fire();
            _bullets.Add(newBullet);
        }
        */
        

    }
    public void CollectBullet(GameObject bullet)
    {
        if (_bullets.Count >= 6)
        {
            Destroy(_bullets[0]);
            _bullets.RemoveAt(0);
            _bullets.Add(bullet);
        }
        else
        {
            _bullets.Add(bullet);
        }

        Debug.Log(_bullets.Count);
    }
}
