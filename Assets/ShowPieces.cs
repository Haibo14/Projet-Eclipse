using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowPieces : MonoBehaviour
{
    public GameObject griffe1;
    public GameObject griffe2;
    public GameObject griffe3;
    public int pieces;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pieces == 1)
        {
            griffe1.SetActive(true);
        }
        else if (pieces == 2)
        {
            griffe2.SetActive(true);
        }
        else if (pieces == 3)
        {
            griffe3.SetActive(true);
        }
    }
}
