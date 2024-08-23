using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TestDictionary : MonoBehaviour
{
    private static Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();

    public static Dictionary<int, int> KeyValuePairs { get { return keyValuePairs; } }

    private int[] ints = { 10, 5, 15, 0 };

    // Start is called before the first frame update
    void Awake()
    {
        for (int i = 0; i < 4; i++) 
        {
            keyValuePairs.Add(ints[i], i);
        }

        keyValuePairs = keyValuePairs.OrderByDescending(x => x.Key).ToDictionary(x => x.Key, x => x.Value);

        for (int i = 0; i < 4; i++)
        {
            Debug.Log(keyValuePairs.ElementAt(i).Key);
        }
    }
}
