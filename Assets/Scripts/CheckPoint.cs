using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    GameObject respawnManager;
    GameObject manager;

    bool playOnce;

    void Start()
    {
        playOnce = true;
        respawnManager = GameObject.FindGameObjectWithTag("RespawnManager");
        manager = GameObject.FindGameObjectWithTag("Manager");
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playOnce == true)
        {

            if (other.tag == "Player1" || other.tag == "Player" || other.tag == "FusedPlayer")
            {
                respawnManager.GetComponent<Respawn>().lastCheckPoint = transform;
                manager.GetComponent<CameraManagerFactory>().i += 1;
                manager.GetComponent<CameraManagerFactory>().cinematic = true;

                playOnce = false;
            }
        }
    }
}
