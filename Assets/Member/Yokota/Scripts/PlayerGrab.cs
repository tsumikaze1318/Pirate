using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    private List<GameObject> canGrabObjects = new List<GameObject>();

    private GameObject grabObject = null;

    private GameObject playerObj = null;

    private Player player = null;

    private float preRotY;

    private Vector3 preRot;

    private Vector3 offset = Vector3.zero;

    private MeshCollider grabCollider;

    private void Start()
    {
        playerObj ??= GetComponentInParent<Player>().gameObject;
        player ??= GetComponentInParent<Player>();
        grabCollider ??= GetComponent<MeshCollider>();
    }

    private void Update()
    {
        if (player._respawn) grabCollider.enabled = false;
        else grabCollider.enabled = true;

        if (grabObject == null) return;

        float nowRotY = playerObj.transform.localEulerAngles.y;

        Vector3 nowRot = playerObj.transform.localEulerAngles;

        float rad = (nowRotY - preRotY) * Mathf.Deg2Rad;

        float x = offset.x * Mathf.Cos(rad) + offset.z * Mathf.Sin(rad);
        float y = offset.y;
        float z = offset.x * -Mathf.Sin(rad) + offset.z * Mathf.Cos(rad);

        offset = new Vector3(x, y, z);

        grabObject.transform.position = playerObj.transform.position + offset;
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
            offset = grabObject.transform.position - playerObj.transform.position;
            preRot = playerObj.transform.localEulerAngles;
            preRotY = playerObj.transform.localEulerAngles.y;
            // ’†’£’Ç‹L
            Physics.IgnoreCollision(grabObject.GetComponent<Collider>(), player._playerCollider, true);
        }
    }

    public void Release()
    {
        if (grabObject == null) return;
        // ’†’£’Ç‹L
        Physics.IgnoreCollision(grabObject.GetComponent<Collider>(), player._playerCollider, false);
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
