using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ImageReady : MonoBehaviour
{
    private static ImageReady instance;
    public static ImageReady Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<ImageReady>();
            return instance;
        }
    }

    [SerializeField]
    public List<Image> _images = new List<Image>();

    public int _count = 0;

    public bool Ready = false;

    void Update()
    {
        foreach(var input in DeviceManager.Instance.Gamepads)
        {
            if (!_images[input.Key-1].enabled && input.Value.aButton.isPressed)
            {
                _images[input.Key - 1].enabled = input.Value.aButton.isPressed;
                SoundManager.Instance.PlaySe(SEType.SE1);
                _count++;
            }
        }
    }
}
