using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TestDictionary : MonoBehaviour
{
    private static Dictionary<int, int> keyValuePairs = new Dictionary<int, int>();

    public static Dictionary<int, int> KeyValuePairs => keyValuePairs;

    private int[] ints = { 5, 15, 10, 0 };

    // Start is called before the first frame update
    void Awake()
    {
        keyValuePairs.Clear();

        for (int i = 1; i < 5; i++) 
        {
            keyValuePairs.Add(i, ints[i - 1]);
        }

        keyValuePairs = keyValuePairs.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
    }
}
