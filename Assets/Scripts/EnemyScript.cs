﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    AudioSource source;

    Animator animator;
    public Transform target;
    public Transform groundDetector;

    public Transform[] targetList;

    public GameObject journey;
    public GameObject lastJourney;
    public GameObject respawnManager;

    Vector3 displacements;
    Vector3 velocity;
    Vector3 gravity;
    Vector3 lastSeenPosition;
    Vector3 bushPosition;
    Vector3 checkVector;
    Vector3 normalizedVector;

    LayerMask layerMask;
    LayerMask layerMaskCollisions;

    public Transform[] journeySteps;

    Transform targetJourneyPoint;

    public AnimationCurve jumpCurve;

    public float speed;
    public float waySpeed;
    public float gravityRaycastDistance;
    public float gravityValue;
    public float collisionsRaycast;
    public float groundDistanceDetection;
    public float chaseDistance;
    public float rotationSpeed;
    public float catchDistance;
    public float zLook;
    public float tJump;
    public float power;
    public float jumpSpeed;


    float distanceFromTarget;
    float adaptedChaseDistance;
    float checkCount;
    float distance;
    float heightValue;
    float dist;
    float nearDistance;

    public int currentJourneyPoint = 0;

    bool running;
    public bool spotted = false;
    public bool work;
    bool lookingStraight;
    bool jumping;

    void Start()
    {
        source = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();

        layerMask = ~(1 << 9);
        layerMaskCollisions = ~((1 << 30) | (1 << 9));

        respawnManager = GameObject.FindGameObjectWithTag("RespawnManager");

        gravity = Vector3.down * gravityValue;


        groundDetector = this.gameObject.transform.GetChild(1);

        lastSeenPosition = transform.position;

        if (journey != null)
        {
            journeySteps = journey.GetComponentsInChildren<Transform>();
        }

        source.Play();
        source.volume = 0;
    }


    void Update()
    {
        if (journeySteps.Length == 0)
        {
            if (journey != null)
            {
                journeySteps = journey.GetComponentsInChildren<Transform>();
            }
        }

        tJump += jumpSpeed * Time.deltaTime;

        RaycastHit hit;

       

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, gravityRaycastDistance, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            transform.position = new Vector3(transform.position.x, hit.point.y + gravityRaycastDistance, transform.position.z);
            velocity = Vector3.zero;

        }
        else
        {
            velocity += gravity * Time.deltaTime;   // allow gravity to work on our velocity vector
            transform.position += velocity * Time.deltaTime;    // move us this frame according to our speed
        }




        //distanceFromTarget = Mathf.Abs(transform.position.x - target.transform.position.x) + Mathf.Abs(transform.position.z - target.transform.position.z);

        if (spotted == true)
        {
            source.volume = 1;
            animator.SetBool("Spotted", true);

            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z) + target.transform.forward * zLook);

            RaycastHit hitCatch;

            if (Physics.Raycast(transform.position, target.transform.position - transform.position, out hitCatch, catchDistance))
            {
                if (hitCatch.collider.gameObject.tag == "Player1" || hitCatch.collider.gameObject.tag == "Player2" || hitCatch.collider.gameObject.tag == "FusedPlayer")
                {

                    respawnManager.GetComponent<Respawn>().player1Live = false;
                    respawnManager.GetComponent<Respawn>().player2Live = false;
                }
            }

            RaycastHit hitChase;

            if(Physics.Raycast(transform.position, target.transform.position - transform.position, out hitChase, chaseDistance, layerMaskCollisions))
            {
                if(hitChase.collider.tag == "Player1" || hitChase.collider.tag == "Player2" || hitChase.collider.tag == "FusedPlayer")
                {

                }
                else
                {
                    running = false;
                    spotted = false;

                    animator.SetBool("Spotted", false);
                }
            }
            else
            {
                running = false;
                spotted = false;


                animator.SetBool("Spotted", false);
            }
        }
        else
        {
            source.volume = 0;
            if (journey != null)
            {

                if (currentJourneyPoint < this.journeySteps.Length)
                {
                    if (targetJourneyPoint == null)
                        targetJourneyPoint = journeySteps[currentJourneyPoint];
                    walk();
                }
                else if (currentJourneyPoint >= this.journeySteps.Length)
                {
                    currentJourneyPoint = 0;
                }
            }

            
        }


        

        RaycastHit hitColl;

        if (Physics.Raycast(transform.position, this.transform.forward, out hitColl, collisionsRaycast, layerMaskCollisions))
        {

           displacements = Vector3.Reflect(this.transform.forward, hitColl.normal);

            spotted = false;
            running = false;
        }
        else
        {
            displacements = Vector3.forward;
        }
        

        RaycastHit hitGround;

        if(Physics.Raycast(groundDetector.position, transform.TransformDirection(Vector3.down), out hitGround, groundDistanceDetection, layerMaskCollisions))
        {
            if (journey != null || spotted == true)
            {
                if (hitGround.point.y > (transform.position.y - 0.5f))
                {
                    jumping = true;

                    if (tJump > 1.5f)
                    {
                        tJump = 0;
                    }
                }
            }
        }
        else
        {
            displacements = Vector3.zero;
        }

        if (running == true)
        {
            transform.Translate(displacements * Time.deltaTime * speed, Space.Self);
        }
        
        if (jumping == true)
        {
            transform.Translate(transform.up * jumpCurve.Evaluate(tJump) * power * Time.deltaTime, Space.World);

            if (tJump >= 1)
            {
                jumping = false;
            }
        }

    }

    void walk()
    {

        transform.LookAt(new Vector3(targetJourneyPoint.position.x, transform.position.y, targetJourneyPoint.transform.position.z));

        // move towards the target
        transform.position = Vector3.MoveTowards
            (transform.position, 
            new Vector3(targetJourneyPoint.position.x, transform.position.y, 
            targetJourneyPoint.position.z), 
            waySpeed * Time.deltaTime);

        if (transform.position.x == targetJourneyPoint.position.x && transform.position.z == targetJourneyPoint.position.z)
        {
            if (currentJourneyPoint >= this.journeySteps.Length)
            {
                currentJourneyPoint = 0;
            }
            else
            {

                currentJourneyPoint++;

                if (currentJourneyPoint >= this.journeySteps.Length)
                {
                    currentJourneyPoint = 0;
                }

                targetJourneyPoint = journeySteps[currentJourneyPoint];
            }

        }
    }

    public void Spot(int p_id, Transform p_transform)
    {
        targetList[p_id] = p_transform;
        nearDistance = 1000000;
        SelectTransform();
    }

    public void Unspot(int p_id)
    {
        targetList[p_id] = null;
        nearDistance = 1000000;
        SelectTransform();
    }

    private void SelectTransform()
    {
        foreach(Transform targetTransform in targetList)
        {

            if (targetTransform != null)
            {
                dist = Vector3.Distance(transform.position, targetTransform.transform.position);

                if (dist < 1000000)
                {
                    if (dist <= nearDistance)
                    {
                        nearDistance = dist;
                        target = targetTransform;
                        spotted = true;
                        running = true;
                    }
                }
            }
        }
    }
}
