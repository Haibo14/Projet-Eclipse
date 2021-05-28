using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBarricade : MonoBehaviour
{
    bool playOnce;
    void Start()
    {
        playOnce = true;
    }

    void Update()
    {
        if (playOnce == true)
        {
            if (transform.GetComponent<BoxCollider>().enabled == false)
            {
                Destroy(transform.GetChild(0).gameObject);
                Destroy(transform.GetChild(1).gameObject);
                Destroy(transform.GetChild(2).gameObject);
                playOnce = false;
            }
        }
    }

    
}
