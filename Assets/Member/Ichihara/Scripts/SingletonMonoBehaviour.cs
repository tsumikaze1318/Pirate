using System;
using UnityEngine;

public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;
    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                Type t = typeof(T);
                _instance = (T)FindObjectOfType(t);
                if (_instance == null)
                {
#if UNITY_EDITOR
                    Debug.LogError($"{t} をアタッチしているゲームオブジェクトはありません。");
#endif
                }
            }
            return _instance;
        }
    }

    /// <summary>
    /// 継承先のクラスから base.Awake() で呼び出す
    /// </summary>
    protected void Awake()
    {
        // 他のゲームオブジェクトにアタッチされているかを調べる。
        // アタッチされている場合は破棄する。
        if (this != Instance)
        {
            Destroy(this);
#if UNITY_EDITOR
            Debug.LogWarning(
                $"{typeof(T)} は既に他のゲームオブジェクトにアタッチされている為、コンポーネントを破棄しました。" +
                $"現在アタッチされているゲームオブジェクトは、{Instance.gameObject.name} です。");
#endif
            return;
        }
    }
}