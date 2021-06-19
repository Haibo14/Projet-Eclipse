using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskPiece : MonoBehaviour
{
    GameObject manager;
    public GameObject player;

    void Start()
    {

        manager = GameObject.FindGameObjectWithTag("Manager");
    }


    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player1" )
        {
            player.GetComponent<ShowPieces>().pieces++;
            manager.GetComponent<MaskManager>().piecesCount++;
            Destroy(gameObject);
        }
        else if(other.tag == "Player2")
        {
            player.GetComponent<ShowPieces>().pieces++;
            manager.GetComponent<MaskManager>().piecesCount++;
            Destroy(gameObject);
        }
    }
}
