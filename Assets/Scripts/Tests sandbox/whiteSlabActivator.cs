using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whiteSlabActivator : MonoBehaviour
{
    public GameObject activatedSlab;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player1")
        {

            if (activatedSlab.GetComponent<SlabDescent>())
            {
                activatedSlab.GetComponent<SlabDescent>().whiteSlab = true;
            }

            if (activatedSlab.GetComponent<slabSlider>())
            {
                activatedSlab.GetComponent<slabSlider>().whiteSlab = true;
            }
        }
    }
}
