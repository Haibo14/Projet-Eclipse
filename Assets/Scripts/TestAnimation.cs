using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestAnimation : MonoBehaviour
{
    public Animator animator;

    public PlayerScript script;


    public float velocityAnimation;

    void Start()
    {
        animator = transform.GetChild(0).GetComponent<Animator>();

    }


    void Update()
    {

        velocityAnimation = script.velocityAnimation;

        animator.SetFloat("Velocity", velocityAnimation);
    }
}
