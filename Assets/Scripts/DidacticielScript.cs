using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DidacticielScript : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject fusedPlayer;

    int didacticielStep;

    bool startDidacticiel;

    void Start()
    {
        
    }

    void Update()
    {
        if (startDidacticiel == true)
        {
            if (didacticielStep == 0)
            {
                if(fusedPlayer.GetComponent<Players>().merged == true)
                {
                    didacticielStep++;
                }
            }



        }
    }
}
