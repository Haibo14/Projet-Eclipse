using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkSpawn : MonoBehaviour
{
    public float offset;

    void Start()
    {
        
    }


    void Update()
    {
        if (transform.position.x == 0 && transform.position.z == 0)
        {
            transform.position = new Vector3(offset, transform.position.y, transform.position.z);
        }
    }
}
