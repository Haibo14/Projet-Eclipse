using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : MonoBehaviour
{
    [SerializeField] Animator grueAnimator;
    bool pressed;

    void Start()
    {
        pressed = false;
    }


    void Update()
    {
        if (pressed)
        {
            grueAnimator.SetInteger("HoistState", 1);
        }
        else
        {
            grueAnimator.SetInteger("HoistState", 2);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
        {
            pressed = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2")
        {
            pressed = false;
        }
    }
}
