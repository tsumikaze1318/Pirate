using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SharkRandam : MonoBehaviour
{
    [SerializeField]
    private GameObject shark;

    [SerializeField]
    private GameObject taget;

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

        //bool instanced = false;

        //if (!instanced)
        //{

            float z = Random.Range(rangeA.position.z, rangeB.position.z);

            Vector3 leftPos = new Vector3(20f, 5f, z);
            Vector3 rightPos = new Vector3(45f, 5f, z);

            shark.transform.position = leftPos;
            taget.transform.position = rightPos;


            // 生成されたことを確認する
            //instanced = true;
        //}
/*        if (instanced)
        {
            float z = Random.Range(rangeA.position.z, rangeB.position.z);

            

            
            // 生成されたことを確認する
            instanced = true;
        }*/
    }
}
