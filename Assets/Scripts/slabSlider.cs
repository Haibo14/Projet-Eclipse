using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slabSlider : MonoBehaviour
{
    public float delta = 10f;  // Amount to move left and right from the start point
    public float speed = 2.0f;
    private Vector3 startPos;

    public bool blackSlab;
    public bool whiteSlab;

    void Start()
    {
        startPos = transform.position;
    }

    
    void Update()
    {
        if (blackSlab == true && whiteSlab == true)
        {
            Vector3 v = startPos;
            v.z += delta * Mathf.Sin(Time.time * speed);
            transform.position = v;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "FusedPlayer")
        {
            
        }
    }
}
