using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    //AudioSource source;
    //public AudioClip montecharge;
    public GameObject platformTraject;

    Transform up;
    Transform down;

    public float waySpeed;

    public bool upDown;
    public bool arrived;

    void Start()
    {
        //source = GetComponent<AudioSource>();
        up = platformTraject.transform.GetChild(0);
        down = platformTraject.transform.GetChild(1);

        //source.clip = montecharge;
        //source.Play();
        //source.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (upDown == true)
        {

            if (transform.position != up.transform.position)
            {
                //source.volume = 1;

                transform.position = Vector3.MoveTowards
                (transform.position,
                up.transform.position,
                waySpeed * Time.deltaTime);

                arrived = false;
            }
            else
            {
                //source.volume = 0;
                arrived = true;
            }

        }
        else
        {


            if (transform.position != down.transform.position)
            {
                //source.volume = 1;

                transform.position = Vector3.MoveTowards
                (transform.position,
                down.transform.position,
                waySpeed * Time.deltaTime);


                arrived = false;
            }
            else
            {
                arrived = true;
                //source.volume = 0;
            }
        }
    }
}
