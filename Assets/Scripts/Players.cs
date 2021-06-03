using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Players : MonoBehaviour
{
    #region variables

    public GameObject player1;
    public GameObject player2;
    public GameObject respawnManager;

    GameObject[] enemies;

    public PlayerScript playerObject1;
    public PlayerScript playerObject2;

    public ReceivePosition ReceivePosition;

    //LineRenderer lineRenderer;

    public AnimationCurve jumpCurve;
    public AnimationCurve splitCurve;


    Vector3 player1MemoryPosition;
    Vector3 player2MemoryPosition;
    Vector3 velocity;
    Vector3 lastVelocity;
    Vector3 gravity;
    Vector3 splitDirectionP1;
    Vector3 splitDirectionP2;
    Vector3 incomingVec;
    Vector3 lastFramePosition;


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
    public float groundHeight;

    float t1;
    float t2;
    float distanceX;
    float distanceY;
    float distanceZ;
    float totalDistance;
    float adaptDistance;
    float tP1;
    float tP2;
    public float tFP;
    float tSplit;
    float angleP1;
    float angleRadP1;
    float angleP2;
    float angleRadP2;
    float angleFP;
    float angleRadFP;
    public float radiusDetection;
    public float angleDetection;

    public double _decelerationTolerance;

    LayerMask layerMask;
    LayerMask layerMaskPlayer;
    LayerMask layerMaskBush;
    LayerMask layerMaskDObject;
    LayerMask layerMaskFuse;
    LayerMask layerMaskMoving;

    public bool fusing;
    public bool merged;
    public bool switchState;
    public bool splitting;
    public bool fpJumping;
    public bool allowFuse;
    public bool changeState;

    public bool IsAlive = true;


    #endregion

    #region initializing
    void Start()
    {
        fusing = false;
        merged = false;
        splitting = false;
        allowFuse = true;
        changeState = true;
        adaptDistance = 0;

        playerObject1 = player1.GetComponent<PlayerScript>();
        playerObject2 = player2.GetComponent<PlayerScript>();

        gravity = Vector3.down * gravityValue;

        //lineRenderer = GetComponent<LineRenderer>();

        layerMaskPlayer = 31;
        layerMaskBush = 30;
        layerMaskDObject = 8;

        layerMask = ~((1 << layerMaskPlayer) | (1 << layerMaskBush));
        layerMaskFuse = ~((1 << layerMaskPlayer) | (1 << layerMaskBush) | (1 << layerMaskDObject));

        layerMaskMoving = ~((1 << 28));

        transform.GetChild(0).gameObject.SetActive(false);


    }

    #endregion


    void Update()
    {
        moveP1.x = Input.GetAxis("p1_Horizontal");
        moveP1.z = Input.GetAxis("p1_Vertical");

        moveP2.x = Input.GetAxis("p2_Horizontal");
        moveP2.z = Input.GetAxis("p2_Vertical");
        
        if(moveP1 == Vector3.zero)
        {

            move.x = moveP2.x;
            move.z = moveP2.z;

        }else if (moveP2 == Vector3.zero)
        {

            move.x = moveP1.x;
            move.z = moveP1.z;
        }
        else
        {

            move.x = (moveP1.x + moveP2.x)/2;
            move.z = (moveP1.z + moveP2.z)/2;
        }


        player1MemoryPosition = player1.transform.position;
        player2MemoryPosition = player2.transform.position;


        distanceX = player1MemoryPosition.x - player2MemoryPosition.x;
        distanceY = player1MemoryPosition.y - player2MemoryPosition.y;
        distanceZ = player1MemoryPosition.z - player2MemoryPosition.z;

        totalDistance = (Mathf.Abs(distanceX) + Mathf.Abs(distanceY) + Mathf.Abs(distanceZ));

        

        

        //switchState = ReceivePosition.switchState;

        if (switchState == true || Input.GetButton("Fuse"))
        {
            if (fusing == false && merged == false && playerObject1.driven == false && playerObject2.driven == false && playerObject1.IsAlive && playerObject2.IsAlive && changeState == true)
            {

                adaptDistance = 1 / totalDistance;

                if (adaptDistance == Mathf.Infinity)
                {
                    adaptDistance = 0.1f;
                }

                playerObject1.adaptDistance = adaptDistance;
                playerObject2.adaptDistance = adaptDistance;

                if (totalDistance <= checkDistance)
                {
                    playerObject1.tFuse = 0;
                    playerObject2.tFuse = 0;

                    playerObject1.Fuse();
                    playerObject2.Fuse();

                    fusing = true;
                }
                else
                {
                    Debug.Log("Trop loin : " + totalDistance);
                }

            }
        }

        #region physics

        #region gravity

        RaycastHit hitFP;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hitFP, gravityRaycastDistanceFP, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hitFP.distance, Color.yellow);

            Vector3 oldPosition = transform.position;

            transform.position = new Vector3(transform.position.x, hitFP.point.y + 2.25f, transform.position.z);
            velocity = Vector3.zero;


            groundHeight = hitFP.point.y;

            if (Physics.Raycast(oldPosition, transform.TransformDirection(Vector3.down), out hitFP, gravityRaycastDistanceFP, ~layerMaskMoving))
            {
                if (lastFramePosition == Vector3.zero || merged == false)
                {
                    lastFramePosition = hitFP.collider.transform.position;
                }

                transform.position = new Vector3(hitFP.point.x, transform.position.y, hitFP.point.z) + (hitFP.collider.transform.position - lastFramePosition);
                lastFramePosition = hitFP.collider.transform.position;
            }
            else
            {
                lastFramePosition = Vector3.zero;
            }

        }
        else
        {
            if (fpJumping == false && splitting == false && merged == true)
            {
                velocity += gravity * Time.deltaTime;
                transform.position += velocity * Time.deltaTime;
            }
            else
            {
                velocity = Vector3.zero;
            }
        }

        if (IsAlive)
        {
            

            IsAlive = Mathf.Abs(velocity.y - lastVelocity.y) < _decelerationTolerance;
            lastVelocity.y = velocity.y;

        }
        else
        {
            merged = false;
            changeState = true;
            respawnManager.GetComponent<Respawn>().player1Live = false;
            respawnManager.GetComponent<Respawn>().player2Live = false;

        }

        #endregion

        #region collisionDetection


        if (merged == true || fusing == true)
        {

            RaycastHit hitCollFP;

            Debug.DrawRay(transform.position, transform.GetChild(0).transform.forward * 1000, Color.black);

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

        #region PlayersPosition

        if (merged == false)
        {
            this.transform.position = ((player1.transform.position + player2.transform.position) / 2) + new Vector3(0, 0.6f, 0);
        }

        #endregion

        #region time

        tFP += jumpSpeed * Time.deltaTime;

        tSplit += splitSpeed * Time.deltaTime;


        #endregion

        #region fusion

        if (fusing == true)
        {

            lastFramePosition = Vector3.zero;

            //lineRenderer.enabled = true;

            //lineRenderer.SetPosition(0, player1.transform.localPosition);
            //lineRenderer.SetPosition(1, player2.transform.localPosition);

            t1 = playerObject1.tFuse;
            t2 = playerObject2.tFuse;

            #region merge

            //Debug.Log(t1);
            //Debug.Log(t2);

            if (t1 >= 0.95f || t2 >= 0.95f)
            {
                if(totalDistance <= 3)
                {
                    transform.GetChild(0).gameObject.SetActive(true);


                    fusing = false;
                    playerObject1.fusing = false;
                    playerObject2.fusing = false;

                    merged = true;
                    playerObject1.merged = true;
                    playerObject2.merged = true;

                    changeState = false;
                }
            }

            /*
            if (t1 >= 0.9f || t2 >= 0.9f)
            {
                transform.GetChild(0).gameObject.SetActive(true);


                fusing = false;
                playerObject1.fusing = false;
                playerObject2.fusing = false;

                merged = true;
                playerObject1.merged = true;
                playerObject2.merged = true;

                changeState = false;

            }
            */
            #endregion
        }
        else
        {
            //lineRenderer.enabled = false;
        }


        #endregion

        #region fusedGameplay

        if (merged == true)
        {


            transform.Translate(transform.forward * move.z * speed * Time.deltaTime, Space.World);
            transform.Translate(transform.right * move.x * speed * Time.deltaTime, Space.World);

            angleP1 = playerObject1.transform.GetChild(0).transform.eulerAngles.y;
            angleP2 = playerObject2.transform.GetChild(0).transform.eulerAngles.y;

        if (moveP1 == Vector3.zero && moveP2 == Vector3.zero)
        {
                transform.GetChild(0).transform.eulerAngles = transform.GetChild(0).transform.eulerAngles;

        }
        else if (moveP1 != Vector3.zero && moveP2 != Vector3.zero)
        {


            float fpRotationAngle = (angleP1 + angleP2) / 2;

            transform.GetChild(0).transform.eulerAngles = new Vector3(0, fpRotationAngle, 0);

        }
        else if (moveP1 != Vector3.zero && moveP2 == Vector3.zero)
        {


            float fpRotationAngle = angleP1;


            transform.GetChild(0).transform.eulerAngles = new Vector3(0, fpRotationAngle, 0);

        }
        else if (moveP1 == Vector3.zero && moveP2 != Vector3.zero)
        {


            float fpRotationAngle = angleP2;


            transform.GetChild(0).transform.eulerAngles = new Vector3(0, fpRotationAngle, 0);

        }
        }
        #endregion



        if (fpJumping == true)
        {

            transform.Translate(transform.up * jumpCurve.Evaluate(tFP) * power * Time.deltaTime, Space.World);

            if (tFP >= 1)
            {
                fpJumping = false;
            }
        }


        #region split

        if (/*switchState == false || */Input.GetButton("Split"))
        {
            if (merged == true && splitting == false && changeState == false)
            {
                lastFramePosition = Vector3.zero;

                changeState = true;

                transform.GetChild(0).gameObject.SetActive(false);

                fusing = false;

                merged = false;
                    
                tSplit = 0;
                    
                splitting = true;
                playerObject1.Split();
                playerObject2.Split();
            }
        }

            if (splitting == true)
            {
                if (tSplit >= 1)
                {
                    splitting = false;
                }
            }

        #endregion

        #region detectedOrNot


        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(enemy.transform.position, transform.position);

            if (dist <= radiusDetection)
            {
                Vector3 targetDir = transform.position - enemy.transform.position;
                float angle = Vector3.Angle(targetDir, enemy.transform.forward);

                if (angle <= angleDetection)
                {
                    RaycastHit checkHit;
                    if (Physics.Raycast(enemy.transform.position, targetDir, out checkHit, dist, layerMask))
                    {

                    }
                    else
                    {
                        enemy.GetComponent<EnemyScript>().target = transform;
                       // enemy.GetComponent<EnemyScript>().Spot();
                    }
                }

            }
        }

        #endregion

    }

    #region jumpBool

    bool fpCanJump()
    {
        return Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), gravityRaycastDistanceFP, layerMask);
    }

    #endregion

    
}

