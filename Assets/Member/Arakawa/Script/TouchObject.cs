using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchObject : MonoBehaviour
{
    public GameObject plauer;
    public GameObject target;
    public bool isRelese; //�ǉ�

    //**********�ǉ���������**********
    void Start()
    {
        isRelese = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isRelese)
            {
                transform.parent = null;
                isRelese = true;
            }
        }
    }
    //**********�ǉ������܂�**********

    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.name == "Cube")
        {
            isRelese = false; //�ǉ�
            this.transform.position = new Vector3(target.transform.position.x, 0.5f, target.transform.position.z);
            transform.SetParent(gameObject.transform);
        }
    }
}