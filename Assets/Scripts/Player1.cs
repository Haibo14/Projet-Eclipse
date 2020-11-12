using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    public GameObject fusedPlayer;

    bool fusing;
    bool merged;

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

        if(other.collider.gameObject.tag != "Player2" || other.collider.gameObject.tag != "FusedPlayer")
        {
            if (merged == false)
            {
                fusedPlayer.GetComponent<Players>().p1CanJump = true;
            }
        }
        
    }

    
}
