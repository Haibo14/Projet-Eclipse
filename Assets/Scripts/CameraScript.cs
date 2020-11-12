using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject fusedPlayer;

    void Start()
    {
        
    }

    void Update()
    {
        this.transform.position =  new Vector3(0, 30, fusedPlayer.transform.position.z - 15);
    }
}
