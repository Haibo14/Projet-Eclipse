using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiPopScript : MonoBehaviour
{
    GameObject player1;
    GameObject player2;
    GameObject fusedPlayer;


    public float radiusUI;

    void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player1_Script");
        player2 = GameObject.FindGameObjectWithTag("Player2_Script");
        fusedPlayer = GameObject.FindGameObjectWithTag("FusedPlayer");

    }


    void Update()
    {
        
    }

}
