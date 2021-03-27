using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    // put the points from unity interface
    public GameObject[] wayPointObjectList;
    public Transform[] wayPointList;

    public int currentWayPoint = 0;
    int i = -1;

    Transform targetWayPoint;

    public GameObject fpPlayer;

    public Players players;

    public Vector3 move;

    public float speed = 4f;

    float playerAngleRad;
    float playerAngle;
    float multiplicator;

    public bool driving;

    // Use this for initialization
    void Start()
    {
        fpPlayer = GameObject.FindGameObjectWithTag("FusedPlayer");
        players = fpPlayer.GetComponent<Players>();

        wayPointObjectList = GameObject.FindGameObjectsWithTag("Waypoint");
        wayPointList = new Transform[wayPointObjectList.Length];

        foreach (GameObject wayPointObject in wayPointObjectList)
        {
            i++;

            wayPointList[i] = wayPointObject.transform; 
        }
    }

    // Update is called once per frame
    void Update()
    {
        move = players.move;

        //if (driving == true)
        {
            // check if we have somewere to walk
            if (currentWayPoint < this.wayPointList.Length)
            {
                if (targetWayPoint == null)
                    targetWayPoint = wayPointList[currentWayPoint];
                walk();
            }
        }
        
    }

    void walk()
    {/*
        playerAngleRad = Mathf.Atan2(move.x, move.z);
        playerAngle = playerAngleRad * Mathf.Rad2Deg;

        if (playerAngle > 0)
        {
            multiplicator = playerAngle / transform.rotation.eulerAngles.y;
        }
        else
        {
            multiplicator = 0;
        }
        
        Debug.Log(transform.rotation.eulerAngles.y);
        Debug.Log(playerAngle);
        */
        // rotate towards the target
        transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.position - transform.position, /*multiplicator */ speed * Time.deltaTime, 0.0f);

        // move towards the target
        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position,/* multiplicator */ speed * Time.deltaTime);

        if (transform.position == targetWayPoint.position)
        {
            currentWayPoint++;

            if (currentWayPoint >= this.wayPointList.Length)
            {
                Destroy(this.gameObject);
                currentWayPoint = 0;
            }

            targetWayPoint = wayPointList[currentWayPoint];
        }
    }
}