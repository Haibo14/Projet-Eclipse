using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeRock : MonoBehaviour
{
    public GameObject enemy;

    GameObject boss;
    GameObject target;
    GameObject lastEnemy;
    
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


    void Start()
    {
        boss = GameObject.FindGameObjectWithTag("Boss");

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

        dist = Vector3.Distance(boss.transform.position, target.transform.position);

        float v_0carré = dist * g * (1 / Mathf.Sin(2 * a));

        v_0 = Mathf.Sqrt(Mathf.Abs(v_0carré));

        dir = new Vector3((target.transform.position.x - boss.transform.position.x), 0.0f, (target.transform.position.z - boss.transform.position.z)).normalized;

        b = (Vector3.Angle(boss.transform.right, dir)) * Mathf.Deg2Rad;

        v_x0 = v_0 * Mathf.Cos(a);
        v_y0 = v_0 * Mathf.Sin(a);
        v_z0 = v_0 * Mathf.Cos(b);

        h = boss.transform.position.y - target.transform.position.y;

        x_0 = transform.position.z;
        y_0 = transform.position.y;
        z_0 = transform.position.x;
    }

    void Update()
    {

        Debug.DrawRay(boss.transform.position, dir * 200, Color.red);

        t += Time.deltaTime;

        x = x_0 + (v_x0 * t) * Mathf.Sin(b);

        y = y_0 + (v_y0 * t) - ((g * t * t) / 2);

        z = z_0 + (v_z0 * t) * Mathf.Cos(a);

        transform.position = new Vector3(z, y, x);
    }

    void OnTriggerEnter(Collider other)
    {
        
        if (other.tag == "Ground")
        {
            lastEnemy = Instantiate(enemy, transform.position + new Vector3(0, 2, 0), Quaternion.identity);
            lastEnemy.transform.LookAt(new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z));
            Destroy(gameObject);
        }
    }
}
