using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2" || other.gameObject.tag == "FusedPlayer")
        {
            //UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
