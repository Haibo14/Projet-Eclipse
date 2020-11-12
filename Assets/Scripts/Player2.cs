using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2 : MonoBehaviour
{
    public GameObject fusedPlayer;

    bool merged;
    bool fusing;

    void Start()
    {
        
    }

    
    void Update()
    {
        merged = fusedPlayer.GetComponent<Players>().merged;
        fusing = fusedPlayer.GetComponent<Players>().fusing;
    }

    void OnCollisionEnter(Collision other)
    {

        if (other.collider.gameObject.tag != "Player1" || other.collider.gameObject.tag != "FusedPlayer")
        {
            if (merged == false)
            {
                fusedPlayer.GetComponent<Players>().p2CanJump = true;
            }
        }
    }

    
}
