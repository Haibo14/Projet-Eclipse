using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Rock : MonoBehaviour
{
    Animator animator;

    public rockThrow animatorScript;

    GameObject respawnManager;
    GameObject boss;
    GameObject target;
    GameObject rockPlace;
    public GameObject colonneFractured;

    private int[] values;

    Vector3 dir;

    public float t;
    public float g;
    public float a;
    public float b;

    float v_0;
    float x_0;
    float y_0;
    float z_0;
    float x;
    float y;
    float z;
    float v_x0;
    float v_y0;
    float v_z0;
    float dist;
    float h;

    bool throwBool;
    bool playOnce;
    

    void Start()
    {

        respawnManager = GameObject.FindGameObjectWithTag("RespawnManager");
        boss = GameObject.FindGameObjectWithTag("Boss");
        rockPlace = GameObject.FindGameObjectWithTag("RockPlace");

        animator = boss.GetComponent<Animator>();

        animatorScript = animator.GetBehaviour<rockThrow>();

        values = new int[] { 0, 1 };

        int value = values[Random.Range(0, values.Length)];

        if (value == 0)
        {
            target = GameObject.FindGameObjectWithTag("Player1_Script");
        }
        else if (value == 1)
        {

            target = GameObject.FindGameObjectWithTag("Player2_Script");
        }

        playOnce = true;
        throwBool = false;

    }

    void Update()
    {

        if(animator.GetCurrentAnimatorStateInfo(0).IsName("rock") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f && playOnce == true)
        {
            Throw();
            transform.parent = null;
            playOnce = false;
            
        }

        if (animator.GetCurrentAnimatorStateInfo(0).IsName("rock rage   ") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.6f && playOnce == true)
        {
            Throw();
            transform.parent = null;
            playOnce = false;

        }

        if (throwBool == true)
        {

            Debug.DrawRay(boss.transform.position, dir * 200, Color.red);

            t += Time.deltaTime;

            x = x_0 + (v_x0 * t) * Mathf.Sin(b);

            y = y_0 - (t * h) + (v_y0 * t) - ((g * t * t) / 2);

            z = z_0 + (v_z0 * t) * Mathf.Cos(a);

            transform.position = new Vector3(z, y, x);
        }
    }


    public void Throw()
    {

        dist = Vector3.Distance(rockPlace.transform.position, target.transform.position);




        float v_0carré = dist * g * (1 / Mathf.Sin(2 * a));

        v_0 = Mathf.Sqrt(Mathf.Abs(v_0carré));

        dir = new Vector3((target.transform.position.x - rockPlace.transform.position.x), 0.0f, (target.transform.position.z - rockPlace.transform.position.z)).normalized;

        Vector3 rockPlacePosition = rockPlace.transform.position;

        b = (Vector3.Angle(rockPlace.transform.right, dir)) * Mathf.Deg2Rad;

        v_x0 = v_0 * Mathf.Cos(a);
        v_y0 = v_0 * Mathf.Sin(a);
        v_z0 = v_0 * Mathf.Cos(b);

        h = rockPlace.transform.position.y - target.transform.position.y;

        x_0 = transform.position.z;
        y_0 = transform.position.y;
        z_0 = transform.position.x;

        throwBool = true;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player1" || other.tag == "Player2" || other.tag == "FusedPlayer")
        {
            respawnManager.GetComponent<Respawn>().RespawnBoth();
        }
        else if (other.tag == "Pilar")
        {
            Debug.Log("Pilar");
            GameObject colonne = Instantiate(colonneFractured, other.transform.position, Quaternion.identity);
            colonne.transform.Rotate(-90f, 0, 0);
            Destroy(other.gameObject);
        }
        else if (other.tag == "Ground")
        {
            Destroy(gameObject);
        }
    }

}
