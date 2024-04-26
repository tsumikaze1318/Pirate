using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static object _lock = new object();

    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            lock (_lock)
            {
                if (instance == null)
                {
                    instance
                        = FindObjectOfType<GameManager>();
                    if (instance == null)
                    {
                        var singletonObject = new GameObject();
                        instance = singletonObject.AddComponent<GameManager>();
                        singletonObject.name = nameof(GameManager) + "(singleton)";
                    }
                }

                return instance;
            }
        }
    }

    [SerializeField]
    private SceneFadeManager fadeManager;

    private int[] scores = { 0, 0, 0, 0 };

    public int[] Scores => scores;

    [SerializeField, EnumIndex(typeof(CommonParam.UnitType))]
    private List<GameSystemManager> gameSystems = new List<GameSystemManager>();

    public void AddScore(int plNum)
    {
        scores[plNum]++;
        gameSystems[plNum].Score = scores[plNum];
    }

    public void SubScore(int plNum)
    {
        scores[plNum]--;
        gameSystems[plNum].Score = scores[plNum];
    }
}
