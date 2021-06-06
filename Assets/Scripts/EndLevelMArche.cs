using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevelMArche : MonoBehaviour
{
    public GameObject maskManager;
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player1")
        {
            maskManager.GetComponent<MaskManager>().p1Finish = true;
        }

        if (other.tag == "Player2")
        {
            maskManager.GetComponent<MaskManager>().p2Finish = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player1")
        {
            maskManager.GetComponent<MaskManager>().p1Finish = false;
        }

        if (other.tag == "Player2")
        {
            maskManager.GetComponent<MaskManager>().p2Finish = false;
        }
    }
}
