using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{   
    public Animator animator;

    public GameObject fpPlayer;    
    public GameObject otherPlayer;
    public GameObject OscMaster;
    public GameObject respawnManager;

    GameObject[] enemies;

    GameObject childPlayer;
    GameObject drivingCar;

    public Players players;
    public PlayerScript playerObject;
    public ReceivePosition oscMessage;

    LayerMask layerMask;
    LayerMask layerMaskPlayer;
    LayerMask layerMaskBush;
    LayerMask layerMaskFuse;
    LayerMask layerMaskDObject;
    LayerMask layerMaskInteract;
    LayerMask layerMaskMoving;

    public string moveX;
    public string moveZ;
    public string hookingString;
    public string jumpString;

    public AnimationCurve jumpCurve;
    public AnimationCurve splitCurve;

    public Vector3 move;
    public Vector3 shift;

    Vector3 velocity;
    Vector3 lastVelocity;
    Vector3 gravity;
    Vector3 jumpMove;
    Vector3 splitDirection;
    Vector3 fuseDirection;
    Vector3 incomingVec;
    Vector3 memoryPosition;
    Vector3 lastFramePosition;

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
    public float interactDistance;
    public float carDrivingDistance;
    public float radiusDetection;
    public float angleDetection;
    public float velocityAnimation;
    public double _decelerationTolerance;

    float angle;
    float angleRad;
    float tSplit;

    public int playerID;

    public bool fusing;
    public bool merged;
    public bool splitting;
    bool hooking;
    bool jumping;
    bool allowFuse;
    public bool driven;
    public bool IsAlive = true;
    public bool firstTouch;
    public bool jump;
    bool interact;
    

    private const float _minimumHeldDuration = 0.25f;
    private float _hookPressedTime = 0;
    private bool _hookHeld = false;

    #region start
    void Start()
    {
        fusing = false;
        merged = false;
        splitting = false;
        jumping = false;
        allowFuse = true;
        driven = false;
        firstTouch = false;

        players = fpPlayer.GetComponent<Players>();
        playerObject = otherPlayer.GetComponent<PlayerScript>();
        oscMessage = OscMaster.GetComponent<ReceivePosition>();

        childPlayer = transform.GetChild(0).gameObject;
        Debug.Log(childPlayer);

        enemies = GameObject.FindGameObjectsWithTag("Enemy");


        gravity = Vector3.down * gravityValue;

        layerMaskPlayer = 31;
        layerMaskBush = 30;
        layerMaskDObject = 8;

        layerMask = ~((1 << layerMaskPlayer) | (1 << layerMaskBush));
        layerMaskFuse = ~((1 << layerMaskPlayer) | (1 << layerMaskBush) | (1 << layerMaskDObject));
        layerMaskInteract = ~((1 << 29));
        layerMaskMoving = ~((1 << 28));
    }
    #endregion

    void Update()
    {


        move.x = Input.GetAxis(moveX);
        move.z = Input.GetAxis(moveZ);

        if (move == Vector3.zero)
        {

            if (playerID == 0)
            {

                move.x = (oscMessage.xAxis_p1 * 1.0f / 6);
                move.z = oscMessage.zAxis_p1 * 1.0f / 6;
                jump = oscMessage.buttonJumpP1;
                interact = oscMessage.buttonInteractP1;
            }
            else if (playerID == 1)
            {

                move.x = (oscMessage.xAxis_p2 * 1.0f / 6);
                move.z = oscMessage.zAxis_p2 * 1.0f / 6;
                jump = oscMessage.buttonJumpP2;
                interact = oscMessage.buttonInteractP2;
            }
        }   



        velocityAnimation = Mathf.Abs(move.x) + Mathf.Abs(move.z);

        if (playerID == 0)
        {
            animator.SetFloat("velocityP1", velocityAnimation);
        }
        else if (playerID == 1)
        {
            animator.SetFloat("velocityP2", velocityAnimation);
        }

            Debug.DrawRay(transform.position, childPlayer.transform.forward * 100, Color.red);


        if (move != Vector3.zero)
        {
            
            if (splitting == false && fusing == false && Time.timeScale != 0)
            {
                angleRad = Mathf.Atan2(move.x, move.z);
                angle = angleRad * Mathf.Rad2Deg;
                childPlayer.transform.eulerAngles = new Vector3(0, angle, 0);
            }
        }

        if (Input.GetButton(hookingString) || interact == true)
        {
            hooking = true;
        }
        else
        {
            hooking = false;
        }

        #region physics

        

        #region collisionDetection


        if (merged == false && splitting == false && fusing == false && driven == false)
        {

            RaycastHit hitColl;



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

    

        #region time

        tJump += jumpSpeed * Time.deltaTime;

        tSplit += splitSpeed * Time.deltaTime;


        if (hooking == false)
        {
            tFuse += timeSpeed * adaptDistance * Time.deltaTime;

        }

        #endregion


        

        #region car

        RaycastHit hitInteract;

        if (Physics.Raycast(transform.position, childPlayer.transform.forward, out hitInteract, interactDistance, ~layerMaskInteract))
        {
            if(hitInteract.collider.tag == "HideCar")
            {
                //Debug.Log("Appuie sur A pour te cacher dans le chariot");

                if (Input.GetButton(hookingString) || interact == true)
                {
                    drivingCar = hitInteract.collider.gameObject;
                    transform.position = hitInteract.collider.transform.position + new Vector3(0, 1, 0);
                    driven = true;
                }
            }

        }

        

        if(driven == true)
        {
            if (drivingCar != null)
            {

                transform.position = drivingCar.transform.position;
                move = Vector3.zero;

                gravity = Vector3.zero;

                if (Input.GetButton(jumpString) || jump == true)
                {
                    driven = false;

                    gravity = Vector3.down * gravityValue;
                }
            }
            else
            {
                driven = false;

                gravity = Vector3.down * gravityValue;
            }

        }



        #endregion

        #region movements
        if (fusing == false && merged == false && splitting == false && IsAlive == true)
        {
            if (move != Vector3.zero)
            {
                transform.Translate(transform.forward * move.z * speed * Time.deltaTime, Space.World);
                transform.Translate(transform.right * move.x * speed * Time.deltaTime, Space.World);
            }
        }

        #endregion

        RaycastHit hitFuse;

        

        if (fusing == true)
        {
            angleRad = Mathf.Atan2(transform.TransformDirection(otherPlayer.transform.position - transform.position).x, transform.TransformDirection(otherPlayer.transform.position - transform.position).z);
            angle = angleRad * Mathf.Rad2Deg;
            childPlayer.transform.eulerAngles = new Vector3(0, angle, 0);

            #region fusionDisplacements

            Debug.DrawRay(transform.position, transform.TransformDirection(otherPlayer.transform.position - transform.position) * 1000, Color.green);

            if (allowFuse == true)
            {

                if (Physics.Raycast(transform.position, transform.TransformDirection(otherPlayer.transform.position - transform.position), out hitFuse, raycastDistanceDetection, layerMaskFuse))
                {
                    allowFuse = false;

                    incomingVec = hitFuse.point - transform.position;
                    fuseDirection = Vector3.Reflect(childPlayer.transform.forward, hitFuse.normal);

                    angleRad = Mathf.Atan2(fuseDirection.x, fuseDirection.z);
                    angle = angleRad * Mathf.Rad2Deg;

                }
                else
                {
                    fuseDirection = Vector3.zero;

                    transform.position = new Vector3
                       (Mathf.Lerp(memoryPosition.x, otherPlayer.transform.position.x, tFuse),
                       Mathf.Lerp(memoryPosition.y, otherPlayer.transform.position.y, tFuse),
                       Mathf.Lerp(memoryPosition.z, otherPlayer.transform.position.z, tFuse));
                }
            }
            else
            {
                
                    //fuseDirection = Vector3.zero;
            }


            childPlayer.transform.eulerAngles = new Vector3(0, angle, 0);
            

            transform.Translate(fuseDirection * splitCurve.Evaluate(tFuse) * splitPower * Time.deltaTime, Space.World);
            

            if (tFuse >= 1)
            {
                fuseDirection = Vector3.zero;

                fusing = false;
                players.fusing = false;

                allowFuse = true;

            }
            
            
            #endregion

           
        }

        #region gravity

        RaycastHit hit;



        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), out hit, gravityRaycastDistancePlayers, layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.down) * hit.distance, Color.yellow);

            Vector3 oldPosition = transform.position;

            transform.position = new Vector3(transform.position.x, hit.point.y + 1.7f, transform.position.z);
            velocity = Vector3.zero;

            groundHeight = hit.point.y;
            firstTouch = true;

            if (Physics.Raycast(oldPosition, transform.TransformDirection(Vector3.down), out hit, gravityRaycastDistancePlayers, ~layerMaskMoving))
            {
                //transform.parent = hit.collider.gameObject.transform;


                if (lastFramePosition == Vector3.zero || merged == true)
                {
                    lastFramePosition = hit.collider.transform.position;
                }

                transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z) + (hit.collider.transform.position - lastFramePosition);
                lastFramePosition = hit.collider.transform.position;
            }
            else
            {
                //transform.parent = null;
                lastFramePosition = Vector3.zero;
            }
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

        if (firstTouch)
        {

            if (IsAlive)
            {   
                childPlayer.SetActive(true);
                /*
                if (velocity != Vector3.zero)
                {
                    Debug.Log(velocity);
                    Debug.Log(Mathf.Abs(velocity.y - lastVelocity.y));
                }
                */
                IsAlive = Mathf.Abs(velocity.y - lastVelocity.y) < _decelerationTolerance;
                lastVelocity.y = velocity.y;

            }
            else
            {
                if (playerID == 0)
                {
                    respawnManager.GetComponent<Respawn>().player1Live = false;
                }

                if (playerID == 1)
                {
                    respawnManager.GetComponent<Respawn>().player2Live = false;
                }

                childPlayer.SetActive(false);
                transform.position = otherPlayer.transform.position;
            }
        }

        #endregion

        #region merge

        if (merged == true)
        {
            childPlayer.gameObject.SetActive(false);
            transform.position = fpPlayer.transform.position + shift;

        }

        #endregion

        #region jump


        if (Input.GetButton(jumpString) || jump == true)
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
            animator.SetBool("jump", jumping);
            transform.Translate(transform.up * jumpCurve.Evaluate(tJump) * power * Time.deltaTime, Space.World);
            
            if (tJump >= 1)
            {
                jumping = false;
                animator.SetBool("jump", jumping);
            }
        }

        #endregion
        
        #region split
        
        

        if (splitting == true)
        {

            RaycastHit hitSplit;

            lastFramePosition = Vector3.zero;

            if (Physics.Raycast(transform.position, childPlayer.transform.forward, out hitSplit, raycastDistanceDetection, layerMask))
            {
                incomingVec = hitSplit.point - transform.position;
                splitDirection = Vector3.Reflect(childPlayer.transform.forward, hitSplit.normal);

                tSplit = tSplit/2;

                angleRad = Mathf.Atan2(splitDirection.x, splitDirection.z);
                angle = angleRad * Mathf.Rad2Deg;

                if (hitSplit.collider.tag == "Breakable_Barricade")
                {
                    hitSplit.collider.gameObject.GetComponent<BoxCollider>().enabled = false;
                }

            }

            childPlayer.transform.eulerAngles = new Vector3(0, angle, 0);
            transform.Translate(splitDirection * splitCurve.Evaluate(tSplit) * splitPower * Time.deltaTime, Space.World);


            if (tSplit >= 1)
            {
                splitting = false;
                allowFuse = true;
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

                if (Mathf.Abs(enemy.transform.position.y - transform.position.y) <= 10)
                {

                    if (angle <= angleDetection)
                    {
                        RaycastHit checkHit;
                        if (Physics.Raycast(enemy.transform.position, targetDir, out checkHit, dist, layerMask))
                        {

                        }
                        else
                        {
                            enemy.GetComponent<EnemyScript>().Spot(playerID, transform);
                        }
                    }
                }

            }
            else
            {
                enemy.GetComponent<EnemyScript>().Unspot(playerID);
            }
        }

        #endregion

    }

    public void Fuse()
    {
        if (allowFuse == true)
        {
            memoryPosition = transform.position;

            if (move != Vector3.zero)
            {
                fuseDirection = childPlayer.transform.forward;
            }
            else
            {
                fuseDirection = Vector3.zero;
            }

            fusing = true;
        }
    }

    public void Split()
    {
        if (merged == true && splitting == false)
        {
            merged = false;

            childPlayer.gameObject.SetActive(true);

            tSplit = 0;

            splitting = true;

            if (move != Vector3.zero)
            {
                splitDirection = childPlayer.transform.forward;
            }
            else
            {
                splitDirection = Vector3.zero;
                tSplit = 1;
            }

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
