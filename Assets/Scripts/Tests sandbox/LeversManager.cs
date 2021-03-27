using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeversManager : MonoBehaviour
{
    [SerializeField] Animator leftDoorFactory;
    [SerializeField] Animator rightDoorFactory;

    public int leverState;

    // Start is called before the first frame update
    void Start()
    {
        leverState = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (leverState == 10)
        {

            leftDoorFactory.SetBool("LeverState", true);
            rightDoorFactory.SetBool("LeverState", true);
        }
    }
}
