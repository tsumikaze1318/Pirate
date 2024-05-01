using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public string targetTag = "Player";
    // Start is called before the first frame update
    private void OnCollsionEnter(Collision collision)
    {
        if(collision.gameObject.name == "Player")
        {
            Destroy(collision.gameObject,5f);
        }
    }
}
