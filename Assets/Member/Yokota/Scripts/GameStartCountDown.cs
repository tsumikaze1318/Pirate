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

    private float countDown = 3;

    private void Update()
    {
        //if (Input.GetMouseButtonUp(0)) isContDown = true;

        if (!isContDown) return;

        countDown -= Time.deltaTime;

        if (countDown < 0)
        {
            countDown = 0;

            countDownText.enabled = false;
            mask.enabled = false;

            GameManager.Instance.SetGameStart();

            isContDown = false;
        }

        countDownText.text = Mathf.Ceil(countDown).ToString("0");
    }

    public void CountDown() { isContDown = true; }
}
