using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MovieController : MonoBehaviour
{
    [SerializeField] VideoPlayer videoPlayer;

    public void FinishPlayingVideo(VideoPlayer vp)
    {
        videoPlayer.Stop();
        GameManager.Instance.FinishMovie();
    }
}
