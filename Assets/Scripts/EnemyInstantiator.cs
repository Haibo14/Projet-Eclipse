using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiator : MonoBehaviour
{
    public GameObject enemyPrefab;

    void Start()
    {
        Reset();
    }

    void Update()
    {
        
    }

    public void Reset()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        enemy.transform.parent = transform;
        enemy.GetComponent<Animator>().SetBool("Work", true);
    }
}
