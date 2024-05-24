using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkRandamInstance : MonoBehaviour
{
    [SerializeField]
    private Shark shark;

    [SerializeField]
    private Transform[] Ranges;

    private float timeLine = 3f;

    private void Update()
    {
        timeLine += Time.deltaTime;

        if (timeLine > 3f)
        {
            RandamInstance();
            timeLine = 0;
        }
    }

    private void RandamInstance()
    {
        float z = Random.Range(Ranges[0].position.z, Ranges[1].position.z);

        shark.ThrowingBall(z);
    }
}
