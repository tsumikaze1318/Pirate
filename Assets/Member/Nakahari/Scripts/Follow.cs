using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    [SerializeField]
    GameObject _targetObject;

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = _targetObject.transform.localPosition;
    }
}
