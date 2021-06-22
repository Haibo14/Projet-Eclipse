using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextDisappear : MonoBehaviour
{
    public float timerEnd = 2.0f;
    public float timer;


    void Start()
    {
    }

    void Update()
    {
        GameObject[] popUI = GameObject.FindGameObjectsWithTag("UI");
        foreach(GameObject pop in popUI)
        {
            if (GetComponent<TextMeshProUGUI>().enabled == true)
            {
                timer += Time.deltaTime;

                if (timer >= timerEnd)
                {
                    GetComponent<TextMeshProUGUI>().enabled = false;
                    pop.GetComponent<UiPopScript>().playersCount = 0;
                    timer = 0;


                }
            }
        }

        
    }
}
