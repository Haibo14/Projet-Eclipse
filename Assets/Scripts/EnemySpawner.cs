﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab1;
    public GameObject enemyPrefab2;
    public GameObject enemyPrefab3;
    GameObject enemyPrefab;
    public GameObject player1;
    public GameObject player2;
    public GameObject journey;

    public Transform spawnSpot1;
    public Transform spawnSpot2;
    public Transform spawnSpot3;

    public Transform[] targetJourney;

    public bool playOnce;

    private int[] values;
    int value;

    void Start()
    {
        values = new int[] { 0, 1, 2 };
        playOnce = true;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (playOnce == true)
        {

            if (other.tag == "Player1" || other.tag == "Player2" || other.tag == "FusedPlayer")
            {

                value = values[Random.Range(0, values.Length)];

                if (value == 0)
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

                GameObject enemy1 = Instantiate(enemyPrefab, spawnSpot1.position, Quaternion.identity);

                value = values[Random.Range(0, values.Length)];

                if (value == 0)
                {
                    enemyPrefab = enemyPrefab1;
                }
                else if (value == 1)
                {
                    enemyPrefab = enemyPrefab2;
                }
                else if (value == 3)
                {
                    enemyPrefab = enemyPrefab2;
                }

                GameObject enemy2 = Instantiate(enemyPrefab, spawnSpot2.position, Quaternion.identity);
                GameObject enemy3 = Instantiate(enemyPrefab, spawnSpot3.position, Quaternion.identity);
                enemy1.GetComponent<EnemyScript>().Spot(0, player1.transform);
                enemy1.GetComponent<EnemyScript>().Spot(1, player2.transform);
                enemy2.GetComponent<EnemyScript>().Spot(1, player2.transform);
                enemy2.GetComponent<EnemyScript>().Spot(0, player1.transform);
                enemy3.GetComponent<EnemyScript>().Spot(0, player1.transform);
                enemy3.GetComponent<EnemyScript>().Spot(1, player2.transform);

                enemy1.GetComponent<EnemyScript>().journey = journey;
                enemy2.GetComponent<EnemyScript>().journey = journey;
                enemy3.GetComponent<EnemyScript>().journey = journey;
                playOnce = false;
            }
        }
    }
}
