using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MaskManager : MonoBehaviour
{
    public int piecesCount;

    public bool p1Finish;
    public bool p2Finish;
    void Start()
    {
        p1Finish = false;
        p2Finish = false;
    }

    
    void Update()
    {
        if (piecesCount ==6 && p1Finish == true && p2Finish == true)
        {
            SceneManager.LoadScene(2);
        }
    }
}
