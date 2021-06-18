using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnBoss : MonoBehaviour
{
    public GameObject respawnManager;
    public GameObject boss;

    public Respawn respawn;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void Reset()
    {
        GameObject[] pilarsInstantiator = GameObject.FindGameObjectsWithTag("PilarInstantiator");
        foreach (GameObject pilar in pilarsInstantiator)
        {
                pilar.GetComponent<PilarInstantiator>().Reset();
        }

        boss.GetComponent<BossScript>().rageCount = 0;
    }
}
