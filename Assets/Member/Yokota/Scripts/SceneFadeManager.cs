using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFadeManager : MonoBehaviour
{
    // フェードアウトの開始、終了を伝えるフラグ
    private bool isFadeOut;
    // フェードインの開始、終了を伝えるフラグ
    private bool isFadeIn;

    // フェードする画像のイメージ
    [SerializeField]
    private Image fadeImage;

    // フェードする画像の不透明度
    private float alpha = 0;

    // 遷移先のシーン名
    private string afterScene;

    // フェードする時間
    private float fadeTime = 1f;

    private void Start()
    {
        DontDestroyOnLoad(this);
        SetAlpha();
        // シーン遷移が完了したときにフェードインを実行するように設定
        SceneManager.sceneLoaded += FadeIn;
    }

    private void Update()
    {
        // フェード処理
        FadeProcess();
    }

    // シーン遷移が完了したときにフェードインを実行するように設定
    private void FadeIn(Scene scene, LoadSceneMode mode)
    {
        // フェードインのフラグを上げる
        isFadeIn = true;
    }

    /// <summary>
    /// フェードアウトをスタートする関数
    /// 遷移先のシーン名を指定
    /// </summary>
    /// <param name="nextScene">遷移先のシーン名</param>
    public void FadeOut(SceneNameClass.SceneName nextScene)
    {
        // フェードアウトのフラグを上げる
        isFadeOut = true;
        // 遷移先のシーン名をEnumから文字列に変換
        afterScene = SceneNameClass.SceneNameToString[nextScene];
    }

    /// <summary>
    /// フェード処理をする関数
    /// </summary>
    private void FadeProcess()
    {
        // フェードインしているとき
        if (isFadeIn)
        {
            // 不透明度を徐々に下げる
            alpha -= fadeTime * Time.deltaTime;
            // 不透明度を画像に反映
            SetAlpha();

            // 不透明度が0より小さくなった時
            if (alpha < 0)
            {
                // 不透明度を0にそろえる
                alpha = 0;
                // フェードインのフラグを下げる
                isFadeIn = false;
            }
        }
        // フェードアウトしているとき
        if (isFadeOut)
        {
            // 不透明度を徐々に上げる
            alpha += fadeTime * Time.deltaTime;
            // 不透明度を画像に反映
            SetAlpha();

            // 不透明度が1より大きくなった時
            if (alpha > 1)
            {
                // 不透明度を1にそろえる
                alpha = 1;
                // フェードアウトのフラグを上げる
                isFadeOut = false;
                // 次のシーンをロードする
                SceneManager.LoadScene(afterScene);
            }
        }
    }

    /// <summary>
    /// 不透明度を画像に反映する関数
    /// </summary>
    private void SetAlpha()
    {
        fadeImage.color = new Color(0, 0, 0, alpha);
    }
}
