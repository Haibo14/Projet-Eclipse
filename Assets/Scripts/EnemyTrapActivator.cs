using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrapActivator : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject journey;

    public Transform spawnSpot;

    public bool playOnce;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (playOnce == true)
        {
            if (other.tag == "Player1" || other.tag == "Player2" || other.tag == "FusedPlayer")
            {
                GameObject enemy = Instantiate(enemyPrefab, spawnSpot.position, Quaternion.identity);
                enemy.GetComponent<EnemyScript>().journey = journey;

                playOnce = false;
            }
        }
    }
}
