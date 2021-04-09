using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    GameObject respawnManager;

    bool playOnce;

    void Start()
    {
        playOnce = true;
        respawnManager = GameObject.FindGameObjectWithTag("RespawnManager");
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

                playOnce = false;
            }
        }
    }
}
