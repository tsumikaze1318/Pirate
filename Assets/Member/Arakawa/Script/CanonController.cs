using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class CanonController : MonoBehaviour
{
    [SerializeField]
    private FireBullet _targetFireBullet;

    private List<GameObject> _bullets = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        for (int i = _bullets.Count - 1; i >= 0; i--)
        {
          if (_bullets[i] != null) _bullets.RemoveAt(i);
        }

        /*if ( 今、５個以上の弾が存在しているか？)
        {
           存在しているならば、ラグる時間を減らしていく、Time.deltaTime 

        }
        else
        {
            時間を戻す（２秒？　１秒？
        }

        if （もし時間が０以下になったら）時間を戻す（２秒？　１秒？
        {
            GameObject newbullet = _targetFireBullet.Fire();
            _bullets.Add(newbullet);
        }
        */

    }
}
