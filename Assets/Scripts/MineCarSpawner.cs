using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineCarSpawner : MonoBehaviour
{
    public GameObject carPrefab;

    public GameObject pressurePlate;

    public GameObject alternativePlate;

    public string railTag;

    public float timer;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (pressurePlate.GetComponent<PressurePlateMine>().pressed == true)
        {
            timer -= Time.deltaTime;
        }

        if (timer <= 0)
        {
            GameObject lastInstantiate = Instantiate(carPrefab, transform.position, Quaternion.identity);
            lastInstantiate.GetComponent<MineCar>().tagRail = railTag;
            lastInstantiate.GetComponent<MineCar>().pressurePlate = alternativePlate;
            timer = 20;
        }
    }

}
