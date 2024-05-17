using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManeger1 : MonoBehaviour
{
    //[SerializeField] GameObject ball;

    void Start()
    {
        //StartCoroutine("BallPrefab");
    }

    /*IEnumerator BallPrefab()
    {
        for (int count = 0; count < 5; count++)
        {
            yield return new WaitForSeconds(0.5f);
            Instantiate(ball, new Vector3(-5, 0, 0), Quaternion.identity);
        }
    }
    */
}
