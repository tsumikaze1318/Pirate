using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkRandamInstance : MonoBehaviour
{
    [SerializeField]
    private Shark shark;

    [SerializeField]
    private Transform[] Ranges;

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            RandamInstance();
        }
    }

    private void RandamInstance()
    {
        float z = Random.Range(Ranges[0].position.z, Ranges[1].position.z);

        Vector3 pos = new Vector3(0, 0, z);

        shark.ThrowingBall(pos);
    }
}
