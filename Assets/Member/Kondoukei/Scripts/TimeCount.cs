using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeCount : MonoBehaviour
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
        // 5/24 追記しました 横田
        if (!GameManager.Instance.GameStart) return;
        if (GameManager.Instance.GameEnd) return;
        Timer -= Time.deltaTime;
        //timerText.text = ((int)Timer).ToString();
        timerText.text = string.Format("{0:#}", Timer);

        if (Timer < 0)
        {
            Timer = 0;
            GameManager.Instance.GameEnded();
            //timerText.text = "は";
        }
    }

}
