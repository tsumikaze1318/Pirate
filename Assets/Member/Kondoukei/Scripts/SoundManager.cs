using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BGMType 
{
    BGM1,
    BGM2, 
    BGM3, 
    BGM4,
}
//シリアライズ化
[System.Serializable]
struct BGMData
{
    public BGMType Type;
    public AudioClip Clip;
    [Range(0, 1)]
    public float Volume;
    public bool Loop;
}
public enum SEType
{
    SE1,
    SE2,
    SE3,
    SE4,
    SE5,
    SE6
}
//シリアライズ化
[System.Serializable]
struct SEData
{
    public SEType Type;
    public AudioClip Clip;
    [Range (0, 1)]
    public float Volume;
    public bool Loop;
}

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    public static SoundManager Instance { get => instance; }
    //ゲーム内で再生するBGMのリスト
    [SerializeField]
    private List<BGMData> bgmDataList = new List<BGMData>();

    [SerializeField]
    private List<SEData> seDataList = new List<SEData>();

    [SerializeField]
    private AudioSource bgmSource = null;

    [SerializeField]
    private AudioSource seSource = null;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        { instance = this; }
        else return;
        PlayBgm(BGMType.BGM1);
        DontDestroyOnLoad(this.gameObject);
    }

    private void Update()
    {
        //テスト
        if (Input.GetKeyDown(KeyCode.Alpha1))
            SoundManager.Instance.PlaySe(SEType.SE2);
        if (Input.GetKey(KeyCode.Alpha2))
            SoundManager.Instance.PlayLoopSe(SEType.SE1);
        if (Input.GetKeyDown(KeyCode.Alpha3))
            SoundManager.Instance.StopBgm();
    }
    //BGM再生
    public void PlayBgm(BGMType type)
    {
        var bgm = bgmDataList[(int)type];
        bgmSource.clip = bgm.Clip;
        bgmSource.volume = bgm.Volume;
        bgmSource.loop = bgm.Loop;
        bgmSource.Play();
    }
    public void StopBgm()
    {
        bgmSource.Stop();
    }

    public void PlaySe(SEType type) 
    {
        var se = seDataList[(int)type];
        seSource.clip = se.Clip;
        seSource.volume = se.Volume;
        seSource.PlayOneShot(se.Clip);
    }
    //サウンドループ再生
    public void PlayLoopSe(SEType type)
    {
        var se = seDataList[(int)type];
        seSource.clip = se.Clip;
        seSource.loop = se.Loop;
        seSource.volume = se.Volume;
        seSource.Play();
    }

    // 市原追記
    public void StopLoopBgm()
    {
        seSource.Stop();
    }
}
