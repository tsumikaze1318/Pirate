using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SharkRandamInstance : MonoBehaviour
{
    [SerializeField]
    private Shark shark;

    public void RandamInstance()
    {
        shark.ThrowingBall();
    }
}
