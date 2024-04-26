using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{
    public float Timer = 99;
    //private Text timerText;
    [SerializeField]
    private Text timerText;
    // Start is called before the first frame update
    private void Start()
    {
        if(timerText == null) 
        {
            timerText = GetComponent<Text>();
            // timerText に Text コンポーネントを代入する
        }
    }

    // Update is called once per frame
    void Update()
    {
        Timer -= Time.deltaTime;
        //timerText.text = ((int)Timer).ToString();
        timerText.text = string.Format("{0:#}", Timer);

        if (Timer <= 0)
        {
            timerText.text = "は";
        }

    }

}
