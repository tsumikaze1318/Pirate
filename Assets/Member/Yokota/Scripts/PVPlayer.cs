using UnityEngine;
using UnityEngine.Video;

public class PVPlayer : MonoBehaviour
{
    private bool _isPlay = false;
    public bool IsPlay => _isPlay;

    private float timeline = 0f;

    private VideoPlayer[] _videoPlayers;
    private Canvas[] _canvases;
    [SerializeField]
    private Camera[] _titleCameras;
    [SerializeField]
    private Camera _pvCamera;

    private void Start()
    {
        _videoPlayers = GetComponents<VideoPlayer>();
        _canvases = GetComponentsInChildren<Canvas>();

    }

    private void Update()
    {
        if (_isPlay) return;

        timeline += Time.deltaTime;

        if (timeline > 10f) 
        {
            timeline = 0f;
            SceneFadeManager.Instance.FadeStart(SceneNameClass.SceneName.Null, BGMType.Null, () => PlayVideo());
        }
    }

    public void PlayVideo()
    {
        foreach (var canvas in _canvases)
        {
            canvas.enabled = false;
        }

        _pvCamera.enabled = true;

        foreach (var camera in _titleCameras)
        {
            camera.enabled = false;
        }

        SoundManager.Instance.StopBgm();

        foreach (var player in _videoPlayers)
        {
            player.Play();
            _isPlay = true;
        }
    }

    public void StopVideo()
    {
        foreach (var player in _videoPlayers)
        {
            timeline = 0f;
            player.Stop();
            _isPlay = false;
        }

        foreach(var camera in _titleCameras)
        {
            camera.enabled = true;
        }

        _pvCamera.enabled = false;

        SoundManager.Instance.PlayBgm(BGMType.BGM1);

        foreach (var canvas in _canvases)
        {
            canvas.enabled = true;
        }
    }
}
