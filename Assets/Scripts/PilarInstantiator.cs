using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PilarInstantiator : MonoBehaviour
{
    public AudioSource source;
    public AudioClip pilierSon;
    public GameObject pilarPrefab;
    GameObject lastFramePilar;

    bool playOnce;
    void Start()
    {
        playOnce = true;
        source = GetComponent<AudioSource>();

        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
        {
            Reset();
        }
    }


    void Update()
    {
        if (transform.childCount == 0)
        {
            if (source.isPlaying == false && playOnce == true)
            {
                source.PlayOneShot(pilierSon, 1f);
                playOnce = false;
            }
        }

        source.volume = 1;
    }

    public void Reset()
    {
        Debug.Log("Proc");
        if (gameObject.transform.childCount == 0)
        {
            GameObject pilar = Instantiate(pilarPrefab, transform.position, Quaternion.identity);
            pilar.transform.Rotate(-90f, 0, 0);
            pilar.transform.parent = gameObject.transform;

            playOnce = true;
        }

    }
}
