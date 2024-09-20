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
        //if (grabObj == null)
        //{
        //    if (Physics.Raycast(rayPoint.position, transform.right, out hit, rayDistance))
        //    {
        //        if (hit.collider != null && hit.collider.tag == "Player")
        //        {
        //            Debug.Log("Ç¬Ç©ÇÒÇ≈ÇÈÅH");
        //            grabObj = hit.collider.gameObject;
        //            grabObj.GetComponent<Rigidbody>().isKinematic = true;
        //            grabObj.transform.localPosition = grabPoint.localPosition;
        //            grabObj.transform.SetParent(transform);

        //        }
        //    }
        //}
    }

    /*private void OnCollisionEnter(Collision collision)
    {
        if (grabObj == null)
        {
            if (collision.gameObject.tag == "Player")
            {
                Debug.Log("Ç¬Ç©ÇÒÇ≈ÇÈÅH");
                grabObj = collision.gameObject;
                grabObj.transform.parent.SetParent(transform);
                grabObj.GetComponent<Rigidbody>().isKinematic = true;
                grabObj.transform.position = grabPoint.localPosition;

            }
        }
    }*/

    private void OnCollisionStay(Collision collision)
    {
        if (grabObj == null)
        {
            if (collision.gameObject.tag == "Player")
            {
                Player player = collision.gameObject.GetComponent<Player>();
                Animator animator = collision.gameObject.GetComponent<Animator>();
                player._state = CommonParam.UnitState.Immovable;
                animator.SetTrigger("Drown");
                Debug.Log("Ç¬Ç©ÇÒÇ≈ÇÈÅH");
                grabObj = collision.gameObject;
                //grabObj.GetComponent<Rigidbody>().isKinematic = true;
                grabObj.transform.position = grabPoint.position;

            }
        }
    }

}

