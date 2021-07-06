using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkipCinMenu : MonoBehaviour
{

    public GameObject video;
    public Canvas menu;
    public AudioSource source;
    // Start is called before the first frame update
    void Start()
    {

        source.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            video.gameObject.SetActive(false);
            menu.enabled = true;
            source.Play();
        }
    }
}
