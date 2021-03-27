using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject fpPlayer;    
    public GameObject otherPlayer;
    public GameObject OscMaster;

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

    public string moveX;
    public string moveZ;
    public string hookingString;
    public string jumpString;

    public AnimationCurve jumpCurve;
    public AnimationCurve splitCurve;

    public Vector3 move;
    public Vector3 shift;

    Vector3 velocity;
    Vector3 gravity;
    Vector3 jumpMove;
    Vector3 splitDirection;
    Vector3 fuseDirection;
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
    public float interactDistance;
    public float carDrivingDistance;
    public float radiusDetection;
    public float angleDetection;

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
    bool driving;
    bool driven;

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
        driving = false;
        driven = false;

        players = fpPlayer.GetComponent<Players>();
        playerObject = otherPlayer.GetComponent<PlayerScript>();
        oscMessage = OscMaster.GetComponent<ReceivePosition>();

        childPlayer = transform.GetChild(0).gameObject;

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        

        gravity = Vector3.down * gravityValue;

        layerMaskPlayer = 31;
        layerMaskBush = 30;
        layerMaskDObject = 8;

        layerMask = ~((1 << layerMaskPlayer) | (1 << layerMaskBush));
        layerMaskFuse = ~((1 << layerMaskPlayer) | (1 << layerMaskBush) | (1 << layerMaskDObject));
        layerMaskInteract = ~((1 << 29));
    }
    #endregion

    void Update()
    {


        move.x = Input.GetAxis(moveX);
        move.z = Input.GetAxis(moveZ);

        //move.x = -(oscMessage.xAxis_ / 6);
        //move.z = oscMessage.zAxis_ / 6;

        Debug.DrawRay(transform.position, childPlayer.transform.forward * 100, Color.red);


        if (move != Vector3.zero)
        {
            
            if (splitting == false && fusing == false)
            {
                angleRad = Mathf.Atan2(move.x, move.z);
                angle = angleRad * Mathf.Rad2Deg;
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

        

        #region collisionDetection


        if (merged == false && splitting == false && fusing == false && driving == false)
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
            if(hitInteract.collider.tag == "DriveCar")
            {
                //Debug.Log("Appuie sur A pour conduire le chariot");


                if (Input.GetButton(hookingString))
                {
                    drivingCar = hitInteract.collider.gameObject;
                    transform.position = drivingCar.transform.position - (drivingCar.transform.forward * carDrivingDistance);
                    drivingCar.GetComponent<Car>().driving = true;
                    driving = true;
                }

                }else if(hitInteract.collider.tag == "HideCar")
            {
                //Debug.Log("Appuie sur A pour te cacher dans le chariot");

                if (Input.GetButton(hookingString))
                {
                    drivingCar = hitInteract.collider.gameObject;
                    transform.position = hitInteract.collider.transform.position;
                    driven = true;
                }
            }

        }

        if(driving == true)
        {
            transform.position = drivingCar.transform.position - (drivingCar.transform.forward * carDrivingDistance);
            move = Vector3.zero;

            if (Input.GetButtonDown(hookingString))
            {
                // Use has pressed the Space key. We don't know if they'll release or hold it, so keep track of when they started holding it.
                _hookPressedTime = Time.timeSinceLevelLoad;
                _hookHeld = false;
            }
            else if (Input.GetButtonUp(hookingString)) {
                if (!_hookHeld)
                {
                    
                }
                _hookHeld = false;
            }

            if (Input.GetButton(hookingString)) {
                if (Time.timeSinceLevelLoad - _hookPressedTime > _minimumHeldDuration)
                {
                    drivingCar.GetComponent<Car>().driving = false;
                    driving = false;
                    _hookHeld = true;

                }
            }
        }

        if(driven == true)
        {
            if (drivingCar != null)
            {

                transform.position = drivingCar.transform.position;
                move = Vector3.zero;

                if (Input.GetButton(jumpString))
                {
                    driven = false;
                }
            }
            else
            {
                driven = false;
            }

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

        #region merge

        if (merged == true)
        {
            childPlayer.gameObject.SetActive(false);
            transform.position = fpPlayer.transform.position + shift;

        }

        #endregion

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
            transform.Translate(transform.up * jumpCurve.Evaluate(tJump) * power * Time.deltaTime, Space.World);
            
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

            
            
            if (Physics.Raycast(transform.position, childPlayer.transform.forward, out hitSplit, raycastDistanceDetection, layerMask))
            {
                incomingVec = hitSplit.point - transform.position;
                splitDirection = Vector3.Reflect(childPlayer.transform.forward, hitSplit.normal);

                tSplit = tSplit/2;

                angleRad = Mathf.Atan2(splitDirection.x, splitDirection.z);
                angle = angleRad * Mathf.Rad2Deg;

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

        foreach (GameObject enemy in enemies)
        {
            float dist = Vector3.Distance(enemy.transform.position, transform.position);

            if (dist <= radiusDetection)
            {
                Vector3 targetDir = transform.position - enemy.transform.position;
                float angle = Vector3.Angle(targetDir, enemy.transform.forward);

                if(angle <= angleDetection)
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
