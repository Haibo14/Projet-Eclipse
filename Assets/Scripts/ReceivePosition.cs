using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
public class ReceivePosition : MonoBehaviour {
    public OSC osc;

    int switchState_;

    public bool switchState;

    void Start(){
        osc.SetAddressHandler("/test", ReedSwitch);

        switchState = false;
    }

    void ReedSwitch(OscMessage message)
    {
        switchState_ = message.GetInt(0);
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
    }
}
*/
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
}