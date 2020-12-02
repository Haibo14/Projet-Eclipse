using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableObject : MonoBehaviour
{
    GameObject fusedPlayer;

    bool fusing;

    void Start()
    {
        fusedPlayer = GameObject.FindGameObjectWithTag("FusedPlayer");
    }

    // Update is called once per frame
    void Update()
    {
        fusing = fusedPlayer.GetComponent<Players>().fusing;
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.tag == "Player1" || gameObject.tag == "Player2")
        {

            if (fusing == true)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
