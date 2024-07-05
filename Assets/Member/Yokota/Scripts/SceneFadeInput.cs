using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SceneFadeInput : MonoBehaviour
{
    public void GoGameScene(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (SceneFadeManager.IsFade) return;
        SoundManager.Instance.PlaySe(SEType.SE1);
        SceneFadeManager.Instance.FadeStart(SceneNameClass.SceneName.Game, BGMType.BGM2);
    }

    public void GoTitleScene(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        if (SceneFadeManager.IsFade) return;
        SoundManager.Instance.PlaySe(SEType.SE1);
        SceneFadeManager.Instance.FadeStart(SceneNameClass.SceneName.Title, BGMType.BGM1);
    }
}
