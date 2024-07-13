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

        Debug.Log("‘ŠŽè‚Ìƒ^ƒO:"+other.gameObject.tag);
        Debug.Log("‘ŠŽè‚Ì–¼‘O"+other.gameObject.name);
    }
}
