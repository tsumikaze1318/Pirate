using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    private List<GameObject> canGrabObjects = new List<GameObject>();

    private GameObject grabObject = null;

    [SerializeField]
    private GameObject player = null;

    private float preRotY;

    private Vector3 preRot;

    private float rotYOffset;

    private Vector3 offset = Vector3.zero;

    private void Start()
    {
        player = GetComponentInParent<Player>().gameObject;
    }

    private void Update()
    {
        if (grabObject == null) return;

        float nowRotY = player.transform.localEulerAngles.y;

        Vector3 nowRot = player.transform.localEulerAngles;

        float rad = (nowRotY - preRotY) * Mathf.Deg2Rad;

        float x = offset.x * Mathf.Cos(rad) + offset.z * Mathf.Sin(rad);
        float y = offset.y;
        float z = offset.x * -Mathf.Sin(rad) + offset.z * Mathf.Cos(rad);

        offset = new Vector3(x, y, z);

        grabObject.transform.position = player.transform.position + offset;
        grabObject.transform.localEulerAngles += nowRot - preRot;

        preRot = nowRot;
        preRotY = nowRotY;
    }

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
            offset = grabObject.transform.position - player.transform.position;
            preRot = player.transform.localEulerAngles;
            preRotY = player.transform.localEulerAngles.y;
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
        if (other.gameObject.layer != 6) return;

        canGrabObjects.Add(other.gameObject);
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
    }
}
