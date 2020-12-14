using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{

    public GameObject fusedPlayer;

    Vector3 target;

    public Vector3 moveP1;
    public Vector3 moveP2;
    public Vector3 move;

    public float z;
    float yP1;
    float yP2;
    float yGround;
    public float y;
    public float cameraSmoothSpeed;

    void Start()
    {
        
    }

    void Update()
    {

        moveP1.x = Input.GetAxis("p1_Horizontal");
        moveP1.z = Input.GetAxis("p1_Vertical");

        moveP2.x = Input.GetAxis("p2_Horizontal");
        moveP2.z = Input.GetAxis("p2_Vertical");

        move = moveP1 + moveP2;

        yP1 = fusedPlayer.GetComponent<Players>().groundHeightP1;
        yP2 = fusedPlayer.GetComponent<Players>().groundHeightP2;

        yGround = (yP1 + yP2) / 2;

        target = new Vector3(fusedPlayer.transform.position.x, yGround + y, fusedPlayer.transform.position.z - z);

        this.transform.position = Vector3.Lerp(transform.position, target, cameraSmoothSpeed);
    }
}
