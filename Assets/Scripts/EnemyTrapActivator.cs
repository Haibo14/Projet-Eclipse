using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTrapActivator : MonoBehaviour
{
    public GameObject enemy;
    public GameObject journey;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player1" || other.tag == "Player2" || other.tag == "FusedPlayer")
        {
            enemy.GetComponent<EnemyScript>().journey = journey;
        }
    }
}
