using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class blackSlabActivator : MonoBehaviour
{

    public GameObject activatedSlab;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player2")
        {
            if (activatedSlab.GetComponent<SlabDescent>())
            {
                activatedSlab.GetComponent<SlabDescent>().blackSlab = true;
            }

            if (activatedSlab.GetComponent<slabSlider>())
            {
                activatedSlab.GetComponent<slabSlider>().blackSlab = true;
            }
        }
    }
}
