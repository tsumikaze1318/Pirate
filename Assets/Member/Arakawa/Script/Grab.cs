using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grab : MonoBehaviour
{
    [SerializeField]
    private Transform grabPoint;
    [SerializeField]
    private Transform rayPoint;

    private float rayDistance = 0.2f;
    private GameObject grabObj;
    RaycastHit hit;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if(grabObj == null)
            {
                if (Physics.Raycast(rayPoint.position, transform.right, out hit, rayDistance))
                {
                    if (hit.collider != null && hit.collider.tag == "Ball")
                    {
                        grabObj = hit.collider.gameObject;
                        grabObj.GetComponent<Rigidbody>().isKinematic = true;
                        //grabObj.transform.position = grabPoint.position;
                        grabObj.transform.SetParent(transform);
                    }
                }
            }
            else
            {
                grabObj.GetComponent<Rigidbody>().isKinematic = false;
                grabObj.transform.SetParent(null);
                grabObj = null;
            }
        }

        
    }
}
