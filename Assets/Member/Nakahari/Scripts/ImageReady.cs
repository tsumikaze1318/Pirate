using System.Collections.Generic;
using System.Threading.Tasks;
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
    // readyImageのList
    [SerializeField]
    public List<Image> _images = new List<Image>();
    // 押した人数
    public int _count = 0;
    // 準備ができているか
    public bool Ready = false;

    void Update()
    {
        ReadyPlayer();
    }

    /// <summary>
    /// ボタンが押されたらそれぞれのプレイヤーの UI に Image を表示する
    /// ボタンを押したらコントローラーが振動する
    /// </summary>
    private async void ReadyPlayer()
    {
        foreach (var input in DeviceManager.Instance.GamepadsDic)
        {
            // ボタンが押された際に image が表記されてない場合
            if (!_images[input.Key - 1].enabled && input.Value.aButton.isPressed)
            {
                // 押したコントローラーと同じ番号の image を true にする
                _images[input.Key - 1].enabled = input.Value.aButton.isPressed;
                // コントローラーを震わせる
                input.Value.SetMotorSpeeds(10.0f, 10.0f);
                // 0.25 秒ディレイを入れる
                await Task.Delay(250);
                // コントローラーの振動を止める
                input.Value.SetMotorSpeeds(0f, 0f);
                // SE を流す
                SoundManager.Instance.PlaySe(SEType.SE1);
                _count++;
            }
        }
        await Task.Yield();
    }
}
