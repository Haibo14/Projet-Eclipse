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
    Vector3 lastSeenPosition;
    Vector3 bushPosition;

    LayerMask layerMask;
    LayerMask layerMaskCollisions;

    public float speed;
    public float gravityRaycastDistance;
    public float gravityValue;
    public float collisionsRaycast;
    public float groundDistanceDetection;
    public float chaseDistance;
    public float rotationSpeed;
    public float catchDistance;

    float distanceFromTarget;

    bool running;
    bool spotted;
    bool lookingStraight;

    void Start()
    {

        layerMask = ~(1 << 9);
        layerMaskCollisions = ~((1 << 9) | (1 << 30));


        gravity = Vector3.down * gravityValue;

        spotted = false;

        lastSeenPosition = transform.position;
    }


    void Update()
    {
        RaycastHit hit;

        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * chaseDistance, Color.yellow);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward + new Vector3(0.25f, 0, 0)) * (chaseDistance * 0.8f), Color.yellow);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward + new Vector3(-0.25f, 0, 0)) * (chaseDistance * 0.8f), Color.yellow);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward + new Vector3(0.5f, 0, 0)) * (chaseDistance * 0.5f), Color.yellow);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward + new Vector3(-0.5f, 0, 0)) * (chaseDistance * 0.5f), Color.yellow);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward + new Vector3(1,0,0)) * (chaseDistance * 0.2f), Color.yellow);
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward + new Vector3(-1,0,0)) * (chaseDistance * 0.2f), Color.yellow);

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


        Debug.Log(spotted);

        distanceFromTarget = Mathf.Abs(transform.position.x - target.transform.position.x) + Mathf.Abs(transform.position.z - target.transform.position.z);

        if (spotted == true)
        {
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z));

            RaycastHit hitCatch;

            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hitCatch, catchDistance))
            {
                if (hitCatch.collider.gameObject.tag == "Player1" || hitCatch.collider.gameObject.tag == "Player2" || hitCatch.collider.gameObject.tag == "FusedPlayer")
                {
                    UnityEditor.EditorApplication.isPlaying = false;
                }
            }
        }
        else
        {
            if (lookingStraight == true)
            {
                
            }
            else
            {
                //transform.eulerAngles = transform.eulerAngles + new Vector3(0, rotationSpeed * Time.deltaTime, 0);
            }

        }

        if (distanceFromTarget <= chaseDistance)
        {
            

            RaycastHit hitTarget;

            if (Physics.Raycast(transform.position, transform.forward, out hitTarget, chaseDistance) 
                || Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward + new Vector3(0.25f, 0, 0)), out hitTarget, (chaseDistance * 0.8f)) 
                || Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward + new Vector3(-0.25f, 0, 0)), out hitTarget, (chaseDistance * 0.8f)) 
                || Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward + new Vector3(0.5f, 0, 0)), out hitTarget, (chaseDistance * 0.5f)) 
                || Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward + new Vector3(-0.5f, 0, 0)), out hitTarget, (chaseDistance * 0.5f))
                || Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward + new Vector3(1f, 0, 0)), out hitTarget, (chaseDistance * 0.2f))
                || Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward + new Vector3(-1f, 0, 0)), out hitTarget, (chaseDistance * 0.2f))
                )
            {
                if (hitTarget.collider.gameObject.tag == "Player1" || hitTarget.collider.gameObject.tag == "Player2" || hitTarget.collider.gameObject.tag == "FusedPlayer")
                {
                    spotted = true;
                    running = true;

                    lastSeenPosition = hitTarget.point;

                    bushPosition = Vector3.zero;
                }
                else if (hitTarget.collider.gameObject.tag == "Bush" && spotted == true)
                {
                    running = false;

                    bushPosition = hitTarget.collider.gameObject.transform.position;
                }
                else
                {
                    running = false;

                    bushPosition = Vector3.zero;
                }
            }
            
        }
        else
        {
            running = false;
        }
       

        

        RaycastHit hitColl;

        if (Physics.Raycast(transform.position, this.transform.forward, out hitColl, collisionsRaycast, layerMaskCollisions))
        {
           displacements = Vector3.Reflect(this.transform.forward, hitColl.normal);


        }
        else
        {
            displacements = Vector3.forward;
        }
        

        RaycastHit hitGround;

        if(Physics.Raycast(groundDetector.position, transform.TransformDirection(Vector3.down), out hitGround, groundDistanceDetection, layerMaskCollisions))
        {

        }
        else
        {
            displacements = Vector3.zero;
        }

        if (running == true)
        {
            transform.Translate(displacements * Time.deltaTime * speed, Space.Self);
        }
        else
        {
            if (bushPosition == Vector3.zero)
            {
                if (transform.position != lastSeenPosition)
                {
                    transform.position = Vector3.MoveTowards(transform.position, lastSeenPosition, Time.deltaTime * speed);

                    spotted = false;
                }
            }
            else
            {
                if (transform.position != bushPosition && spotted == true)
                {
                    transform.position = Vector3.MoveTowards(transform.position, bushPosition, Time.deltaTime * speed);
                    lookingStraight = true;

                }
                else if (transform.position == bushPosition)
                {

                    lookingStraight = false;
                    spotted = false;
                }
            }
        }


    }
}
