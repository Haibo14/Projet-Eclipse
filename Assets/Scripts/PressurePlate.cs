using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    public AudioSource source;
    public AudioClip grueSon;

    public GameObject hoist;

    public float speedRotation;
    float multiplicator;
    float rotation;

    bool pressed;

    void Start()
    {
        pressed = false;

        source.clip = grueSon;
        source.Play();
        source.volume = 0;
    }


    void Update()
    {
        if (pressed == true)
        {
            if (hoist.transform.eulerAngles.y >= 180 && hoist.transform.eulerAngles.y <= 359)
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
            if (hoist.transform.eulerAngles.y <= 360 && hoist.transform.eulerAngles.y >= 181)
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

        if (multiplicator == 0)
        {

            source.volume = 0;
        }
        else
        {

            source.volume = 1;
        }

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
