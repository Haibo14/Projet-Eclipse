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
        {/*
            if (Input.GetButtonDown(hookingStringP1))
            {
                _hookPressedTimeP1 = Time.timeSinceLevelLoad;
                _hookHeldP1 = false;
            }
            else if (Input.GetButtonUp(hookingStringP1) || interactP1)
            {
                if (!_hookHeldP1)
                {

                }
                _hookHeldP1 = false;
            }

            if (Input.GetButton(hookingStringP1) || interactP1)
            {
                if (Time.timeSinceLevelLoad - _hookPressedTimeP1 > (_minimumHeldDuration * Time.deltaTime))
                {
                    _hookHeldP1 = true;

                }
            }

            if (Input.GetButtonDown(hookingStringP2) || interactP2)
            {
                _hookPressedTimeP2 = Time.timeSinceLevelLoad;
                _hookHeldP2 = false;
            }
            else if (Input.GetButtonUp(hookingStringP2) || interactP2)
            {
                if (!_hookHeldP2)
                {

                }
                _hookHeldP2 = false;
            }

            if (Input.GetButton(hookingStringP2) || interactP2)
            {
                if (Time.timeSinceLevelLoad - _hookPressedTimeP2 > _minimumHeldDuration)
                {
                    _hookHeldP2 = true;

                }
            }

            if (_hookHeldP1 == true && _hookHeldP2 == true)
            {
                if (playOnce == true)
                {
                    //leversManager.GetComponent<LeversManager>().leverState++;
                    playOnce = false;
                }

                platform.GetComponent<Platform>().upDown = !platform.GetComponent<Platform>().upDown;

                _hookPressedTimeP1 = Time.timeSinceLevelLoad;
                _hookPressedTimeP2 = Time.timeSinceLevelLoad;

                _hookHeldP1 = false;
                _hookHeldP2 = false;
            }*/
            if(Input.GetButtonDown(hookingStringP1) && Input.GetButtonDown(hookingStringP2))
            {
                if(playOnce == true)
                {

                    platform.GetComponent<Platform>().upDown = !platform.GetComponent<Platform>().upDown;

                    playOnce = false;
                }
                else
                {
                    playOnce = true;
                }
            }

            if (_hookHeldP1 == true && _hookHeldP2 == true)
            {
                if (playOnce == true)
                {

                    platform.GetComponent<Platform>().upDown = !platform.GetComponent<Platform>().upDown;

                    playOnce = false;
                }
                else
                {
                    playOnce = true;
                }

            }
        }
    }
}
