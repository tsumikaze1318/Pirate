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

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (grabObj == null)
    //    {
    //        if (collision.gameObject.tag == "Player")
    //        {
    //            Debug.Log("つかんでる？");
    //            grabObj = collision.gameObject;
    //            grabObj.transform.SetParent(transform);
    //            grabObj.GetComponent<Rigidbody>().isKinematic = true;
    //            grabObj.transform.localPosition = grabPoint.localPosition;

    //        }
    //    }
    //}

    private void OnTriggerEnter(Collider other)
    {
        if(grabObj == null)
        {
            if(other.gameObject.tag == "Player")
            {
                grabObj = other.gameObject;
                grabObj.transform.SetParent(transform);
                grabObj.GetComponent<Rigidbody>().isKinematic = true;
                grabObj.transform.localPosition = grabPoint.localPosition;
            }
        }
    }
}

