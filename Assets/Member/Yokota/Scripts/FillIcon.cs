using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillIcon : MonoBehaviour
{
    private Image[] _icons;
    private Image[] _iconOutlines;
    [SerializeField]
    private GameObject _iconOutlineParent;
    private Dictionary<int, Color> _colors
        = new Dictionary<int, Color>()
        { 
            {0, Color.cyan},
            {1, Color.red},
            {2, Color.green},
            {3, Color.yellow},
        };


    private void Start()
    {
        var icons = GetComponentsInChildren<Image>();
        _icons = new Image[icons.Length];
        _icons = icons;

        var outlines = _iconOutlineParent.GetComponentsInChildren<Image>();
        _iconOutlines = new Image[outlines.Length];
        _iconOutlines = outlines;
    }

    public void SetIconFillPercentage(int iconNum, float percentage)
    {
        if (_icons == null) return;
        if (_icons[iconNum].fillAmount >= 1f) return;

         _icons[iconNum].fillAmount = percentage;
        if (_icons[iconNum].fillAmount >= 1f)
        {
            _icons[iconNum].fillAmount = 1f;
            _icons[iconNum].color = _colors[iconNum];
            _iconOutlines[iconNum].color = _colors[iconNum];

            CheckSkip();
        }
    }

    private void CheckSkip()
    {
        foreach (var icon in _icons)
        {
            if (icon.fillAmount < 1f) return; 
        }

        GameManager.Instance.FinishMovie();
    }
}
