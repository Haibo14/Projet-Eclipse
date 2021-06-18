using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetRotation : MonoBehaviour
{
    public GameObject rockPlace;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = rockPlace.transform.position;
    }
}
