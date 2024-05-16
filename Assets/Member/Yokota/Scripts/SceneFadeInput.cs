using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SceneFadeInput : MonoBehaviour
{
    [SerializeField]
    private SceneFadeManager fadeManager;

    public void GoGameScene(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        fadeManager.FadeOut(SceneNameClass.SceneName.Game);
    }

    public void GoTitleScene(InputAction.CallbackContext context)
    {
        if (!context.performed) return;

        fadeManager.FadeOut(SceneNameClass.SceneName.Title);
    }
}
