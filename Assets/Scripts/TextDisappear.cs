﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
            
            GetComponent<TextMeshProUGUI>().enabled = false;
            
        }
    }
}
