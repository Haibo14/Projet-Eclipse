using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject carPrefab;

    private IEnumerator coroutine;

    // Start is called before the first frame update
    void Start()
    {
        carPrefab = Resources.Load("Prefabs/Proto-Props/Car") as GameObject;

        StartCoroutine("Spawn");

        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator Spawn()
    {
        while (true)
        {
            Instantiate(carPrefab, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(5.0f);
        }
    }
}
