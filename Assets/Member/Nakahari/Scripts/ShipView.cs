using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipView : MonoBehaviour
{
    [SerializeField]
    private Transform _ship;

    [SerializeField]
    private float _cameraSpeed;

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(_ship.position, Vector3.up, _cameraSpeed);
    }
}
