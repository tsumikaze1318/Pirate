using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneFadeManager : MonoBehaviour
{
    private static SceneFadeManager instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public static SceneFadeManager Instance => instance;

    [SerializeField]
    private List<FadeCanvas> canvasList = new List<FadeCanvas>();

    private void Start()
    {
        FadeCanvas[] canvas = GetComponentsInChildren<FadeCanvas>();
        foreach(FadeCanvas canvasItem in canvas)
        {
            canvasList.Add(canvasItem);
        }
    }

    public void FadeStart(SceneNameClass.SceneName sceneName, BGMType bgmType)
    {
        for (int i = 0; i < canvasList.Count; i++)
        {
            canvasList[i].FadeOut(sceneName, bgmType);
        }
    }
}
