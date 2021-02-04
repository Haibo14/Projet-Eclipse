using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushVision : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerStay(Collider other)
    {
        this.GetComponent<MeshRenderer>().material.color = new Color(0.27f, 0.53f, 0.25f, 0.5f);
    }

    private void OnTriggerExit(Collider other)
    {
        this.GetComponent<MeshRenderer>().material.color = new Color(0.27f, 0.53f, 0.25f, 1.0f);
    }
}
