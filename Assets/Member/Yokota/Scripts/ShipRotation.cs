using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipRotation : MonoBehaviour
{
    [SerializeField]
    GameObject ship;

    float timeLine = 0;

    private void Update()
    {
        timeLine += Time.deltaTime;
        ship.transform.eulerAngles = new Vector3(0, 0, 10 * Mathf.Sin(timeLine));
    }
}
