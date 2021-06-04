using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoleScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player1" || other.tag == "Player2" || other.tag == "FusedPlayer")
        {
            Debug.Log("Niveau fini");

            //Passer au niveau suivant
        }
    }
}
