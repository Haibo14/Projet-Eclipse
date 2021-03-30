using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    GameObject deadPlayer;

    public Transform lastCheckPoint;

    float timer;
    public float timeRevive;

    public bool player1Live;
    public bool player2Live;

    void Start()
    {
        timer = 0;
        player1Live = true;
        player2Live = true;
    }


    void Update()
    {
        if (!player1Live && player2Live)
        {
            timer += Time.deltaTime;
            deadPlayer = Player1;
        }
        else if (!player2Live && player1Live)
        {
            timer += Time.deltaTime;
            deadPlayer = Player2;
        }
        else if (!player1Live && !player2Live)
        {
            RespawnBoth();
        }

        if(timer >= timeRevive)
        {
            RespawnPlayer(deadPlayer);
        }
    }

    public void RespawnPlayer(GameObject player)
    {
        timer = 0;
        player.transform.position = lastCheckPoint.position;
        player.GetComponent<PlayerScript>().IsAlive = true;
    }

    public void RespawnBoth()
    {
        timer = 0;
    }

    
}
