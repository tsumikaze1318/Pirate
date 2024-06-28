using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    private List<GameObject> canGrabObjects = new List<GameObject>();

    private GameObject grabObject = null;

    public void Grab()
    {
        if (grabObject != null) return;

        canGrabObjects.RemoveAll(item => item == null);

        float minDis = float.MaxValue;

        foreach (GameObject obj in canGrabObjects)
        {
            float distance = Mathf.Abs((obj.transform.position - transform.position).magnitude);
            
            if (distance < minDis)
            {
                minDis = distance;
                grabObject = obj;
            }
        }

        if (grabObject != null)
        {
            canGrabObjects.Clear();
            grabObject.transform.parent = transform.parent;
        }
    }

    public void Release()
    {
        if (grabObject == null) return;

        grabObject.transform.parent = null;
        grabObject = null;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (grabObject != null) return;
        if (!other.CompareTag("Ground")) canGrabObjects.Add(other.gameObject);
        
        Debug.Log($"TriggerEnter : {canGrabObjects.Count}");
    }

    private void OnTriggerExit(Collider other)
    {
        int count = 0;

        foreach (GameObject obj in canGrabObjects)
        {
            if (obj == other.gameObject)
            {
                canGrabObjects.RemoveAt(count);
                break;
            }

            count++;
        }

        Debug.Log($"TriggerExit : {canGrabObjects.Count}");
    }
}
