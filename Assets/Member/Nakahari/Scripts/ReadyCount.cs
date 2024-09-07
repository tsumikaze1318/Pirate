using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReadyCount : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ImageColor.Instance._count == GameManager.Instance.Attendance && !ImageColor.Instance.Ready)
        {
            ImageColor.Instance.Ready = true;
            GameManager.Instance.StandbyPlayersReady();
        }
    }
}
