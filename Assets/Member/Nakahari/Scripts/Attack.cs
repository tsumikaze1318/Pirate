using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public BoxCollider _collider;
    private void Start()
    {
        this._collider = GetComponent<BoxCollider>();
        _collider.enabled = false;
    }

    void SubCount(Collision other)
    {
        HitCount hitCount = other.gameObject.GetComponent<HitCount>();
        hitCount._count--;
    }

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            SubCount(other);
            _collider.enabled = false;
        }

        Debug.Log("相手のタグ:"+other.gameObject.tag);
        Debug.Log("相手の名前"+other.gameObject.name);
    }
}
