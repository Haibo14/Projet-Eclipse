using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialMineCar : MonoBehaviour
{
    // put the points from unity interface
    public GameObject[] wayPointObjectList;
    public Transform[] wayPointList;

    public int currentWayPoint = 0;
    int i = -1;

    Transform targetWayPoint;

    public GameObject fpPlayer;
    public GameObject pressurePlate;

    public Players players;

    public Vector3 move;

    public float speed = 4f;

    float playerAngleRad;
    float playerAngle;
    float multiplicator;

    public bool driving;

    void Start()
    {
        fpPlayer = GameObject.FindGameObjectWithTag("FusedPlayer_Script");
        players = fpPlayer.GetComponent<Players>();

        pressurePlate = GameObject.FindGameObjectWithTag("PressurePlate");

        wayPointObjectList = GameObject.FindGameObjectsWithTag("Waypoint");
        Debug.Log(wayPointObjectList.Length);
        wayPointList = new Transform[wayPointObjectList.Length];

        foreach (GameObject wayPointObject in wayPointObjectList)
        {
            i++;

            wayPointList[i] = wayPointObject.transform;
        }
    }

    void Update()
    {




        if (currentWayPoint < this.wayPointList.Length)
        {
            if (targetWayPoint == null)
                targetWayPoint = wayPointList[currentWayPoint];
            walk();
        }



    }

    void walk()
    {
        if (pressurePlate.GetComponent<PressurePlateMine>().pressed == true)
        {
            multiplicator = 1;
        }
        else
        {
            multiplicator = 0;
        }

        transform.forward = Vector3.RotateTowards(transform.forward, targetWayPoint.position - transform.position, multiplicator * speed * Time.deltaTime, 0.0f);

        transform.position = Vector3.MoveTowards(transform.position, targetWayPoint.position, multiplicator * speed * Time.deltaTime);


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
