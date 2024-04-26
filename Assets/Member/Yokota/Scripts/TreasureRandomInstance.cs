using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TreasureRandomInstance : MonoBehaviour
{
    [SerializeField]
    private GameObject TreasureBox;

    [SerializeField]
    private Transform rangeA;
    
    [SerializeField]
    private Transform rangeB;

    private Vector3 halfExtens = new Vector3(0.5f, 0.5f, 0.5f);

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RandomInstance();
        }
    }

    private void RandomInstance()
    {
        bool instanced = false;

        while (!instanced)
        {
            float x = Random.Range(rangeB.position.x, rangeA.position.x);
            float y = Random.Range(rangeB.position.y, rangeA.position.y);
            float z = Random.Range(rangeB.position.z, rangeA.position.z);

            Vector3 pos = new Vector3(x, y, z);

            if (!Physics.CheckBox(pos, halfExtens, Quaternion.identity))
            {
                Instantiate(TreasureBox, pos, Quaternion.identity);
                instanced = true;
            }
        }
    }
}
