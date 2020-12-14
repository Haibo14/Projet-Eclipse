using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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