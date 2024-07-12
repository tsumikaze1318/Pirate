using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneFadeManager : MonoBehaviour
{
    private static SceneFadeManager instance = null;
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

    public Action _cameraChange = null;
    public Action _countStart = null;
    public Action _movieStart = null;
    public Action _movieSet = null;

    // フェードする時間
    public float _fadeTime = 1f;

    private void Start()
    {
        FadeCanvas[] canvas = GetComponentsInChildren<FadeCanvas>();
        foreach(FadeCanvas canvasItem in canvas)
        {
            canvasList.Add(canvasItem);
        }

        SceneManager.sceneLoaded += SceneChanged;
    }

    public void FadeStart(SceneNameClass.SceneName sceneName, BGMType bgmType)
    {
        _bgmType = bgmType;

        for (int i = 0; i < canvasList.Count; i++)
        {
            isFade = true;
            canvasList[i].FadeOut(sceneName);
        }

        isFade = true;
    }

    public void RegisterAction_Assign(Action camera
                                    , Action count
                                    , Action movieStart
                                    , Action movieSet)
    {
        _cameraChange = camera;
        _countStart = count;
        _movieStart = movieStart;
        _movieSet = movieSet;
    }

    public void SetIsFade(bool fade)
    {
        isFade = fade;
    }

    public void SetFadeTime(float fadeTime)
    {
        _fadeTime = fadeTime;
    }

    private void SceneChanged(Scene scene, LoadSceneMode mode)
    {
        soundManager.StopBgm();
        soundManager.PlayBgm(_bgmType);
    }
}
