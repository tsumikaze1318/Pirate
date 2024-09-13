using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FadeUi : MonoBehaviour
{
    private Image[] _images;
    [SerializeField]
    private float _time;

    [SerializeField]
    private float _uiSpeed;

    private float _border = 700;
    // Start is called before the first frame update
    void Start()
    {
        _images = GetComponentsInChildren<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.TimeLimit <= _time)
        {
            foreach(Image image in _images)
            {
                if (image.rectTransform.localPosition.y > _border) return;
                    var speed = _uiSpeed * Time.deltaTime;
                image.rectTransform.position += new Vector3(0, speed, 0);
            }
        }
    }
}
