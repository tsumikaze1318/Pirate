using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ranking : MonoBehaviour
{
    [SerializeField]
    private List<PlayerTest> testList = new List<PlayerTest>();
    private int[] points = { };

    // Start is called before the first frame update
    void Start()
    {
        points = new int[testList.Count];
        for (int i = 0; i < testList.Count; i++)
        {
            points[i] = testList[i].Point;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //for (int i = 0; i < testList.Count; i++)
        //{
        //    points[i] = testList[i].Point;
        //}
        ChangeRank();
        Debug.Log($"{points[0]},{points[1]},{points[2]}");
    }

    private void ChangeRank()
    {
        var list = new List<int>();
        // list�ɗv�f��ǉ�
        list.AddRange(points);
        // list�������_���Ń\�[�g
        list.Sort((a, b) => b - a);
        for (int i = 0; i < list.Count; i++)
        {
            points[i] = list[i];
        }
    }
}

//�J�E���g�����ł���̐�
//���v���C���[�̂���̐�����ʂ̐����z�����珇�ʂ���ɓ���ς��
//���v���C���[�̂���̐������ʂ̐�����������珇�ʂ����ɓ���ς��