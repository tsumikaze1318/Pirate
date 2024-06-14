using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNameClass
{
    public enum SceneName
    {
        Null,
        Title,
        Game,
        Result
    }

    public static Dictionary<SceneName, string> SceneNameToString
        = new Dictionary<SceneName, string>
        {
            {SceneName.Title, "TitleScene"},
            {SceneName.Game, "Game"},
            {SceneName.Result, "ResultScene"}
        };
}
