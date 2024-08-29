using System.Collections;
using System.Collections.Generic;
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

    private int num = 0;

    void Update()
    {
        var ctrl = Input.GetJoystickNames();
        
        if (num != GameManager.Instance.Players.Count)
        {
            //_images[ctrl.Length].enabled = true;
            _images[num].enabled = true;
            num = GameManager.Instance.Players.Count;
        }
    }
}
