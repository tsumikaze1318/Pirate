using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameStartCountDown : MonoBehaviour
{
    [SerializeField]
    private Text countDownText;
    [SerializeField]
    private Image mask;

    private bool isContDown = false;

    private bool callOneTime = false;

    private float countDown = 3;

    private void Update()
    {
        //if (Input.GetMouseButtonUp(0)) isContDown = true;

        if (!isContDown) return;
        if (!callOneTime) 
        {
            SoundManager.Instance.PlaySe(SEType.SE5);
            callOneTime = true;
        }

        float preIntSecond = Mathf.Ceil(countDown);

        countDown -= Time.deltaTime;

        if (countDown < 0)
        {
            SoundManager.Instance.PlaySe(SEType.SE6);

            countDown = 0;

            countDownText.enabled = false;
            mask.enabled = false;

            GameManager.Instance.SetGameStart();

            isContDown = false;
            callOneTime = false;
        }

        float nowIntSecond = Mathf.Ceil(countDown);

        if (preIntSecond != nowIntSecond)
        {
            SoundManager.Instance.PlaySe(SEType.SE5);
        }

        countDownText.text = nowIntSecond.ToString("0");
    }

    public void CountDown() { isContDown = true; }
}
