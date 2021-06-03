using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiesRunning : MonoBehaviour
{
    public GameObject fusedPlayer;

    public float speed;

    bool running;

    void Start()
    {
        running = false;
        this.GetComponent<MeshRenderer>().enabled = false;
    }


    void Update()
    {
        if (fusedPlayer.transform.position.z >= 175)
        {
            running = true;
        }


        if (running == true)
        {
            this.GetComponent<MeshRenderer>().enabled = true;
            transform.Translate(transform.forward * speed);
        }

        if (this.transform.position.z >= 260)
        {
            running = false;
            transform.Translate(transform.up * -speed);
        }
    }
}
