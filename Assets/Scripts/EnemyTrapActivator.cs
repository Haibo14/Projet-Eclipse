using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrapActivator : MonoBehaviour
{
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    GameObject enemyPrefab;

    private int[] values;

    public GameObject journey;

    public Transform spawnSpot;

    public bool playOnce;

    void Start()
    {
        values = new int[] { 0, 1, 2};
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        int value = values[Random.Range(0, values.Length)];

        if (playOnce == true)
        {
            if (other.tag == "Player1" || other.tag == "Player2" || other.tag == "FusedPlayer")
            {

                if(value == 0)
                {
                    enemyPrefab = enemyPrefab1;
                }
                else if (value == 1)
                {
                    enemyPrefab = enemyPrefab2;
                }
                else if (value == 2)
                {
                    enemyPrefab = enemyPrefab3;
                }


                GameObject enemy = Instantiate(enemyPrefab, spawnSpot.position, Quaternion.identity);
                enemy.GetComponent<EnemyScript>().journey = journey;

                playOnce = false;
            }
        }
    }
}
