using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Playables;

public class CameraManagerMine : MonoBehaviour
{
    public CinemachineVirtualCamera gameCam;
    public CinemachineVirtualCamera cinematicCam_1;
    public CinemachineVirtualCamera cinematicCam_2;
    public CinemachineVirtualCamera cinematicCam_3;

    public PlayableDirector directorCam_1;

    public int i;

    public bool cinematic;


    void Start()
    {
        cinematic = true;
        i = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (i == 0)
        {
            //cinematicCam_1.enabled = !cinematic;

        }
        else if (i == 1)
        {
            cinematicCam_1.enabled = cinematic;
            directorCam_1.Play();
            i += 1;
        }
        else if (i == 3)
        {
            //cinematicCam_2.enabled = cinematic;
            i += 1;
        }
        else if (i == 5)
        {
            cinematicCam_2.enabled = false;
            cinematicCam_3.enabled = cinematic;

            i += 1;
        }
        else if (i == 7)
        {
            cinematicCam_3.enabled = false;

            i += 1;
        }
        else if (i == 9)
        {
            cinematicCam_3.enabled = cinematic;

            i += 1;
        }
        else if (i == 11)
        {
            cinematicCam_3.enabled = false;

            i += 1;
        }
        else if (i == 13)
        {
            cinematicCam_3.enabled = false;

            i += 1;
        }
        else if (i == 15)
        {
            cinematicCam_3.enabled = false;
            i += 1;
        }

        if (cinematicCam_1.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition >= 2 && cinematicCam_1.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition < 10)
        {
            cinematic = false;
            directorCam_1.Pause();
            directorCam_1.time = 0;
            cinematicCam_1.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 10;
            cinematicCam_1.enabled = false;
        }

        //gameCam.gameObject.GetComponent<CameraScript>().cinematic = cinematic;
    }
}
