using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public Transform target;
    public Transform groundDetector;

    Vector3 displacements;
    Vector3 velocity;
    Vector3 gravity;

    LayerMask layerMask;

    public float speed;
    public float gravityRaycastDistance;
    public float gravityValue;
    public float collisionsRaycast;
    public float groundDistanceDetection;

    void Start()
    {

        layerMask = ~(1 << 9);


        gravity = Vector3.down * gravityValue;
    }


    void FixedUpdate()
    {
        transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));
       

        RaycastHit hit;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, gravityRaycastDistance, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            transform.position = new Vector3(transform.position.x, hit.point.y + 1, transform.position.z);
            velocity = Vector3.zero;
        }
        else
        {
            velocity += gravity * Time.deltaTime;   // allow gravity to work on our velocity vector
            transform.position += velocity * Time.deltaTime;    // move us this frame according to our speed
        }

        RaycastHit hitColl;

        if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(1,0,1)), out hitColl, collisionsRaycast, layerMask))
        {
            transform.right = -hitColl.normal;
            //displacements = -Vector3.right;

            if (Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(-1, 0, 1)), out hitColl, collisionsRaycast, layerMask))
            {

                transform.right = hitColl.normal;
                //displacements = Vector3.right;
            }
        }
        else if(Physics.Raycast(transform.position, transform.TransformDirection(new Vector3(-1, 0, 1)), out hitColl, collisionsRaycast, layerMask))
        {

            transform.right = hitColl.normal;
            //displacements = Vector3.right;
        }
        else
        {
            displacements = Vector3.forward;
        }

        RaycastHit hitGround;

        if(Physics.Raycast(groundDetector.position, transform.TransformDirection(Vector3.down), out hitGround, groundDistanceDetection, layerMask))
        {

            displacements = Vector3.forward;
        }
        else
        {

            displacements = Vector3.zero;
        }
        transform.Translate(displacements * Time.deltaTime * speed, Space.Self);


    }
}
