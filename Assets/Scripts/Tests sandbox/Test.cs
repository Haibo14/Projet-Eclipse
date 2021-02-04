using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    public Vector3 rotation;

    public float power;

    void Start()
    {
        
    }

    void Update()
    {
        transform.Translate(rotation * power, Space.Self);
    }
}
