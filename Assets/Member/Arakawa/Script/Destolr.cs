using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destolr : MonoBehaviour
{
    // Start is called before the first frame update
    void OnCollisionEnter(Collision collision)
    {
        // Õ“Ë‚µ‚½‘Šè‚ÉPlayerƒ^ƒO‚ª•t‚¢‚Ä‚¢‚é‚Æ‚«
        if (collision.gameObject.tag == "Player")
        {
            // 0.2•bŒã‚ÉÁ‚¦‚é
            Destroy(gameObject, 5f);
        }
    }
}
