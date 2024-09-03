using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ImageColor : MonoBehaviour
{
    private static ImageColor instance;
    public static ImageColor Instance
    {
        get
        {
            if (instance == null) instance = FindObjectOfType<ImageColor>();
            return instance;
        }
    }

    [SerializeField]
    public List<Image> _images = new List<Image>();

    private int _count = 0;

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
        Debug.Log(_count);

        if (_count == GameManager.Instance.Attendance && !Ready)
        {
            Ready = true;
            GameManager.Instance.PlayersReady();
        }
    }
}
