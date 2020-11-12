using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    #region variables

    public GameObject player1;
    public GameObject player2;

    public AnimationCurve jumpCurve;
    public AnimationCurve splitCurve;


    Vector3 player1MemoryPosition;
    Vector3 player2MemoryPosition;

    public Vector2 moveP1;
    public Vector2 moveP2;
    public Vector2 move;

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

    public bool fusing;
    public bool p1CanJump;
    public bool p2CanJump;
    public bool merged;
    bool splitting;
    bool p1Jumping;
    bool p2Jumping;
    bool fpJumping;
    bool p1Hooking;
    bool p2Hooking;


    #endregion

    #region controllerSetup1
    void Awake()
    {

    }
    #endregion

    #region initializing
    void Start()
    {
        fusing = false;
        merged = false;
        splitting = false;
        p1Jumping = false;
        p1CanJump = true;
        adaptDistance = 0;

        transform.GetChild(0).gameObject.SetActive(false);

        /*
        Physics.IgnoreCollision(this.gameObject.GetComponentInChildren<Collider>(), player1.GetComponent<Collider>());
        Physics.IgnoreCollision(player1.GetComponent<Collider>(), this.gameObject.GetComponentInChildren<Collider>());
        Physics.IgnoreCollision(this.gameObject.GetComponentInChildren<Collider>(), player2.GetComponent<Collider>());
        Physics.IgnoreCollision(player2.GetComponent<Collider>(), this.gameObject.GetComponentInChildren<Collider>());
        Physics.IgnoreCollision(player1.GetComponent<Collider>(), player2.GetComponent<Collider>());
        Physics.IgnoreCollision(player2.GetComponent<Collider>(), player1.GetComponent<Collider>());
        */
    }

    #endregion

    void FixedUpdate()
    {
        moveP1.x = Input.GetAxis("p1_Horizontal");
        moveP1.y = Input.GetAxis("p1_Vertical");

        moveP2.x = Input.GetAxis("p2_Horizontal");
        moveP2.y = Input.GetAxis("p2_Vertical");

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

        if (Input.GetButton("Fuse"))
        {
            if (fusing == false)
            {
                fusing = true;


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

            #region avoidBeingInTheGround

            /*
            if (fusing == false)
            {

                if (player1.transform.position.y < Terrain.activeTerrain.SampleHeight(player1.transform.position) + 1f)
                {
                    Vector3 posP1 = player1.transform.position;
                    posP1.y = Terrain.activeTerrain.SampleHeight(player1.transform.position) + 1f;
                    player1.transform.position = posP1;
                }

                if (player2.transform.position.y < Terrain.activeTerrain.SampleHeight(player2.transform.position) + 1f)
                {
                    Vector3 posP2 = player2.transform.position;
                    posP2.y = Terrain.activeTerrain.SampleHeight(player2.transform.position) + 1f;
                    player2.transform.position = posP2;
                }

            }
            */
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
            if (p2Hooking == false)
            {
                t2 += timeSpeed * adaptDistance * Time.deltaTime;
            }



            #endregion

            #region movements
            if (fusing == false && merged == false && splitting == false)
            {
                if (moveP1 != Vector2.zero /* && p1CanJump == true */)
                {
                    player1.transform.Translate(transform.forward * moveP1.y * speed * Time.deltaTime, Space.World);
                    player1.transform.Translate(transform.right * moveP1.x * speed * Time.deltaTime, Space.World);

                    player1.transform.eulerAngles = new Vector3(0, Mathf.Atan2(moveP1.x, moveP1.y) * Mathf.Rad2Deg, 0);
                }

                if (moveP2 != Vector2.zero /* && p2CanJump == true */ )
                {
                    player2.transform.Translate(transform.forward * moveP2.y * speed * Time.deltaTime, Space.World);
                    player2.transform.Translate(transform.right * moveP2.x * speed * Time.deltaTime, Space.World);


                    player2.transform.eulerAngles = new Vector3(0, Mathf.Atan2(moveP2.x, moveP2.y) * Mathf.Rad2Deg, 0);
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
                #region fusionDisplacements

                player1.transform.position = new Vector3
                    (Mathf.Lerp(player1MemoryPosition.x, player2.transform.position.x, t1),
                    Mathf.Lerp(player1MemoryPosition.y, player2.transform.position.y, t1),
                    Mathf.Lerp(player1MemoryPosition.z, player2.transform.position.z, t1));

                player2.transform.position = new Vector3
                    (Mathf.Lerp(player2MemoryPosition.x, player1.transform.position.x, t2),
                     Mathf.Lerp(player2MemoryPosition.y, player1.transform.position.y, t2),
                    Mathf.Lerp(player2MemoryPosition.z, player1.transform.position.z, t2));

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


            #endregion

            #region fusedGameplay

            if (merged == true)
            {
                move = moveP1 + moveP2;

                if (moveP1 != Vector2.zero && moveP2 != Vector2.zero)
                {

                    transform.Translate(transform.forward * move.y * speed * Time.deltaTime, Space.World);
                    transform.Translate(transform.right * move.x * speed * Time.deltaTime, Space.World);

                    float p1RotationAngle = Mathf.Atan2(moveP1.x, moveP1.y) * Mathf.Rad2Deg;
                    float p2RotationAngle = Mathf.Atan2(moveP2.x, moveP2.y) * Mathf.Rad2Deg;
                    float fpRotationAngle = (p1RotationAngle + p2RotationAngle) / 2;

                    transform.GetChild(0).transform.eulerAngles = new Vector3(0, fpRotationAngle, 0);

                }
                else if (moveP1 != Vector2.zero && moveP2 == Vector2.zero)
                {

                    transform.Translate(transform.forward * move.y * speed * Time.deltaTime, Space.World);
                    transform.Translate(transform.right * move.x * speed * Time.deltaTime, Space.World);

                    float p1RotationAngle = Mathf.Atan2(moveP1.x, moveP1.y) * Mathf.Rad2Deg;
                    float fpRotationAngle = p1RotationAngle;


                    transform.GetChild(0).transform.eulerAngles = new Vector3(0, fpRotationAngle, 0);

                }
                else if (moveP1 == Vector2.zero && moveP2 != Vector2.zero)
                {

                    transform.Translate(transform.forward * move.y * speed * Time.deltaTime, Space.World);
                    transform.Translate(transform.right * move.x * speed * Time.deltaTime, Space.World);

                    float p2RotationAngle = Mathf.Atan2(moveP2.x, moveP2.y) * Mathf.Rad2Deg;
                    float fpRotationAngle = p2RotationAngle;


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
                        if (p1CanJump == true)
                        {
                            tP1 = 0;
                            p1Jumping = true;
                            p1CanJump = false;

                            jumpMoveP1 = moveP1;
                        }
                    }
                    else
                    {
                        if (p1CanJump == true)
                        {
                            tFP = 0;
                            tP1 = 0;
                            fpJumping = true;
                            p1CanJump = false;
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
                        if (p2CanJump == true)
                        {
                            tP2 = 0;
                            p2Jumping = true;
                            p2CanJump = false;

                            jumpMoveP2 = moveP2;
                        }
                    }
                    else
                    {
                        if (p2CanJump == true)
                        {
                            tFP = 0;
                            tP2 = 0;
                            fpJumping = true;
                            p2CanJump = false;
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

            #region splitInUpdate

            if (Input.GetButton("Split"))
            {
                if (merged == true && splitting == false)
                {
                    transform.GetChild(0).gameObject.SetActive(false);

                    fusing = false;
                    merged = false;

                    player1.gameObject.SetActive(true);


                    player2.gameObject.SetActive(true);


                    splitMoveP1 = moveP1;
                    splitMoveP2 = moveP2;

                    tSplit = 0;

                    splitting = true;
                }
            }

            if (splitting == true)
            {

                player1.transform.Translate(transform.forward * splitMoveP1.y * splitCurve.Evaluate(tSplit) * splitPower, Space.World);
                player1.transform.Translate(transform.right * splitMoveP1.x * splitCurve.Evaluate(tSplit) * splitPower, Space.World);
                //player1.transform.Translate(transform.up * ((Mathf.Abs(moveP1.y) + Mathf.Abs(moveP1.x))/2) * jumpCurve.Evaluate(tSplit * 2) * splitPowerY, Space.World);

                player2.transform.Translate(transform.forward * splitMoveP2.y * splitCurve.Evaluate(tSplit) * splitPower, Space.World);
                player2.transform.Translate(transform.right * splitMoveP2.x * splitCurve.Evaluate(tSplit) * splitPower, Space.World);
                //player2.transform.Translate(transform.up * ((Mathf.Abs(moveP2.y) + Mathf.Abs(moveP2.x)) / 2) * jumpCurve.Evaluate(tSplit * 2) * splitPowerY, Space.World);

                if (tSplit >= 1)
                {
                    splitting = false;
                }

            }

            #endregion
        }


        
    }

