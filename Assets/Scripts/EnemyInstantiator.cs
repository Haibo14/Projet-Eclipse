using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInstantiator : MonoBehaviour
{
    public GameObject enemyPrefab;
    public string animBool;

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void Reset()
    {
        GameObject enemy = Instantiate(enemyPrefab, transform.position, transform.rotation);
        enemy.transform.parent = transform;
        enemy.GetComponent<Animator>().SetBool(animBool, true);
    }
}
