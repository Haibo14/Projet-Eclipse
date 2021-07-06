using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class BeginCinEnd : MonoBehaviour
{
    public VideoPlayer video;
    public AudioSource source;

    public Canvas menu;

    void Start()
    {
        video = GetComponent<VideoPlayer>();
        source.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        video.loopPointReached += OnMovieFinished;

        //the action on finish
        void OnMovieFinished(UnityEngine.Video.VideoPlayer vp)
        {
            vp.playbackSpeed = vp.playbackSpeed / 10.0F;
            source.Play();
            menu.enabled = true;
            video.Stop();
        }
    }
}
