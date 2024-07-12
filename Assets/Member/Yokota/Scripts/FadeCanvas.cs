using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class FadeCanvas : MonoBehaviour
{
    // フェードアウトの開始、終了を伝えるフラグ
    private bool _isFadeOut;
    // フェードインの開始、終了を伝えるフラグ
    private bool _isFadeIn;

    // フェードする画像のイメージ
    [SerializeField]
    private Image _fadeImage;

    // フェードする画像の不透明度
    private float _alpha = 0;

    // 遷移先のシーン名
    private string _afterScene;

    // フェードする時間
    private float _fadeTime = 1f;

    private Action _cameraChange = null;

    private Action _countStart = null;

    private Action _movieStart = null;

    private SceneFadeManager _sceneFadeManager;

    private void Start()
    {
        SetAlpha();
        _sceneFadeManager = GetComponentInParent<SceneFadeManager>();
        // シーン遷移が完了したときにフェードインを実行するように設定
        SceneManager.sceneLoaded += FadeIn_and_SceneChange;
    }

    private void Update()
    {
        // フェード処理
        FadeProcess();
    }

    // シーン遷移が完了したときにフェードインを実行するように設定
    private void FadeIn_and_SceneChange(Scene scene, LoadSceneMode mode)
    {
        // フェードインのフラグを上げる
        _isFadeIn = true;

        _afterScene = null;
    }

    /// <summary>
    /// フェードアウトをスタートする関数
    /// 遷移先のシーン名を指定
    /// </summary>
    /// <param name="nextScene">遷移先のシーン名</param>
    public void FadeOut(SceneNameClass.SceneName nextScene, float fadeTime)
    {
        // フェードアウトのフラグを上げる
        _isFadeOut = true;
        _fadeTime = fadeTime;

        if (nextScene == SceneNameClass.SceneName.Null)
        {
            // シーンは遷移しない
        }
        else
        {
            // 遷移先のシーン名をEnumから文字列に変換
            _afterScene = SceneNameClass.SceneNameToString[nextScene];
        }
    }

    /// <summary>
    /// フェード処理をする関数
    /// </summary>
    private void FadeProcess()
    {
        // フェードインしているとき
        if (_isFadeIn)
        {
            // 不透明度を徐々に下げる
            _alpha -= _fadeTime * Time.deltaTime;
            // 不透明度を画像に反映
            SetAlpha();

            // 不透明度が0より小さくなった時
            if (_alpha < 0)
            {
                // 不透明度を0にそろえる
                _alpha = 0;
                // フェードインのフラグを下げる
                _isFadeIn = false;
                _sceneFadeManager.SetIsFade(false);

                if (_countStart != null)
                {
                    _countStart();
                    _countStart = null;
                }

                if (_movieStart != null)
                {
                    _movieStart();
                    _movieStart = null;
                }
            }
        }
        // フェードアウトしているとき
        if (_isFadeOut)
        {
            // 不透明度を徐々に上げる
            _alpha += _fadeTime * Time.deltaTime;
            // 不透明度を画像に反映
            SetAlpha();

            // 不透明度が1より大きくなった時
            if (_alpha > 1)
            {
                // 不透明度を1にそろえる
                _alpha = 1;
                // フェードアウトのフラグを上げる
                _isFadeOut = false;
                // 次のシーンをロードする
                if (_afterScene != null) SceneManager.LoadScene(_afterScene);
                else _isFadeIn = true;

                if (_cameraChange != null)
                {
                    _cameraChange();
                    _cameraChange = null;
                }
            }
        }
    }

    public void RegisterAction(Action cameraChange,
                                Action countStart,
                                Action movieStart)
    {
        _cameraChange = cameraChange;
        _countStart = countStart;
        _movieStart = movieStart;
    }

    /// <summary>
    /// 不透明度を画像に反映する関数
    /// </summary>
    private void SetAlpha()
    {
        _fadeImage.color = new Color(0, 0, 0, _alpha);
    }
}
