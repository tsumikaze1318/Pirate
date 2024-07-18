using System.Collections;
using System.Collections.Generic;
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

        float lagTime = 2.0f;//�炮���Ԃ̏����l
        float deltaTime = Time.deltaTime;


        if(_bullets.Count >= 5)//���A�T�ȏ�̒e�����݂��Ă��邩�H
        {
            //���݂��Ă���Ȃ�΁A���O�鎞�Ԃ����炵�Ă����A
            lagTime += deltaTime;

        }
        else
        {
            //���Ԃ�߂��i�Q�b�H�@�P�b�H
            lagTime += deltaTime;
        }

        if (_bullets.Count >= 5) return;


        if(lagTime <= 0) //�������Ԃ��O�ȉ��ɂȂ�����j���Ԃ�߂��i�Q�b�H�@�P�b�H
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

        //Debug.Log(_bullets.Count);
    }
}
