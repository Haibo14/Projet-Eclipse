using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{
    GameObject FpPlayer;

    LayerMask layerMask;
    LayerMask layerMaskPlayer;


    public AnimationCurve jumpCurve;
    public AnimationCurve splitCurve;

    public Vector3 move;

    Vector3 velocity;
    Vector3 gravity;
    Vector3 jumpMove;
    Vector3 splitDirection;
    Vector3 incomingVec;

    public float gravityValue;
    public float gravityRaycastDistancePlayers;
    public float groundHeight;
    public float raycastDistanceDetection;
    public float jumpSpeed;
    public float timeSpeed;
    public float splitSpeed;
    public float speed;
    public float power;
    public float splitPower;

    float angle;
    float angleRad;
    float t1;
    float t;
    float adaptDistance;
    float tSplit;


    bool fusing;
    bool merged;
    bool splitting;
    bool hooking;
    bool jumping;

    void Start()
    {
        merged = false;
        splitting = false;
        jumping = false;

        gravity = Vector3.down * gravityValue;

        layerMaskPlayer = 31;
        
        layerMask = ~(1 << layerMaskPlayer);
    }


    void Update()
    {


        move.x = Input.GetAxis("p1_Horizontal");
        move.z = Input.GetAxis("p1_Vertical");

        if (move != Vector3.zero)
        {
            angleRad = Mathf.Atan2(move.x, move.z);
            angle = angleRad * Mathf.Rad2Deg;

            if (splitting == false)
            {
                transform.GetChild(0).transform.eulerAngles = new Vector3(0, angle, 0);
            }
        }

        if (Input.GetButton("p1_Hook"))
        {
            hooking = true;
        }
        else
        {
            hooking = false;
        }

        #region physics

        #region gravity

        RaycastHit hit;



        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, gravityRaycastDistancePlayers, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);
            transform.position = new Vector3(transform.position.x, hit.point.y + 1, transform.position.z);
            velocity = Vector3.zero;

            groundHeight = hit.point.y;
        }
        else
        {
            if (jumping == false && merged == false)
            {
                velocity += gravity * Time.deltaTime;   // allow gravity to work on our velocity vector
                transform.position += velocity * Time.deltaTime;    // move us this frame according to our speed
            }
            else
            {
                velocity = Vector3.zero;
            }
        }

        #endregion

        #region collisionDetection


        if (merged == false && splitting == false)
        {

            RaycastHit hitColl;


            Debug.DrawRay(transform.position, transform.forward * 1000, Color.green);


            if (Physics.Raycast(transform.position, transform.forward, out hitColl, raycastDistanceDetection, layerMask))
            {
                Debug.Log("Joystick : " + move);
                transform.position = hitColl.point - transform.forward;

                if (move != Vector3.zero)
                {
                    move = Vector3.Reflect(transform.forward, hitColl.normal);
                }


                Debug.Log("HitNormal : " + hitColl.normal);

            }


            #endregion
            #endregion

            if (merged == true)
            {
                transform.position = FpPlayer.transform.position;
            }

        }

        #region time

        t += jumpSpeed * Time.deltaTime;

        tSplit += splitSpeed * Time.deltaTime;


        if (hooking == false)
        {

            t1 += timeSpeed * adaptDistance * Time.deltaTime;
        }

        #endregion


        #region movements
        if (fusing == false && merged == false && splitting == false)
        {
            if (move != Vector3.zero)
            {
                transform.Translate(transform.forward * move.z * speed * Time.deltaTime, Space.World);
                transform.Translate(transform.right * move.x * speed * Time.deltaTime, Space.World);
            }
        }

        #endregion

        #region collisionDetection

        if (merged == false && splitting == false)
        {
            RaycastHit hitColl;


            Debug.DrawRay(transform.position, transform.forward * 1000, Color.green);


            if (Physics.Raycast(transform.position, transform.forward, out hitColl, raycastDistanceDetection, layerMask))
            {
                transform.position = hitColl.point - transform.forward;

                if (move != Vector3.zero)
                {
                    move = Vector3.Reflect(transform.forward, hitColl.normal);
                }
            }
        }

        #endregion

        #region jump

        if (Input.GetButton("p1_Jump"))
        {
            if (fusing == false)
            {
                if (merged == false)
                {
                    if (canJump())
                    {
                        t = 0;
                        jumping = true;

                        jumpMove = move;
                    }
                }
                else
                {
                    /*
                    if (fpCanJump())
                    {
                        tFP = 0;
                        t = 0;
                        fpJumping = true;
                    }*/
                }
            }
        }

        if (jumping == true)
        {
            transform.Translate(transform.up * jumpCurve.Evaluate(t) * power, Space.World);

            if (t >= 1)
            {
                jumping = false;
            }

        }

        #endregion

        #region split


        if (splitting == true)
        {

            RaycastHit hitSplit;

            Debug.DrawRay(transform.position, transform.forward * 100, Color.red);

            if (Physics.Raycast(transform.position, transform.forward, out hitSplit, raycastDistanceDetection, layerMask))
            {
                Debug.Log("Hit");

                incomingVec = hitSplit.point - transform.position;
                splitDirection = Vector3.Reflect(transform.forward, hitSplit.normal);

            }

            

            transform.Translate(splitDirection * splitCurve.Evaluate(tSplit) * splitPower, Space.World);

            if (tSplit >= 1)
            {
                splitting = false;
            }
        }

        #endregion

    }

    bool canJump()
    {
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), gravityRaycastDistancePlayers, layerMask);
    }
}
