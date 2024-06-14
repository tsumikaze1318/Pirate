using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    List<Collider> _colliders = new List<Collider>();
    IKTestScript _ikTestSc;

    private void Start()
    {
        _ikTestSc = GetComponentInParent<IKTestScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _colliders.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        _colliders.Remove(other);
    }

    void OnTriggerStay(Collider other)
    {
        // トリガーエリア内のオブジェクトの中で最も近いものを見つける
        Transform closestObject = FindClosestObject();

        if (closestObject != null)
        {
            _ikTestSc._targetObj = closestObject.gameObject;
            //Debug.Log("Closest Object: " + closestObject.name);
        }
    }

    private Transform FindClosestObject()
    {
        Transform closestObject = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;

        foreach (Collider collider in _colliders)
        {
            float distance = Vector3.Distance(currentPosition, collider.transform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestObject = collider.transform;
            }
        }

        return closestObject;
    }
}
