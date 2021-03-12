using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{

    public Transform target;
    public Transform groundDetector;

    public GameObject journey;

    Vector3 displacements;
    Vector3 velocity;
    Vector3 gravity;
    Vector3 lastSeenPosition;
    Vector3 bushPosition;
    Vector3 checkVector;
    Vector3 normalizedVector;

    LayerMask layerMask;
    LayerMask layerMaskCollisions;

    Transform[] journeySteps;

    Transform targetJourneyPoint;

    public AnimationCurve jumpCurve;

    public float speed;
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

    public int currentJourneyPoint = 0;

    bool running;
    bool spotted;
    bool lookingStraight;
    bool jumping;

    void Start()
    {

        layerMask = ~(1 << 9);
        layerMaskCollisions = ~((1 << 9) | (1 << 30));


        gravity = Vector3.down * gravityValue;

        spotted = false;

        groundDetector = this.gameObject.transform.GetChild(1);

        lastSeenPosition = transform.position;

        if (journey != null)
        {
            journeySteps = journey.GetComponentsInChildren<Transform>();
        }
    }


    void Update()
    {

        tJump += jumpSpeed * Time.deltaTime;

        RaycastHit hit;

       

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, gravityRaycastDistance, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            transform.position = new Vector3(transform.position.x, hit.point.y + 1f, transform.position.z);
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
            transform.LookAt(new Vector3(target.position.x, transform.position.y, target.position.z) + target.transform.forward * zLook);

            RaycastHit hitCatch;

            if (Physics.Raycast(transform.position, target.transform.position - transform.position, out hitCatch, catchDistance))
            {
                if (hitCatch.collider.gameObject.tag == "Player1" || hitCatch.collider.gameObject.tag == "Player2" || hitCatch.collider.gameObject.tag == "FusedPlayer")
                {
                    UnityEditor.EditorApplication.isPlaying = false;
                }
            }
        }
        else
        {
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

            /*
            if (lookingStraight == true)
            {
                
            }
            else
            {
                //transform.eulerAngles = transform.eulerAngles + new Vector3(0, rotationSpeed * Time.deltaTime, 0);
            }
            */
        }

        /*if (distanceFromTarget <= chaseDistance)
        
            */

        /*

        if (target != null)
        {
            heightValue = target.transform.parent.transform.position.y - transform.position.y;
        }
        else
        {
            heightValue = 0;
        }


        for (int i = -5; i < 6; i++)
        {
            
            checkVector = Vector3.forward + new Vector3(0.2f * i, heightValue, 0);
            adaptedChaseDistance = chaseDistance * (1 - Mathf.Abs(i * 0.125f));
            distance = checkVector.magnitude;
            normalizedVector = checkVector / distance;

            //Debug.Log(normalizedVector);

            Debug.DrawRay(transform.position, transform.TransformDirection(normalizedVector) * adaptedChaseDistance, Color.yellow);

            RaycastHit hitTarget;

            if (Physics.Raycast(transform.position, transform.TransformDirection(normalizedVector), out hitTarget, adaptedChaseDistance))
            {
                if (hitTarget.collider.gameObject.tag == "Player1" || hitTarget.collider.gameObject.tag == "Player2" || hitTarget.collider.gameObject.tag == "FusedPlayer")
                {
                    spotted = true;
                    running = true;

                    lastSeenPosition = hitTarget.point;

                    bushPosition = Vector3.zero;
                    target = hitTarget.collider.gameObject.transform;
                }
                else if (hitTarget.collider.gameObject.tag == "Bush" && spotted == true)
                {

                    bushPosition = hitTarget.collider.gameObject.transform.position;
                }
                else
                {

                    bushPosition = Vector3.zero;
                }
            }
        
        }
            */
        /*}
        else
        {
            running = false;
        }*/
       

        

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
            if (journey != null || spotted == true)
            {
                if (hitGround.point.y > (transform.position.y - 1))
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
        /*else
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
        */
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
        // rotate towards the target
        /* transform.forward = new Vector3(
             transform.position.x, 
             Vector3.RotateTowards(transform.forward, targetJourneyPoint.position - transform.position, rotationSpeed * Time.deltaTime, 0.0f).y, 
             transform.position.z);
             */

        transform.LookAt(new Vector3(targetJourneyPoint.position.x, transform.position.y, targetJourneyPoint.transform.position.z));

        // move towards the target
        transform.position = Vector3.MoveTowards
            (transform.position, 
            new Vector3(targetJourneyPoint.position.x, transform.position.y, 
            targetJourneyPoint.position.z), 
            speed * Time.deltaTime);

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

    public void Spot()
    {
        spotted = true;
        running = true;
    }
}
