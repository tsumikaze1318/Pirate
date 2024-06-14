using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageColor : MonoBehaviour
{
    [SerializeField]
    public List<Image> images = new List<Image>();

    private int num = 0;

    void Update()
    {
        if(num != GameManager.Instance.Players.Count)
        {
            images[num].color = Color.gray;
            num = GameManager.Instance.Players.Count;
        }
    }
}
