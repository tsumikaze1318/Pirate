using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SceneFadeInput : MonoBehaviour
{
    public void GoGameScene(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        SceneFadeManager.Instance.FadeStart(SceneNameClass.SceneName.Game, BGMType.BGM1);
    }

    public void GoTitleScene(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        SceneFadeManager.Instance.FadeStart(SceneNameClass.SceneName.Title, BGMType.BGM1);
    }
}
