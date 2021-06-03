using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskPiece : MonoBehaviour
{
    GameObject manager;

    void Start()
    {

        manager = GameObject.FindGameObjectWithTag("Manager");
    }


    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player1" || other.tag == "Player2")
        {
            manager.GetComponent<MaskManager>().piecesCount++;
            Destroy(gameObject);
        }
    }
}
