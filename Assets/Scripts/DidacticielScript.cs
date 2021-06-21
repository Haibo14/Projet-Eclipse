using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Cinemachine;
using UnityEngine.Playables;
using UnityEngine.Video;

public class DidacticielScript : MonoBehaviour
{
    public VideoPlayer Cinematic1;
    public VideoPlayer Cinematic2;
    public CinemachineVirtualCamera gameCam;
    public CinemachineVirtualCamera cinematicCam_1;
    public PlayableDirector directorCam_1;

    public GameObject player1;
    public GameObject player2;
    public GameObject fusedPlayer;

    public GameObject chefPrefab;

    public Transform spawnSpot;

    public TextMeshProUGUI text0;
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;
    public TextMeshProUGUI text4;
    public TextMeshProUGUI text5;

    public int didacticielStep;
    double timeC1;
    double timeC2;

    bool startDidacticiel;

    void Start()
    {
        startDidacticiel = true;
        text0.enabled = false;
        text1.enabled = false;
        text2.enabled = false;
        text3.enabled = false;
        text4.enabled = false;
        text5.enabled = false;

        timeC1 = Cinematic1.clip.length;
        timeC2 = Cinematic2.clip.length;

        gameCam.enabled = true;
        cinematicCam_1.enabled = false;

    }


    void Update()
    {
        if (startDidacticiel == true)
        {
            if(didacticielStep == 0)
            {
                Time.timeScale = 0;

            }
            else if (didacticielStep == 1)
            {
                //montrer comment faire

                text0.enabled = true;

                if (fusedPlayer.GetComponent<Players>().merged == true)
                {
                    didacticielStep++;
                    text0.enabled = false;

                    gameCam.enabled = true;
                    cinematicCam_1.enabled = false;
                }


            }
            else if (didacticielStep  == 2)
            {
                //expliquer comment faire

                text1.enabled = true;

                if (fusedPlayer.GetComponent<Players>().splitting == true)
                {
                    didacticielStep++;
                    text1.enabled = false;
                }

            }
            else if (didacticielStep == 3)
            {
                //demander de refusionner

                text2.enabled = true;

                if (fusedPlayer.GetComponent<Players>().merged == true)
                {

                    text2.enabled = false;
                    didacticielStep++;
                }

            }
            else if (didacticielStep == 4)
            {
                //défusionner + joystick
                text3.enabled = true;

                if (fusedPlayer.GetComponent<Players>().splitting == true && fusedPlayer.GetComponent<Players>().move != Vector3.zero)
                {
                    text3.enabled = false;
                    didacticielStep++;
                }

            }
            else if (didacticielStep == 5)
            {
                //casser barricade

                text4.enabled = true;

                GameObject barricade = GameObject.FindGameObjectWithTag("Breakable_Barricade");
                if (barricade == null)
                {
                    text4.enabled = false;
                    Cinematic2.Play();
                    Time.timeScale = 0;
                }
                
            }
            else if (didacticielStep == 7)
            {
                //apparition chef + message fuite
                gameCam.enabled = true;
                text5.enabled = true;

                GameObject enemy = Instantiate(chefPrefab, spawnSpot.position, spawnSpot.rotation);


                enemy.GetComponent<EnemyScript>().Spot(1, player2.transform);
                enemy.GetComponent<EnemyScript>().Spot(0, player1.transform);
                enemy.GetComponent<EnemyScript>().spotted = true;

                didacticielStep++;
            }


            if (cinematicCam_1.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition >= 1 && cinematicCam_1.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition < 10)
            {
                directorCam_1.Pause();
                directorCam_1.time = 0;
                cinematicCam_1.GetCinemachineComponent<CinemachineTrackedDolly>().m_PathPosition = 10;
                cinematicCam_1.enabled = false;
                didacticielStep++;
            }

            Cinematic1.loopPointReached += OnMovieFinished;

            //the action on finish
            void OnMovieFinished(UnityEngine.Video.VideoPlayer vp)
            {
                vp.playbackSpeed = vp.playbackSpeed / 10.0F; 
                if(didacticielStep == 0)
                {

                    didacticielStep++;
                }
                if(Time.timeScale == 0)
                {
                    Time.timeScale = 1;
                }
                if (Cinematic1 != null)
                {
                    Destroy(Cinematic1.gameObject);

                }
            }

            Cinematic2.loopPointReached += OnMovieFinished2;

            //the action on finish
            void OnMovieFinished2(UnityEngine.Video.VideoPlayer vp)
            {
                vp.playbackSpeed = vp.playbackSpeed / 10.0F;
                if (didacticielStep == 5)
                {
                    cinematicCam_1.enabled = true;
                    directorCam_1.Play();
                    didacticielStep++;
                }
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
}
