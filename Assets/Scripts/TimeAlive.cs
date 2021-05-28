using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeAlive : MonoBehaviour
{
    public float timer;
    public float endTimer;
    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= endTimer)
        {
            Destroy(gameObject);
        }
    }
}
