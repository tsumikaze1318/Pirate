using UnityEngine;
using UnityEngine.UI;

public class FillIcon : MonoBehaviour
{
    private Image[] _icons;

    private void Start()
    {
        var icons = GetComponentsInChildren<Image>();
        _icons = new Image[icons.Length];
        _icons = icons;
    }

    public void SetIconFillPercentage(int iconNum, float percentage)
    {
        if (_icons == null) return;
        _icons[iconNum].fillAmount = percentage;
    }
}
