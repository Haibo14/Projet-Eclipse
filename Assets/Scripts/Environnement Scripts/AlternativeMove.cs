using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlternativeMove : MonoBehaviour
{
    Vector3 altMove;

    public float speed;

    void Start()
    {
        
    }

    void Update()
    {
        altMove = transform.localPosition; 
        
        altMove.z += Input.GetAxis("kb_Horizontal") * Time.deltaTime * speed;
        altMove.x += Input.GetAxis("kb_Vertical") * Time.deltaTime * speed;
        transform.localPosition = altMove;
    }
}
