using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextDisappear : MonoBehaviour
{
    public float timerEnd = 1.0f;
    public float timer;

    void Start()
    {
        
    }

    void Update()
    {
        timer += Time.unscaledDeltaTime;

        if(timer >= timerEnd)
        {
            timer = 0;
            gameObject.SetActive(false);
        }
    }
}
