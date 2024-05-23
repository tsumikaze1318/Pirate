using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField]
    private Transform grabPoint;
    [SerializeField]
    private Transform rayPoint;

    private float rayDistance = 5f;
    private GameObject grabObj;
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 1; i++)
        {
            if (grabObj == null)
            {
                if (Physics.Raycast(rayPoint.position, transform.right, out hit, rayDistance))
                {
                    if (hit.collider != null && hit.collider.tag == "Player")
                    {
                        Debug.Log("‚Â‚©‚ñ‚Å‚éH");
                        grabObj = hit.collider.gameObject;
                        grabObj.GetComponent<Rigidbody>().isKinematic = true;
                        grabObj.transform.position = grabPoint.position;
                        grabObj.transform.SetParent(transform);
                    }
                }
            }
        }


    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Untagged"))
        {
            Destroy(gameObject);
        }
    }
}

