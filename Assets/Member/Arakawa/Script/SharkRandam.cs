using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkRandam : MonoBehaviour
{
    [SerializeField]
    private GameObject Shark;

    [SerializeField]
    private GameObject Taget;

    [SerializeField]
    private Transform rangeA;

    [SerializeField]
    private Transform rangeB;

    private void Start()
    {
        for (int i = 0; i < 1; i++)
        {
            RandomInstance();
        }
    }

    public void RandomInstance()
    {

        bool instanced = false;

        if (!instanced)
        {
            // x, y, z���W���w�肳�ꂽ�͈͓��Ō��肷��
            //float x = Random.Range(rangeA.position.x, rangeB.position.x);
            //float y = Random.Range(rangeA.position.y, rangeB.position.y);
            float z = Random.Range(rangeA.position.z, rangeB.position.z);

            Vector3 pos = new Vector3(20f, 0f, z);

            Instantiate(Shark, pos, Quaternion.identity);
            // �������ꂽ���Ƃ��m�F����
            instanced = true;
        }
        if (!!instanced)
        {
            // x, y, z���W���w�肳�ꂽ�͈͓��Ō��肷��
            //float x = Random.Range(rangeA.position.x, rangeB.position.x);
            //float y = Random.Range(rangeA.position.y, rangeB.position.y);
            float z = Random.Range(rangeA.position.z, rangeB.position.z);

            Vector3 pos = new Vector3(45f, 0f, z);

            Instantiate(Taget, pos, Quaternion.identity);
            // �������ꂽ���Ƃ��m�F����
            instanced = true;
        }
    }

}
