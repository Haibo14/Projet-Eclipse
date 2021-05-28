using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public GameObject platformTraject;

    Transform up;
    Transform down;

    public float waySpeed;

    public bool upDown;

    void Start()
    {
        up = platformTraject.transform.GetChild(0);
        down = platformTraject.transform.GetChild(1);
    }

    // Update is called once per frame
    void Update()
    {
        if (upDown == true)
        {

            if (transform.position != up.transform.position)
            {
                transform.position = Vector3.MoveTowards
                (transform.position,
                up.transform.position,
                waySpeed * Time.deltaTime);
            }

        }
        else
        {
            
            if (transform.position != down.transform.position)
            {
                transform.position = Vector3.MoveTowards
                (transform.position,
                down.transform.position,
                waySpeed * Time.deltaTime);
            }
        }
    }
}
