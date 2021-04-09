﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject Player1;
    public GameObject Player2;
    public GameObject FusedPlayer;
    GameObject deadPlayer;

    public Transform lastCheckPoint;

    int ID;

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
            ID = 0;
        }
        else if (!player2Live && player1Live)
        {
            timer += Time.deltaTime;
            deadPlayer = Player2;
            ID = 1;
        }
        else if (!player1Live && !player2Live)
        {
            RespawnBoth();
        }

        if(timer >= timeRevive)
        {
            RespawnPlayer(deadPlayer, ID);
        }
    }

    public void RespawnPlayer(GameObject player, int playerID)
    {
        timer = 0;

        if (player == Player1)
        {
            player.transform.position = Player2.transform.position;
        }
        else
        {
            player.transform.position = Player1.transform.position;
        }

        player.GetComponent<PlayerScript>().firstTouch = false;
        player.GetComponent<PlayerScript>().IsAlive = true;

        if (playerID == 0)
        {
            player1Live = true;
        }
        else if (playerID == 1)
        {
            player2Live = true;
        }
    }

    public void RespawnBoth()
    {
        timer = 0;

        Player1.transform.position = lastCheckPoint.position;
        Player2.transform.position = lastCheckPoint.position;

        Player1.GetComponent<PlayerScript>().merged = false;
        Player2.GetComponent<PlayerScript>().merged = false;
        FusedPlayer.GetComponent<Players>().merged = false;
        FusedPlayer.GetComponent<Players>().changeState = true;
        FusedPlayer.transform.GetChild(0).gameObject.SetActive(false);

        Player1.GetComponent<PlayerScript>().firstTouch = false;
        Player2.GetComponent<PlayerScript>().firstTouch = false;

        Player1.GetComponent<PlayerScript>().IsAlive = true;
        Player2.GetComponent<PlayerScript>().IsAlive = true;
        FusedPlayer.GetComponent<Players>().IsAlive = true;

        player1Live = true;
        player2Live = true;
    }

    
}
