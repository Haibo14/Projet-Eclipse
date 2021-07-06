using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReceivePosition : MonoBehaviour {
    public OSC osc;

    int switchState_;
    public int xAxis_p1;
    public int zAxis_p1;
    public int xAxis_p2;
    public int zAxis_p2;
    int buttonJump_p1;
    int buttonJump_p2;
    int buttonInteract_p1;
    int buttonInteract_p2;

    public bool switchState;
    public bool buttonJumpP1;
    public bool buttonJumpP2;
    public bool buttonInteractP1;
    public bool buttonInteractP2;

    void Start(){
        osc.SetAddressHandler("/manette1", ReadOSC);
        osc.SetAddressHandler("/manette2", ReadOSC2);

        switchState = false;
    }

    void ReadOSC(OscMessage message)
    {
        switchState_ = message.GetInt(0);
        xAxis_p2 = message.GetInt(1);
        zAxis_p2 = message.GetInt(2);
        buttonInteract_p2 = message.GetInt(3);
        buttonJump_p2 = message.GetInt(4);

    }
    void ReadOSC2(OscMessage message)
    {
        xAxis_p1 = message.GetInt(0);
        zAxis_p1 = message.GetInt(1);
        buttonInteract_p1 = message.GetInt(2);
        buttonJump_p1 = message.GetInt(3);
    }
    void Update()
    {
        if (switchState_ == 1)
        {
            switchState = true;
        }
        else
        {
            switchState = false;
        }

        if (buttonJump_p1 == 0)
        {
            buttonJumpP1 = true;
        }
        else
        {
            buttonJumpP1 = false;
        }

        if (buttonInteract_p1 == 0)
        {
            buttonInteractP1 = true;
        }
        else
        {
            buttonInteractP1 = false;
        }
        if (buttonJump_p2 == 0)
        {
            buttonJumpP2 = true;
        }
        else
        {
            buttonJumpP2 = false;
        }

        if (buttonInteract_p2 == 0)
        {
            buttonInteractP2 = true;
        }
        else
        {
            buttonInteractP2 = false;
        }

        Debug.Log("XP1 : " + xAxis_p1 + "   & ZP1 : " + zAxis_p1 + "        switchState is " + switchState + "      Bouton Jump : " + buttonJumpP1 + "        Bouton Interagir : " + buttonInteractP1);
        Debug.Log("XP1 : " + xAxis_p2 + "   & ZP1 : " + zAxis_p2 + "        switchState is " + switchState + "      Bouton Jump : " + buttonJumpP2 + "        Bouton Interagir : " + buttonInteractP2);
    }
}
