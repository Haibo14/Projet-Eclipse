using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlabDescent : MonoBehaviour
{
    float t;
    public float speed;

    public bool blackSlab;
    public bool whiteSlab;


    void Start()
    {
        t = 0;
        blackSlab = false;
        whiteSlab = false;
    }

    void Update()
    {
        if (blackSlab == true && whiteSlab == true)
        {

            t += Time.deltaTime * speed;

            this.transform.position = new Vector3(this.transform.position.x, Mathf.Lerp(-30, -75, t), this.transform.position.z);
        }

    }
}
