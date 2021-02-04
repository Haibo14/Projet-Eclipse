using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject fpPlayer;    
    public GameObject otherPlayer;
    public GameObject OscMaster;

    GameObject childPlayer;

    public Players players;
    public PlayerScript playerObject;
    public ReceivePosition oscMessage;

    LayerMask layerMask;
    LayerMask layerMaskPlayer;
    LayerMask layerMaskBush;
    LayerMask layerMaskFuse;
    LayerMask layerMaskDObject;

    public string moveX;
    public string moveZ;
    public string hookingString;
    public string jumpString;

    public AnimationCurve jumpCurve;
    public AnimationCurve splitCurve;

    public Vector3 move;

    Vector3 velocity;
    Vector3 gravity;
    Vector3 jumpMove;
    Vector3 splitDirection;
    Vector3 incomingVec;
    Vector3 memoryPosition;

    public float gravityValue;
    public float gravityRaycastDistancePlayers;
    public float gravityRaycastDistanceFP;
    public float groundHeight;
    public float raycastDistanceDetection;
    public float jumpSpeed;
    public float timeSpeed;
    public float splitSpeed;
    public float speed;
    public float power;
    public float splitPower;
    public float raycastDistanceFuse;
    public float tFuse;
    public float tJump;
    public float adaptDistance;

    float angle;
    float angleRad;
    float tSplit;


    public bool fusing;
    public bool merged;
    public bool splitting;
    bool hooking;
    bool jumping;

    void Start()
    {
        fusing = false;
        merged = false;
        splitting = false;
        jumping = false;

        players = fpPlayer.GetComponent<Players>();
        playerObject = otherPlayer.GetComponent<PlayerScript>();
        oscMessage = OscMaster.GetComponent<ReceivePosition>();

        childPlayer = transform.GetChild(0).gameObject;

        gravity = Vector3.down * gravityValue;

        layerMaskPlayer = 31;
        layerMaskBush = 30;
        layerMaskDObject = 8;

        layerMask = ~((1 << layerMaskPlayer) | (1 << layerMaskBush));
        layerMaskFuse = ~((1 << layerMaskPlayer) | (1 << layerMaskBush) | (1 << layerMaskDObject));
    }


    void Update()
    {


        move.x = Input.GetAxis(moveX);
        move.z = Input.GetAxis(moveZ);

        //move.x = -(oscMessage.xAxis_ / 6);
        //move.z = oscMessage.zAxis_ / 6;

        if (move != Vector3.zero)
        {
            angleRad = Mathf.Atan2(move.x, move.z);
            angle = angleRad * Mathf.Rad2Deg;

            if (splitting == false)
            {
                childPlayer.transform.eulerAngles = new Vector3(0, angle, 0);
            }
        }

        if (Input.GetButton(hookingString))
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


            Debug.DrawRay(transform.position, childPlayer.transform.forward * 1000, Color.green);


            if (Physics.Raycast(transform.position, childPlayer.transform.forward, out hitColl, raycastDistanceDetection, layerMask))
            {
                //Debug.Log("Joystick : " + move);
                transform.position = hitColl.point - childPlayer.transform.forward;

                if (move != Vector3.zero)
                {
                    move = Vector3.Reflect(childPlayer.transform.forward, hitColl.normal);
                }


                //Debug.Log("HitNormal : " + hitColl.normal);

            }

        }

        #endregion
        #endregion

        if (merged == true)
        {
            transform.position = fpPlayer.transform.position;
        }

        #region time

        tJump += jumpSpeed * Time.deltaTime;

        tSplit += splitSpeed * Time.deltaTime;


        if (hooking == false)
        {
            tFuse += timeSpeed * adaptDistance * Time.deltaTime;

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


        if (fusing == true)
        {
            #region fusionDisplacements

            RaycastHit hitFuse;


            Debug.DrawRay(transform.position, transform.TransformDirection(otherPlayer.transform.position - transform.position) * 1000, Color.green);

            if (Physics.Raycast(transform.position, transform.TransformDirection(otherPlayer.transform.position - transform.position), out hitFuse, raycastDistanceFuse, layerMaskFuse))
            {
                fusing = false;
                players.fusing = false;
                playerObject.fusing = false;

            }
            else
            {
                transform.position = new Vector3
                   (Mathf.Lerp(memoryPosition.x, otherPlayer.transform.position.x, tFuse),
                   Mathf.Lerp(memoryPosition.y, otherPlayer.transform.position.y, tFuse),
                   Mathf.Lerp(memoryPosition.z, otherPlayer.transform.position.z, tFuse));
            }

            #endregion

            #region merge

            if (merged == true)
            {
                childPlayer.gameObject.SetActive(false);
                transform.position = fpPlayer.transform.position;

            }

            #endregion
        }
            
        #region jump

            
        if (Input.GetButton(jumpString))
        {
            if (fusing == false)
            {
                if (merged == false)
                {
                    if (canJump())
                    {
                        tJump = 0;

                        jumping = true;

                        jumpMove = move;
                    }
                }
                else
                {
                    if (canJumpMerged())
                    {
                        players.tFP = 0;
                        players.fpJumping = true;
                    }
                }

            }
        }

        

        if (jumping == true)
        {
            transform.Translate(transform.up * jumpCurve.Evaluate(tJump) * power, Space.World);
            
            if (tJump >= 1)
            {
                jumping = false;
            }
        }

        #endregion
        
        #region split
        
        

        if (splitting == true)
        {

            RaycastHit hitSplit;

            Debug.DrawRay(transform.position, childPlayer.transform.forward * 100, Color.red);
            
            if (Physics.Raycast(transform.position, childPlayer.transform.forward, out hitSplit, raycastDistanceDetection, layerMask))
            {
                incomingVec = hitSplit.point - transform.position;
                splitDirection = Vector3.Reflect(childPlayer.transform.forward, hitSplit.normal);

            }
            
            transform.Translate(splitDirection * splitCurve.Evaluate(tSplit) * splitPower, Space.World);

            if (tSplit >= 1)
            {
                splitting = false;
            }
        }
        
        #endregion

    }

    public void Fuse()
    {
        memoryPosition = transform.position;
    }

    public void Split()
    {
        if (merged == true && splitting == false)
        {
            merged = false;

            childPlayer.gameObject.SetActive(true);

            if (move != Vector3.zero)
            {
                splitDirection = childPlayer.transform.forward;
            }
            else
            {
                splitDirection = Vector3.zero;
            }

            tSplit = 0;

            splitting = true;
        }
    }

    bool canJump()
    {
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), gravityRaycastDistancePlayers, layerMask);
    }

    bool canJumpMerged()
    {
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), gravityRaycastDistanceFP, layerMask);
    }
}
