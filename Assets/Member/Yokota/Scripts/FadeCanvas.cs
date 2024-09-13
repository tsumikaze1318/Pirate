using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
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

    private SceneFadeManager _sceneFadeManager;

    private Action _afterFadeInAction;
    private Action _afterFadeOutAction;

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
    public void FadeOut(SceneNameClass.SceneName nextScene, Action afterFadeOutAction = null, Action afterFadeInAction = null)
    {
        // フェードアウトのフラグを上げる
        _isFadeOut = true;

        _afterFadeInAction = afterFadeInAction;
        _afterFadeOutAction = afterFadeOutAction;

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
    private async void FadeProcess()
    {
        // フェードインしているとき
        if (_isFadeIn)
        {
            // 不透明度を徐々に下げる
            _alpha -= Time.deltaTime / _sceneFadeManager._fadeTime;
            // 不透明度を画像に反映
            SetAlpha();

            // 不透明度が0より小さくなった時
            if (_alpha < 0)
            {
                // 不透明度を0にそろえる
                _alpha = 0;
                _sceneFadeManager._fadeTime = 1f;
                // フェードインのフラグを下げる
                _isFadeIn = false;
                _sceneFadeManager.SetIsFade(false);

                if (_afterFadeInAction != null)
                {
                    _afterFadeInAction();
                    _afterFadeInAction = null;
                }
            }
        }
        // フェードアウトしているとき
        if (_isFadeOut)
        {
            // 不透明度を徐々に上げる
            _alpha += Time.deltaTime / _sceneFadeManager._fadeTime;
            // 不透明度を画像に反映
            SetAlpha();

            // 不透明度が1より大きくなった時
            if (_alpha > 1)
            {
                // 不透明度を1にそろえる
                _alpha = 1;
                // フェードアウトのフラグを上げる
                _isFadeOut = false;

                if (_afterFadeOutAction != null)
                {
                    _afterFadeOutAction();
                    _afterFadeOutAction = null;
                }

                await Task.Delay(100);

                // 次のシーンをロードする
                if (_afterScene != null) SceneManager.LoadScene(_afterScene);
                else _isFadeIn = true;
            }
        }
    }

    /// <summary>
    /// 不透明度を画像に反映する関数
    /// </summary>
    private void SetAlpha()
    {
        _fadeImage.color = new Color(0, 0, 0, _alpha);
    }
}
