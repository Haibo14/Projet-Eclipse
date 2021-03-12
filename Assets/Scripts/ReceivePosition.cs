using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ReceivePosition : MonoBehaviour {
    public OSC osc;

    int switchState_;
    public int xAxis_;
    public int zAxis_;

    public bool switchState;

    void Start(){
        osc.SetAddressHandler("/manette1", ReadOSC);
        osc.SetAddressHandler("/manette2", ReadOSC2);

        switchState = false;
    }

    void ReadOSC(OscMessage message)
    {
        switchState_ = message.GetInt(0);
        xAxis_ = message.GetInt(1);
        zAxis_ = message.GetInt(2);
    }
    void ReadOSC2(OscMessage message)
    {
        string hello = message.ToString();
        print(hello);
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

        Debug.Log("X : " + xAxis_ + "   & Z : " + zAxis_ + "        switchState is " + switchState);
    }
}
/*
public class ReceivePosition : MonoBehaviour
{
    public OSC osc; 

    public int xAxis_;
    public int zAxis_;


    void Start()
    {
        osc.SetAddressHandler("/test", JoystickAxis);

    }

    void JoystickAxis(OscMessage message)
    {
        xAxis_ = message.GetInt(0);
        zAxis_ = message.GetInt(1);
    }
    void Update()
    {
        Debug.Log("X : " + xAxis_ + "   & Z : " + zAxis_);
    }
}*/