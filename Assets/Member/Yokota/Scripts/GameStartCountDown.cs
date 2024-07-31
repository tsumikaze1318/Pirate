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

    private bool _gameStart = false;

    private float countDown = 3;

    private void Update()
    {
        //if (Input.GetMouseButtonUp(0)) isContDown = true;

        CountDown();
    }

    private void CountDown()
    {
        if (!isContDown) return;

        float preIntSecond = Mathf.Ceil(countDown);

        countDown -= Time.deltaTime;

        if (countDown < 0)
        {
            SoundManager.Instance.PlaySe(SEType.SE6);

            countDown = 0;

            countDownText.enabled = false;
            mask.enabled = false;

            if (_gameStart) GameManager.Instance.SetGameStart();

            isContDown = false;
        }

        float nowIntSecond = Mathf.Ceil(countDown);

        if (preIntSecond != nowIntSecond)
        {
            SoundManager.Instance.PlaySe(SEType.SE5);
        }

        countDownText.text = nowIntSecond.ToString("0");
    }

    public void StartCountDown(bool gameStart) 
    {
        if (isContDown) return;

        _gameStart = gameStart;
        countDown = 3;
        countDownText.enabled = gameStart;
        isContDown = true;

        SoundManager.Instance.PlaySe(SEType.SE5);
    }
}
