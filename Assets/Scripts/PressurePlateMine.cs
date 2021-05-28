using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateMine : MonoBehaviour
{
    public bool pressed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1" || other.tag == "Player2")
        {
            pressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player1" || other.tag == "Player2")
        {
            pressed = false;
        }
    }
}
