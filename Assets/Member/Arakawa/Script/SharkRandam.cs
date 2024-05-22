using System.Collections;
using System.Collections.Generic;
using TMPro;
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

    [SerializeField]
    private float ThroeingAngle;

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

            float z = Random.Range(rangeA.position.z, rangeB.position.z);

            Vector3 pos = new Vector3(20f, 4.5f, z);

            Instantiate(Shark, pos, Quaternion.identity);

            // 生成されたことを確認する
            instanced = true;
        }
        if (!!instanced)
        {
            float z = Random.Range(rangeA.position.z, rangeB.position.z);

            Vector3 pos = new Vector3(45f, 4.5f, z);

            Instantiate(Taget, pos, Quaternion.identity);
            // 生成されたことを確認する
            instanced = true;
        }
    }
}
