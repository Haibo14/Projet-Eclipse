using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class PilarInstantiator : MonoBehaviour
{
    public GameObject pilarPrefab;
    void Start()
    {
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByBuildIndex(2))
        {
            Reset();
        }
    }


    void Update()
    {
        
    }

    public void Reset()
    {
        Debug.Log("Proc");

        if (gameObject.transform.childCount == 0)
        {
            GameObject pilar = Instantiate(pilarPrefab, transform.position, Quaternion.identity);
            pilar.transform.Rotate(-90f, 0, 0);
            pilar.transform.parent = gameObject.transform;
        }

    }
}
