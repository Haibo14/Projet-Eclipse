using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilarInstantiator : MonoBehaviour
{
    public GameObject pilarPrefab;
    void Start()
    {
        Instantiate(pilarPrefab, transform.position, Quaternion.identity);
    }


    void Update()
    {
        
    }

    public void Reset()
    {
        Debug.Log("Proc");

        if (transform.childCount == 0)
        {
            Instantiate(pilarPrefab, transform.position, Quaternion.identity);
        }

    }
}
