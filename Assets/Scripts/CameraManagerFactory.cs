using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class CameraManagerFactory : MonoBehaviour
{
    public CinemachineVirtualCamera gameCam;
    public CinemachineVirtualCamera cinematicCam_1;
    public CinemachineVirtualCamera cinematicCam_2;
    public CinemachineVirtualCamera cinematicCam_3;
    public CinemachineVirtualCamera cinematicCam_4;
    public CinemachineVirtualCamera cinematicCam_5;
    public CinemachineVirtualCamera cinematicCam_6;
    public PlayableDirector directorCam_1;
    public PlayableDirector directorCam_2;
    public PlayableDirector directorCam_3;
    public PlayableDirector directorCam_4;
    public PlayableDirector directorCam_5;
    public PlayableDirector directorCam_6;
    /*
    [SerializeField] private CinemachineVirtualCamera gameCam;
    [SerializeField] private CinemachineVirtualCamera cinematicCam_1;
    [SerializeField] private CinemachineVirtualCamera cinematicCam_2;
    [SerializeField] private CinemachineVirtualCamera cinematicCam_3;
    [SerializeField] private CinemachineVirtualCamera cinematicCam_4;
    [SerializeField] private CinemachineVirtualCamera cinematicCam_5;
    [SerializeField] private CinemachineVirtualCamera cinematicCam_6;
    [SerializeField] private PlayableDirector directorCam_1;
    [SerializeField] private PlayableDirector directorCam_2;
    [SerializeField] private PlayableDirector directorCam_3;
    [SerializeField] private PlayableDirector directorCam_4;
    [SerializeField] private PlayableDirector directorCam_5;
    [SerializeField] private PlayableDirector directorCam_6;
    */

    public int i;

    public bool cinematic;

    void Start()
    {
        cinematic = true;
        i = 0;
    }

    void Update()
    {

        if(i == 0)
        {
            cinematicCam_1.enabled = cinematic;

        }
        else if(i == 1)
        {
            cinematicCam_2.enabled = cinematic;
            directorCam_2.Play();
            i += 1;
        }
        else if(i == 3)
        {
            cinematicCam_3.enabled = cinematic;
            directorCam_3.Play();
            i += 1;
        }
        else if (i == 5)
        {
            cinematicCam_4.enabled = cinematic;
            directorCam_4.Play();
            i += 1;
        }
        else if (i == 7)
        {
            cinematicCam_5.enabled = cinematic;
            directorCam_5.Play();
            i += 1;
        }
        else if (i == 9)
        {
            cinematicCam_6.enabled = cinematic;
            directorCam_6.Play();
            i += 1;
        }


        if (cinematicCam_1.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition >= 5 && cinematicCam_1.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition < 10)
        {
            cinematic = false;
            directorCam_1.Pause();
            directorCam_1.time = 0;
            cinematicCam_1.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 10;
            cinematicCam_1.enabled = false;
        }


        if (cinematicCam_2.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition >= 3 && cinematicCam_2.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition < 10)
        {
            cinematic = false;
            directorCam_2.Pause();
            directorCam_2.time = 0;
            cinematicCam_2.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 10;
            cinematicCam_2.enabled = false;
        }

        if (cinematicCam_3.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition >= 1 && cinematicCam_3.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition < 10)
        {
            cinematic = false;
            directorCam_3.Pause();
            directorCam_3.time = 0;
            cinematicCam_3.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 10;
            cinematicCam_3.enabled = false;
        }

        if (cinematicCam_4.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition >= 1 && cinematicCam_4.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition < 10)
        {
            cinematic = false;
            directorCam_4.Pause();
            directorCam_4.time = 0;
            cinematicCam_4.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 10;
            cinematicCam_4.enabled = false;
        }

        if (cinematicCam_5.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition >= 2 && cinematicCam_5.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition < 10)
        {
            cinematic = false;
            directorCam_5.Pause();
            directorCam_5.time = 0;
            cinematicCam_5.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 10;
            cinematicCam_5.enabled = false;
        }

        if (cinematicCam_6.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition >= 3 && cinematicCam_6.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition < 10)
        {
            cinematic = false;
            directorCam_6.Pause();
            directorCam_6.time = 0;
            cinematicCam_6.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 10;
            cinematicCam_6.enabled = false;
        }

        //gameCam.gameObject.GetComponent<CameraScript>().cinematic = cinematic;
    }
}
