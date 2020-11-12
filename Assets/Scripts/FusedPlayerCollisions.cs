using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusedPlayerCollisions : MonoBehaviour
{
    bool merged;
    bool p1CanJump;
    bool p2CanJump;

    void Start()
    {
        
    }


    void Update()
    {
        merged = transform.parent.gameObject.GetComponent<Players>().merged;
    }

    #region collisions

    void OnCollisionEnter(Collision other)
    {
        if (merged == true)
        {

            transform.parent.gameObject.GetComponent<Players>().p1CanJump = true;
            transform.parent.gameObject.GetComponent<Players>().p2CanJump = true;

        }
    }

    void OnCollisionExit(Collision collision)
    {

    }

    #endregion
}
