using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilarManager : MonoBehaviour
{
    public Animator animator;

    GameObject[] pilarList;
    void Start()
    {
        
    }


    void Update()
    {
        pilarList = GameObject.FindGameObjectsWithTag("Pilar");

        if(pilarList.Length == 1)
        {
            animator.SetBool("death", true);
        }
    }
}
