using System.Linq;
using UnityEngine;

public class ExecuteGimmick : MonoBehaviour
{
    [SerializeField]
    private GameObject[] gimmicks;

    private float[] posx = new float[4];

    private void Start()
    {
        for (int i = 0; i < posx.Length; i++)
        {
            posx[i] = TestDictionary.KeyValuePairs.ElementAt(i).Value;
        }

        for (int i = 0; i < gimmicks.Length; i++)
        {
            Instantiate(gimmicks[i], new Vector3(posx[gimmicks.Length - i], 0, 0), Quaternion.identity);
        }
    }
}
