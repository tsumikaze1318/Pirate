using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static SceneNameClass;

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

    private static bool isFade = false;
    public static bool IsFade => isFade;

    [SerializeField]
    private SoundManager soundManager;

    [SerializeField]
    private List<FadeCanvas> canvasList = new List<FadeCanvas>();

    private BGMType _bgmType;

    private void Start()
    {
        FadeCanvas[] canvas = GetComponentsInChildren<FadeCanvas>();
        foreach(FadeCanvas canvasItem in canvas)
        {
            canvasList.Add(canvasItem);
        }

        SceneManager.sceneLoaded += SceneChanged;
    }

    public void FadeStart(SceneName sceneName, BGMType bgmType)
    {
        _bgmType = bgmType;

        for (int i = 0; i < canvasList.Count; i++)
        {
            isFade = true;
            canvasList[i].FadeOut(sceneName);
        }

        isFade = true;
    }

    //private async void StandbyFade(SceneName sceneName)
    //{
    //    if (isFade)
    //    {
    //        await Task.Delay(100);
    //        StandbyFade(sceneName);
    //        return;
    //    }
    //    else
    //    {
    //        for (int i = 0; i < canvasList.Count; i++)
    //        {
    //            isFade = true;
    //            canvasList[i].FadeOut(sceneName);
    //        }
    //    }
    //}

    public void RegisterAction_Assign(Action camera, Action count)
    {
        for (int i = 0; i < canvasList.Count; i++)
        {
            canvasList[i].RegisterAction(camera, count);
        }
    }

    public void SetIsFade(bool fade)
    {
        isFade = fade;
    }

    private void SceneChanged(Scene scene, LoadSceneMode mode)
    {
        soundManager.StopBgm();
        soundManager.PlayBgm(_bgmType);
    }
}
