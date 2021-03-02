using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeCamera : MonoBehaviour
{
    public GameObject fusedPlayer;

    Vector3 target;


    public float z;
    public float y;
    public float cameraSmoothSpeed;

    void Start()
    {

    }

    void Update()
    {
        target = new Vector3(fusedPlayer.transform.position.x, fusedPlayer.transform.position.y + y, fusedPlayer.transform.position.z - z);

        this.transform.position = Vector3.Lerp(transform.position, target, cameraSmoothSpeed);
    }
}
