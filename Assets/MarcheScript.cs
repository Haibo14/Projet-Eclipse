using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Cinemachine;

public class MarcheScript : MonoBehaviour
{

    public VideoPlayer Cinematic1;

    public CinemachineVirtualCamera gameCam;
    public CinemachineVirtualCamera gameCam1;
    public CinemachineVirtualCamera gameCam2;
    public Camera mainCam;

    bool play;

    // Start is called before the first frame update
    void Start()
    {
        play = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(play == false)
        {
            Time.timeScale = 0;
            mainCam.enabled = true;
            gameCam.enabled = true;
            gameCam1.enabled = false;
            gameCam2.enabled = false;

        }
        else
        {
            Time.timeScale = 1;
            mainCam.enabled = false;
            gameCam.enabled = false;
            gameCam1.enabled = true;
            gameCam2.enabled = true;
        }

        Cinematic1.loopPointReached += OnMovieFinished;

        //the action on finish
        void OnMovieFinished(UnityEngine.Video.VideoPlayer vp)
        {
            vp.playbackSpeed = vp.playbackSpeed / 10.0F;
            play = true;

            Cinematic1.Stop();
        }
    }


}
