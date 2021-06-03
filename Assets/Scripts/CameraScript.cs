using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Playables;

public class CameraScript : MonoBehaviour
{

    public CinemachineVirtualCamera gameCam_0;
    public CinemachineVirtualCamera gameCam_1;
    public CinemachineVirtualCamera gameCam_2;

    public Camera gameCam0;

    public GameObject fusedPlayer;
    GameObject player1;
    GameObject player2;
    GameObject respawnManager;

    Vector3 target;

    public Vector3 moveP1;
    public Vector3 moveP2;
    public Vector3 move;

    public float z;
    float yP1;
    float yP2;
    float yGround;
    public float y;
    public float x;
    public float cameraSmoothSpeed;

    float distanceX;
    float distanceY;
    float distanceZ;
    float totalDistance;
    public float checkDistance;
    public float checkDistanceCam;

    bool merged;
    bool p1Life;
    bool p2Life;

    public bool cinematic;

    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1_Script");
        player2 = GameObject.FindGameObjectWithTag("Player2_Script");
        respawnManager = GameObject.FindGameObjectWithTag("RespawnManager");

        target = new Vector3(fusedPlayer.transform.position.x, yGround + y, fusedPlayer.transform.position.z - z);
    }

    void Update()
    {

        if (cinematic == false)
        {
            distanceX = player1.transform.position.x - player2.transform.position.x;
            distanceY = player1.transform.position.y - player2.transform.position.y;
            distanceZ = player1.transform.position.z - player2.transform.position.z;

            totalDistance = (Mathf.Abs(distanceX) + Mathf.Abs(distanceZ));

            if (totalDistance >= checkDistance)
            {
                gameCam0.enabled = false;
                gameCam_0.enabled = false;
                gameCam_1.enabled = true;
                gameCam_2.enabled = true;
            }
            else
            {
                gameCam_0.enabled = true;
                gameCam_1.enabled = false;
                gameCam_2.enabled = false;

                if (Vector3.Distance(gameCam0.transform.position, gameCam_0.transform.position) <= checkDistanceCam)
                {

                    gameCam0.enabled = true;
                }
            }
        }
        else
        {
            gameCam0.enabled = true;
            gameCam_0.enabled = false;
            gameCam_1.enabled = false;
            gameCam_2.enabled = false;
        }

        moveP1.x = Input.GetAxis("p1_Horizontal");
        moveP1.z = Input.GetAxis("p1_Vertical");

        moveP2.x = Input.GetAxis("p2_Horizontal");  
        moveP2.z = Input.GetAxis("p2_Vertical");

        move = moveP1 + moveP2;

        merged = fusedPlayer.GetComponent<Players>().merged;
        p1Life = respawnManager.GetComponent<Respawn>().player1Live;
        p2Life = respawnManager.GetComponent<Respawn>().player2Live;
        
        if (merged)
        {
            yGround = fusedPlayer.GetComponent<Players>().groundHeight;
        }
        else
        {
            if (p1Life)
            {
                yP1 = player1.GetComponent<PlayerScript>().groundHeight;
            }
            else
            {

                yP1 = player2.GetComponent<PlayerScript>().groundHeight;
            }

            if (p2Life)
            {

                yP2 = player2.GetComponent<PlayerScript>().groundHeight;
            }
            else
            {

                yP2 = player1.GetComponent<PlayerScript>().groundHeight;
            }


            yGround = (yP1 + yP2) / 2;
        }



        target = new Vector3(fusedPlayer.transform.position.x - x, yGround + y, fusedPlayer.transform.position.z - z);

        this.transform.position = Vector3.Lerp(transform.position, target, cameraSmoothSpeed);
    }
}
