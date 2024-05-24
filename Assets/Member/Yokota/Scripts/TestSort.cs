using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSort : MonoBehaviour
{
    [SerializeField]
    private int[] ints = new int[] { 1, 2, 3 };

    private void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            int[] ranks = ints;
            Array.Sort(ranks);
            Array.Reverse(ranks);

            for (int i = 0; i < ranks.Length; i++)
            {
                Debug.Log("ranks:" + ranks[i]);
                Debug.Log("ints:" + ints[i]);
            }
        }
    }
}
