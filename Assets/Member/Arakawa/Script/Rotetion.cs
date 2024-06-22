using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotetion : MonoBehaviour
{
    //public GameObject targetObject;
    


    void Update()
    {
        transform.Rotate(0f, 0f ,-20f * Time.deltaTime);
    }
}
