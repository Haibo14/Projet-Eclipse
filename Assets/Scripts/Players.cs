using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    #region variables

    public GameObject player1;
    public GameObject player2;

    public ReceivePosition ReceivePosition;

    LineRenderer lineRenderer;

    public AnimationCurve jumpCurve;
    public AnimationCurve splitCurve;


    Vector3 player1MemoryPosition;
    Vector3 player2MemoryPosition;
    Vector3 velocityFP;
    Vector3 velocityP1;
    Vector3 velocityP2;
    Vector3 gravity;
    Vector3 splitDirectionP1;
    Vector3 splitDirectionP2;
    Vector3 incomingVec;


    public Vector3 moveP1;
    public Vector3 moveP2;
    public Vector3 move;

    Vector2 jumpMoveP1;
    Vector2 jumpMoveP2;
    Vector2 splitMoveP1;
    Vector2 splitMoveP2;

    public float impulse;
    public float speed;
    public float jumpSpeed;
    public float timeSpeed;
    public float power;
    public float checkDistance;
    public float splitPower;
    public float splitPowerY;
    public float splitSpeed;
    public float gravityRaycastDistanceFP;
    public float gravityRaycastDistancePlayers;
    public float gravityValue;
    public float raycastDistanceDetectionFP;
    public float raycastDistanceDetectionPlayers;
    public float raycastDistanceFuse;
    public float groundHeightP1;
    public float groundHeightP2;

    float t1;
    float t2;
    float distanceX;
    float distanceY;
    float distanceZ;
    float totalDistance;
    float adaptDistance;
    float tP1;
    float tP2;
    float tFP;
    float tSplit;
    float angleP1;
    float angleRadP1;
    float angleP2;
    float angleRadP2;
    float angleFP;
    float angleRadFP;

    LayerMask layerMask;
    LayerMask layerMaskPlayer;
    LayerMask layerMaskDObject;
    LayerMask layerMaskFuse;

    public bool fusing;
    public bool merged;
    bool switchState;
    bool splitting;
    bool p1Jumping;
    bool p2Jumping;
    bool fpJumping;
    bool p1Hooking;
    bool p2Hooking;


    #endregion

    #region initializing
    void Start()
    {
        fusing = false;
        merged = false;
        splitting = false;
        p1Jumping = false;
        adaptDistance = 0;

        gravity = Vector3.down * gravityValue;

        lineRenderer = GetComponent<LineRenderer>();

        layerMaskPlayer = 31;
        layerMaskDObject = 8;

        layerMask = ~(1 << layerMaskPlayer);
        layerMaskFuse = ~((1 << layerMaskPlayer) | (1 << layerMaskDObject));

        transform.GetChild(0).gameObject.SetActive(false);


    }

    #endregion


    void Update()
    {
        moveP1.x = Input.GetAxis("p1_Horizontal");
        moveP1.z = Input.GetAxis("p1_Vertical");

        moveP2.x = Input.GetAxis("p2_Horizontal");
        moveP2.z = Input.GetAxis("p2_Vertical");

        move.x = moveP1.x + moveP2.x;
        move.z = moveP1.z + moveP2.z;

        if (moveP1 != Vector3.zero)
        {
            angleRadP1 = Mathf.Atan2(moveP1.x, moveP1.z);
            angleP1 = angleRadP1 * Mathf.Rad2Deg;

            if (splitting == false)
            {
                player1.transform.eulerAngles = new Vector3(0, angleP1, 0);
            }
        }

        if (moveP2 != Vector3.zero)
        {
            angleRadP2 = Mathf.Atan2(moveP2.x, moveP2.z);
            angleP2 = angleRadP2 * Mathf.Rad2Deg;

            if (splitting == false)
            {
                player2.transform.eulerAngles = new Vector3(0, angleP2, 0);
            }
        }
        
        if (move != Vector3.zero)
        {
            if (moveP1 != Vector3.zero && moveP2 != Vector3.zero)
            {
                transform.GetChild(0).transform.eulerAngles = (player1.transform.rotation.eulerAngles + player2.transform.rotation.eulerAngles) / 2;
            }

            if (moveP1 != Vector3.zero && moveP2 == Vector3.zero)
            {
                transform.GetChild(0).transform.eulerAngles = player1.transform.rotation.eulerAngles;
            }

            if (moveP1 == Vector3.zero && moveP2 != Vector3.zero)
            {
                transform.GetChild(0).transform.eulerAngles = player2.transform.rotation.eulerAngles;
            }
        }

        if (Input.GetButton("p1_Hook"))
        {
            p1Hooking = true;
        }
        else
        {
            p1Hooking = false;
        }

        if (Input.GetButton("p2_Hook"))
        {
            p2Hooking = true;
        }
        else
        {
            p2Hooking = false;
        }

        switchState = ReceivePosition.switchState;

        //if (Input.GetButton("Fuse"))
        if (switchState == true)
        {
            if (fusing == false && merged == false)
            {


                player1MemoryPosition = player1.transform.position;
                player2MemoryPosition = player2.transform.position;

                t1 = 0;
                t2 = 0;

                distanceX = player1MemoryPosition.x - player2MemoryPosition.x;
                distanceY = player1MemoryPosition.y - player2MemoryPosition.y;
                distanceZ = player1MemoryPosition.z - player2MemoryPosition.z;

                totalDistance = (Mathf.Abs(distanceX) + Mathf.Abs(distanceY) + Mathf.Abs(distanceZ));

                if (totalDistance <= checkDistance)
                {
                    fusing = true;
                }
                else
                {
                    Debug.Log("Trop loin : " + totalDistance);
                }

                adaptDistance = 1 / totalDistance;
            }
        }

        #region physics

        #region gravity

        RaycastHit hitFP;
        RaycastHit hitP1;
        RaycastHit hitP2;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hitFP, gravityRaycastDistanceFP, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hitFP.distance, Color.yellow);
            transform.position = new Vector3(transform.position.x, hitFP.point.y + 1.5f, transform.position.z);
            velocityFP = Vector3.zero;

        }
        else
        {
            if (fpJumping == false && splitting == false && merged == true)
            {
                velocityFP += gravity * Time.deltaTime;
                transform.position += velocityFP * Time.deltaTime;
            }
            else
            {
                velocityFP = Vector3.zero;
            }
        }

        if (Physics.Raycast(player1.transform.position, transform.TransformDirection(Vector3.down), out hitP1, gravityRaycastDistancePlayers, layerMask))
        {
            Debug.DrawRay(player1.transform.position, transform.TransformDirection(Vector3.down) * hitP1.distance, Color.yellow);
            player1.transform.position = new Vector3(player1.transform.position.x, hitP1.point.y + 1, player1.transform.position.z);
            velocityP1 = Vector3.zero;

            groundHeightP1 = hitP1.point.y;
        }
        else
        {
            if (p1Jumping == false && merged == false)
            {
                velocityP1 += gravity * Time.deltaTime;
                player1.transform.position += velocityP1 * Time.deltaTime;
            }
            else
            {
                velocityP1 = Vector3.zero;
            }
        }

        if (Physics.Raycast(player2.transform.position, transform.TransformDirection(Vector3.down), out hitP2, gravityRaycastDistancePlayers, layerMask))
        {
            Debug.DrawRay(player2.transform.position, transform.TransformDirection(Vector3.down) * hitP2.distance, Color.yellow);
            player2.transform.position = new Vector3(player2.transform.position.x, hitP2.point.y + 1, player2.transform.position.z);
            velocityP2 = Vector3.zero;

            groundHeightP2 = hitP2.point.y;
        }
        else
        {
            if (p2Jumping == false && merged == false)
            {
                velocityP2 += gravity * Time.deltaTime;
                player2.transform.position += velocityP2 * Time.deltaTime;
            }
            else
            {
                velocityP2 = Vector3.zero;
            }
        }

        #endregion

        #region collisionDetection

        #region playersCollisions

        if (merged == false && splitting == false)
        {
            #region p1Collisions

            RaycastHit hitCollP1;


            Debug.DrawRay(player1.transform.position, player1.transform.forward * 1000, Color.green);


            if (Physics.Raycast(player1.transform.position, player1.transform.forward, out hitCollP1, raycastDistanceDetectionPlayers, layerMask))
            {
                player1.transform.position = hitCollP1.point - player1.transform.forward;

                if (moveP1 != Vector3.zero)
                {
                    moveP1 = Vector3.Reflect(player1.transform.forward, hitCollP1.normal);
                }
            }



            #endregion


            #region p2Collisions
                
            RaycastHit hitCollP2;

            if (Physics.Raycast(player2.transform.position, player2.transform.forward, out hitCollP2, raycastDistanceDetectionPlayers, layerMask))
            {
                player2.transform.position = hitCollP2.point - player2.transform.forward;
                    
                if (moveP2 != Vector3.zero)
                {
                    moveP2 = Vector3.Reflect(player2.transform.forward, hitCollP2.normal);
                }

                #endregion
            }
        }

        #endregion

        #region fpCollisions

        if (merged == true || fusing == true)
        {

            RaycastHit hitCollFP;

            Debug.DrawRay(transform.position, transform.GetChild(0).transform.forward * 1000, Color.green);

            if (Physics.Raycast(transform.position, transform.GetChild(0).transform.forward, out hitCollFP, raycastDistanceDetectionFP, layerMask))
            {
                transform.position = hitCollFP.point - transform.GetChild(0).transform.forward;

                if (move != Vector3.zero)
                {
                    move = Vector3.Reflect(transform.GetChild(0).transform.forward, hitCollFP.normal);
                }
            }

        }

            #endregion

            #endregion


            #endregion

            #region PlayersPosition

            if (merged == false)
            {
                this.transform.position = ((player1.transform.position + player2.transform.position) / 2) + new Vector3(0, 0.6f, 0);
            }
            else
            {
                player1.transform.position = transform.position;
                player2.transform.position = transform.position;

            }

            #endregion

            #region time

            tP1 += jumpSpeed * Time.deltaTime;
            tP2 += jumpSpeed * Time.deltaTime;
            tFP += jumpSpeed * Time.deltaTime;

            tSplit += splitSpeed * Time.deltaTime;


            if (p1Hooking == false)
            {

                t1 += timeSpeed * adaptDistance * Time.deltaTime;
            }
            else
            {

            }

            if (p2Hooking == false)
            {
                t2 += timeSpeed * adaptDistance * Time.deltaTime;
            }



            #endregion


            #region movements
            if (fusing == false && merged == false && splitting == false)
            {
                if (moveP1 != Vector3.zero /* && p1CanJump == true */)
                {
                    player1.transform.Translate(transform.forward * moveP1.z * speed * Time.deltaTime, Space.World);
                    player1.transform.Translate(transform.right * moveP1.x * speed * Time.deltaTime, Space.World);

                }

                if (moveP2 != Vector3.zero /* && p2CanJump == true */ )
                {
                    player2.transform.Translate(transform.forward * moveP2.z * speed * Time.deltaTime, Space.World);
                    player2.transform.Translate(transform.right * moveP2.x * speed * Time.deltaTime, Space.World);

                }

                /*
                if(p1CanJump == false)
                {
                    player1.transform.Translate(transform.forward * jumpMoveP1.y * speed * Time.deltaTime, Space.World);
                    player1.transform.Translate(transform.right * jumpMoveP1.x * speed * Time.deltaTime, Space.World);
                }

                if (p2CanJump == false)
                {
                    player2.transform.Translate(transform.forward * jumpMoveP2.y * speed * Time.deltaTime, Space.World);
                    player2.transform.Translate(transform.right * jumpMoveP2.x * speed * Time.deltaTime, Space.World);
                }
                */
            }

            #endregion

            #region fusion

            if (fusing == true)
            {

                lineRenderer.enabled = true;

                lineRenderer.SetPosition(0, player1.transform.localPosition);
                lineRenderer.SetPosition(1, player2.transform.localPosition);


                #region fusionDisplacements

                RaycastHit hitFuseP1;


                Debug.DrawRay(player1.transform.position, transform.TransformDirection(player2.transform.position - player1.transform.position) * 1000, Color.green);

                if (Physics.Raycast(player1.transform.position, transform.TransformDirection(player2.transform.position - player1.transform.position), out hitFuseP1, raycastDistanceFuse, layerMaskFuse))
                {
                    fusing = false;
                }
                else
                {
                    player1.transform.position = new Vector3
                       (Mathf.Lerp(player1MemoryPosition.x, player2.transform.position.x, t1),
                       Mathf.Lerp(player1MemoryPosition.y, player2.transform.position.y, t1),
                       Mathf.Lerp(player1MemoryPosition.z, player2.transform.position.z, t1));
                }

                RaycastHit hitFuseP2;

                if (Physics.Raycast(player2.transform.position, transform.TransformDirection(player1.transform.position - player2.transform.position), out hitFuseP2, raycastDistanceFuse, layerMaskFuse))
                {
                    fusing = false;
                }
                else
                {
                    player2.transform.position = new Vector3
                        (Mathf.Lerp(player2MemoryPosition.x, player1.transform.position.x, t2),
                        Mathf.Lerp(player2MemoryPosition.y, player1.transform.position.y, t2),
                        Mathf.Lerp(player2MemoryPosition.z, player1.transform.position.z, t2));
                    splitting = false;
                }



                #endregion

                #region merge

                if (t1 >= 0.9f || t2 >= 0.9f)
                {
                    transform.GetChild(0).gameObject.SetActive(true);

                    player1.gameObject.SetActive(false);
                    player1.transform.position = this.transform.position;

                    player2.gameObject.SetActive(false);
                    player2.transform.position = this.transform.position;

                    fusing = false;
                    merged = true;

                }

                #endregion
            }
            else
            {
                lineRenderer.enabled = false;
            }


            #endregion

            #region fusedGameplay

            if (merged == true)
            {


                transform.Translate(transform.forward * move.z * speed * Time.deltaTime, Space.World);
                transform.Translate(transform.right * move.x * speed * Time.deltaTime, Space.World);



                if (moveP1 != Vector3.zero && moveP2 != Vector3.zero)
                {


                    float fpRotationAngle = (angleP1 + angleP2 + (2 * Mathf.PI)) / 2;

                    transform.GetChild(0).transform.eulerAngles = new Vector3(0, fpRotationAngle, 0);

                }
                else if (moveP1 != Vector3.zero && moveP2 == Vector3.zero)
                {


                    float fpRotationAngle = angleP1 + (2 * Mathf.PI);


                    transform.GetChild(0).transform.eulerAngles = new Vector3(0, fpRotationAngle, 0);

                }
                else if (moveP1 == Vector3.zero && moveP2 != Vector3.zero)
                {

                    transform.Translate(transform.forward * move.y * speed * Time.deltaTime, Space.World);
                    transform.Translate(transform.right * move.x * speed * Time.deltaTime, Space.World);

                    float fpRotationAngle = angleP2 + (2 * Mathf.PI);


                    transform.GetChild(0).transform.eulerAngles = new Vector3(0, fpRotationAngle, 0);

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
                        if (p1CanJump())
                        {
                            tP1 = 0;
                            p1Jumping = true;

                            jumpMoveP1 = moveP1;
                        }
                    }
                    else
                    {
                        if (fpCanJump())
                        {
                            tFP = 0;
                            tP1 = 0;
                            fpJumping = true;
                        }
                    }
                }
            }

            if (Input.GetButton("p2_Jump"))
            {
                if (fusing == false)
                {
                    if (merged == false)
                    {
                        if (p2CanJump())
                        {
                            tP2 = 0;
                            p2Jumping = true;

                            jumpMoveP2 = moveP2;
                        }
                    }
                    else
                    {
                        if (fpCanJump())
                        {
                            tFP = 0;
                            tP2 = 0;
                            fpJumping = true;
                        }
                    }
                }
            }

            if (p1Jumping == true)
            {
                player1.transform.Translate(transform.up * jumpCurve.Evaluate(tP1) * power, Space.World);

                if (tP1 >= 1)
                {
                    p1Jumping = false;
                }

            }

            if (p2Jumping == true)
            {
                player2.transform.Translate(transform.up * jumpCurve.Evaluate(tP2) * power, Space.World);

                if (tP2 >= 1)
                {
                    p2Jumping = false;
                }

            }

            if (fpJumping == true)
            {
                transform.Translate(transform.up * jumpCurve.Evaluate(tFP) * power, Space.World);

                if (tP1 >= 1 && tP2 >= 1)
                {

                    fpJumping = false;
                }


            }

        #endregion

        #region split

        //if (Input.GetButton("Split"))
        if (switchState == false)
        {
            if (merged == true && splitting == false)
            {
                transform.GetChild(0).gameObject.SetActive(false);

                fusing = false;
                merged = false;
                    
                player1.gameObject.SetActive(true);
                player2.gameObject.SetActive(true);
                    
                if (moveP1 != Vector3.zero)
                {
                    splitDirectionP1 = player1.transform.forward;
                }
                else
                {
                    splitDirectionP1 = Vector3.zero;
                }
                    
                if (moveP2 != Vector3.zero)
                {
                    splitDirectionP2 = player2.transform.forward;
                }
                else
                {
                    splitDirectionP2 = Vector3.zero;
                }
                    
                tSplit = 0;
                    
                splitting = true;
            }
        }

            if (splitting == true)
            {

                RaycastHit hitSplitP1;
                RaycastHit hitSplitP2;

                Debug.DrawRay(player1.transform.position, player1.transform.forward * 100, Color.red);

                if (Physics.Raycast(player1.transform.position, player1.transform.forward, out hitSplitP1, raycastDistanceDetectionPlayers, layerMask))
                {

                    incomingVec = hitSplitP1.point - player1.transform.position;
                    splitDirectionP1 = Vector3.Reflect(player1.transform.forward, hitSplitP1.normal);

                }

                Debug.DrawRay(player2.transform.position, player2.transform.forward * 100, Color.red);

                if (Physics.Raycast(player2.transform.position, player2.transform.forward, out hitSplitP2, raycastDistanceDetectionPlayers, layerMask))
                {

                    incomingVec = hitSplitP2.point - player2.transform.position;
                    splitDirectionP2 = Vector3.Reflect(player2.transform.forward, hitSplitP2.normal);

                }

                player1.transform.Translate(splitDirectionP1 * splitCurve.Evaluate(tSplit) * splitPower, Space.World);
                player2.transform.Translate(splitDirectionP2 * splitCurve.Evaluate(tSplit) * splitPower, Space.World);

                if (tSplit >= 1)
                {
                    splitting = false;
                }
            }

        #endregion

        Debug.Log("Fusing : " + fusing);
        Debug.Log("Splitting : " + splitting);
    }

    #region jumpBools

    bool p1CanJump()
    {
        return Physics.Raycast(player1.transform.position, player1.transform.TransformDirection(Vector3.down), gravityRaycastDistancePlayers, layerMask);
    }
    
    bool p2CanJump()
    {
        return Physics.Raycast(player2.transform.position, player2.transform.TransformDirection(Vector3.down), gravityRaycastDistancePlayers, layerMask);
    }

    bool fpCanJump()
    {
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), gravityRaycastDistanceFP, layerMask);
    }

    #endregion

    
}

