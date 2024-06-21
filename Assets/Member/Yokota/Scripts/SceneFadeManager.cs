using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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

    private bool isFade = false;
    public bool IsFade => isFade;

    [SerializeField]
    private SoundManager soundManager;

    [SerializeField]
    private List<FadeCanvas> canvasList = new List<FadeCanvas>();

    private PlayerInputManager playerInputManager;

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

    public void FadeStart(SceneNameClass.SceneName sceneName, BGMType bgmType)
    {
        _bgmType = bgmType;

        for (int i = 0; i < canvasList.Count; i++)
        {
            canvasList[i].FadeOut(sceneName);
        }
    }

    public void RegisterAction_Assign(Action camera, Action count)
    {
        for (int i = 0; i < canvasList.Count; i++)
        {
            canvasList[i].RegisterAction(camera, count);
        }
    }

    private void DisablePlayerInputManager()
    {
        playerInputManager.enabled = false;
    }

    public void SetIsFade(bool fade)
    {
        isFade = fade;
    }

    private void SceneChanged(Scene scene, LoadSceneMode mode)
    {
        soundManager.StopBgm();
        soundManager.PlayBgm(_bgmType);

        playerInputManager = FindObjectOfType<PlayerInputManager>();
    }
}
