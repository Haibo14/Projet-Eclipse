using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableBarricade : MonoBehaviour
{
    void Start()
    {
    }

    void Update()
    {
        
        if (transform.GetComponent<BoxCollider>().enabled == false)
        {
            Destroy(gameObject);            
        }
    }

    
}
