using UnityEngine;

public class CameraSplit : MonoBehaviour
{
    [SerializeField, Range(1, 8)]
    private int _useDisplayCount = 4;

    private void Awake()
    {
        // ディスプレイをアクティベートする
        int count = Mathf.Min(Display.displays.Length, _useDisplayCount);

        for (int i = 0; i < count; ++i)
        {
            Display.displays[i].Activate();
        }
    }
}
