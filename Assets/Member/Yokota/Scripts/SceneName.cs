using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneNameClass
{
    public enum SceneName
    {
        Title,
        Game,
        Result
    }

    public Dictionary<SceneName, string> SceneNameToString
        = new Dictionary<SceneName, string>
        {
            {SceneName.Title, "Title"},
            {SceneName.Game, "Game"},
            {SceneName.Result, "Result"}
        };
}
