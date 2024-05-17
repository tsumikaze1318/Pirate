using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    //public string targetTag = "Player";
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ball")
        {
            Destroy(collision.gameObject,5f);
            //Debug.Log("êGÇÍÇƒÇÈÅH");
        }
    }
}
