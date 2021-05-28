using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public GameObject hoist;

    public float speedRotation;
    float multiplicator;
    float rotation;

    bool pressed;

    void Start()
    {
        pressed = false;
    }


    void Update()
    {
        if (pressed == true)
        {
            if (hoist.transform.eulerAngles.y >= 90 && hoist.transform.eulerAngles.y <= 269)
            {
                multiplicator = 0;
            }
            else
            {
                multiplicator = 1;
            }
            
        }
        else if (pressed == false)
        {
            if (hoist.transform.eulerAngles.y <= 270 && hoist.transform.eulerAngles.y >= 91)
            {
                multiplicator = 0;
            }
            else
            {
                multiplicator = -1;
            }
        }

        rotation = multiplicator * speedRotation * Time.deltaTime;

        hoist.transform.RotateAround(hoist.transform.position, Vector3.up, rotation);

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
