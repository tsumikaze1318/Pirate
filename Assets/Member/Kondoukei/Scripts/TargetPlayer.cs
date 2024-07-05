using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class TargetPlayer : MonoBehaviour
{

    [SerializeField]
    private GameObject KraKen = null;

    private readonly string playertag = "Player";

    List<GameObject> playerObjects = new List<GameObject>();

    float distanceBetweenKrakenAndPlayerposition;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LateUpdate()
    {
        SetPlayerPosition();
    }

    private void SetPlayerPosition()
    {
        var playreObjs = GameObject.FindGameObjectsWithTag(playertag);
        playerObjects = playreObjs.ToList();
        Vector3 KrakenPosition = KraKen.transform.position;
        for (int i = 0; i < playerObjects.Count; i++)
        {
            Vector3 playerPosition = playerObjects[i].transform.position;
            float distance = Vector3.Distance(KrakenPosition, playerPosition);
            if (distanceBetweenKrakenAndPlayerposition == 0 ||
               distanceBetweenKrakenAndPlayerposition > distance)
            {
                distanceBetweenKrakenAndPlayerposition = distance;
            }
            Debug.Log(distanceBetweenKrakenAndPlayerposition);
        }
    }

    /// <summary>
    /// クラーケンのターゲットになったプレイヤーの値を返す
    /// </summary>
    /// <returns>クラーケンのターゲットになったプレイヤーとの距離</returns>
    public float GetPlayerPosition()
    {
        return distanceBetweenKrakenAndPlayerposition;
    }

}

/*
{1, 3, 5, 9} -> {9, 5. 3. 1}
1 < 3 -> {3, 1, 5, 9}
3 < 5 -> {5, 3, 1, 9}

*/
