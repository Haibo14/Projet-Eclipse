using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarricadeSound : MonoBehaviour
{
    AudioSource source;
    public AudioClip barricadeSon;

    public bool playOnce = true;

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (transform.childCount == 0)
        {
            if (source.isPlaying == false && playOnce == true)
            {
                source.PlayOneShot(barricadeSon, 1f);
                playOnce = false;
            }
        }
    }
}
