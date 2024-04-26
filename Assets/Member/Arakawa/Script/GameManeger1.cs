using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger1 : MonoBehaviour
{
    public GameObject prefab;
    private int count;

    void Start()
    {
        count = 0;
    }

    void Update()
    {
        if(count < 5)
        {
            GameObject ball = GameObject.Instantiate(prefab) as GameObject;
            //ball.GetComponent<Rigidbody>().AddForce(dir * 

        }
    }
}
