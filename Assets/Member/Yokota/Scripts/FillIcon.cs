using UnityEngine;
using UnityEngine.UI;

public class FillIcon : MonoBehaviour
{
    private Image[] _icons;

    private void Start()
    {
        var icons = GetComponentsInChildren<Image>();
        _icons = icons;
    }

    public void SetIconFillPercentage(int iconNum, float percentage)
    {
        _icons[iconNum].fillAmount = percentage;
    }
}
