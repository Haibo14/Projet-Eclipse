using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlateMine : MonoBehaviour
{
    AudioSource source;
    public AudioClip plaqueSon;

    public bool pressed;

    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1" || other.tag == "Player2")
        {
            pressed = true;
            source.PlayOneShot(plaqueSon, 1f);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player1" || other.tag == "Player2")
        {
            pressed = false;
        }
    }
}
