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

        /*if ( ���A�T�ȏ�̒e�����݂��Ă��邩�H)
        {
           ���݂��Ă���Ȃ�΁A���O�鎞�Ԃ����炵�Ă����ATime.deltaTime 

        }
        else
        {
            ���Ԃ�߂��i�Q�b�H�@�P�b�H
        }

        if �i�������Ԃ��O�ȉ��ɂȂ�����j���Ԃ�߂��i�Q�b�H�@�P�b�H
        {
            GameObject newbullet = _targetFireBullet.Fire();
            _bullets.Add(newbullet);
        }
        */

    }
}
