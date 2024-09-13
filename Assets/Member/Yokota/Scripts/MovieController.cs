using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class MovieController : MonoBehaviour
{
    [SerializeField] 
    private VideoPlayer[] _videoPlayer = new VideoPlayer[4];

    private void Start()
    {
        _videoPlayer = GetComponents<VideoPlayer>();
    }

    public void SetMovie()
    {
        foreach (var video in _videoPlayer)
        {
            video.Play();
            video.Pause();
            video.loopPointReached += FinishPlayingVideo;
        }
    }

    public void StartMovie()
    {
        foreach (var video in _videoPlayer)
        {
            video.Play();
        }
    }

    public void FinishPlayingVideo(VideoPlayer vp)
    {
        foreach (var video in _videoPlayer)
        {
            video.Pause();
        }
        
        GameManager.Instance.FinishMovie();
    }

    public void MovieEnd()
    {
        foreach(var video in _videoPlayer)
        {
            video.Pause();
        }
    }
}
