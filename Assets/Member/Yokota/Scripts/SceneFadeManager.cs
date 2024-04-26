using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFadeManager : MonoBehaviour
{
    private bool isFadeOut;

    private bool isFadeIn;

    [SerializeField]
    private Image fadeImage;

    private float alpha = 0;

    private string afterScene;

    private float fadeTime = 1f;

    private void Start()
    {
        DontDestroyOnLoad(this);
        SetAlpha();
        SceneManager.sceneLoaded += FadeIn;
    }

    private void Update()
    {
        FadeProcess();
    }

    private void FadeIn(Scene scene, LoadSceneMode mode)
    {
        isFadeIn = true;
    }

    public void FadeOut(string nextScene)
    {
        isFadeOut = true;
        afterScene = nextScene;
    }

    private void FadeProcess()
    {
        if (isFadeIn)
        {
            alpha -= fadeTime * Time.deltaTime;
            SetAlpha();

            if (alpha < 0)
            {
                alpha = 0;
                isFadeIn = false;
            }
        }
        if (isFadeOut)
        {
            alpha += fadeTime * Time.deltaTime;
            SetAlpha();

            if (alpha > 1)
            {
                alpha = 1;
                isFadeOut = false;
                SceneManager.LoadScene(afterScene);
            }
        }
    }

    private void SetAlpha()
    {
        fadeImage.color = new Color(0, 0, 0, alpha);
    }
}
