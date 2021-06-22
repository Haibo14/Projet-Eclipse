using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using Cinemachine;
using UnityEngine.Playables;

public class PilarManager : MonoBehaviour
{
    public Animator animator;
    public Animator animator2;

    public VideoPlayer Cinematic1;
    public VideoPlayer Cinematic2;
    public CinemachineVirtualCamera cinematicCam_1;
    public CinemachineVirtualCamera cinematicCam_2;
    public PlayableDirector directorCam_1;
    public PlayableDirector directorCam_2;

    public bool canDie;

    GameObject[] pilarList;
    void Start()
    {
        Time.timeScale = 0;

        Cinematic1.Play();
        Cinematic2.Pause();
        cinematicCam_1.enabled = false;
        cinematicCam_2.enabled = false;
        directorCam_2.enabled = false;
        canDie = false;
    }


    void Update()
    {
        pilarList = GameObject.FindGameObjectsWithTag("Pilar");


        if(pilarList.Length == 1)
        {
            cinematicCam_2.enabled = true;
            directorCam_2.enabled = true;
            GameObject boss = GameObject.FindGameObjectWithTag("Boss");
            boss.GetComponent<BossScript>().StopCoroutine("Rock");
            canDie = true;
            

        }

        if (cinematicCam_1.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition >= 5 && cinematicCam_1.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition < 10)
        {
            directorCam_1.Pause();
            directorCam_1.time = 0;
            cinematicCam_1.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 10;
            cinematicCam_1.enabled = false;
        }

        if (cinematicCam_2.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition >= 1 && cinematicCam_2.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition < 10 && canDie == true)
        {
            canDie = false;
            GameObject boss = GameObject.FindGameObjectWithTag("Boss");
            boss.GetComponent<BossScript>().animator.SetBool("death", true);
            animator2.SetBool("Play", true);
            directorCam_2.Pause();
            directorCam_2.time = 0;
            cinematicCam_2.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 10;
            cinematicCam_2.enabled = false;
        }

        Cinematic1.loopPointReached += OnMovieFinished;

        //the action on finish
        void OnMovieFinished(UnityEngine.Video.VideoPlayer vp)
        {
            vp.playbackSpeed = vp.playbackSpeed / 10.0F;
            
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            if (Cinematic1 != null)
            {
                Destroy(Cinematic1.gameObject);

            }

            cinematicCam_1.enabled = true;
            GameObject boss = GameObject.FindGameObjectWithTag("Boss");
            boss.GetComponent<BossScript>().animator.SetBool("rage", true);

        }

        Cinematic2.loopPointReached += OnMovieFinished2;

        //the action on finish
        void OnMovieFinished2(UnityEngine.Video.VideoPlayer vp)
        {
            vp.playbackSpeed = vp.playbackSpeed / 10.0F;

            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
            }
            if (Cinematic2 != null)
            {
                Destroy(Cinematic2.gameObject);

            }

        }

    }
}
