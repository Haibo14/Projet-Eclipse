using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever : MonoBehaviour
{
    public GameObject oscObject;
    public ReceivePosition osc;
    public GameObject platform;

    //public Transform leversManager;

    public string hookingStringP1;
    public string hookingStringP2;

    private const float _minimumHeldDuration = 5;
    private float _hookPressedTimeP1 = 0;
    private float _hookPressedTimeP2 = 0;
    private bool _hookHeldP1 = false;
    private bool _hookHeldP2 = false;

    bool interactP1;
    bool interactP2;

    bool playOnce;

    void Start()
    {
        playOnce = true;
        osc = oscObject.GetComponent<ReceivePosition>();
        interactP1 = false;
        interactP2 = false;
    }

    void Update()
    {
        if (osc.enabled == true)
        {
            interactP1 = _hookHeldP1;
            interactP2 = _hookHeldP2;

            
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "FusedPlayer")
        {
            if(Input.GetButton(hookingStringP1) && Input.GetButton(hookingStringP2) && platform.GetComponent<Platform>().arrived == true)
            {
                if(playOnce == true)
                {

                    platform.GetComponent<Platform>().upDown = !platform.GetComponent<Platform>().upDown;

                    playOnce = false;
                }

            }
            else
            {
                playOnce = true;
            }

            if (_hookHeldP1 == true && _hookHeldP2 == true && platform.GetComponent<Platform>().arrived == true)
            {
                if (playOnce == true)
                {

                    platform.GetComponent<Platform>().upDown = !platform.GetComponent<Platform>().upDown;

                    playOnce = false;
                }
                

            }
            else
            {
                playOnce = true;
            }
        }
    }
}
