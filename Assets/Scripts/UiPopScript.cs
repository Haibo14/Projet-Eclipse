using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UiPopScript : MonoBehaviour
{
    Camera mainCam;

    public GameObject text;

    public int playersCount;

    public string textTag;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        BoxCollider box = this.gameObject.AddComponent<BoxCollider>();

        box.isTrigger =true;

        if(textTag == "BarricadeText")
        {
            box.size = new Vector3(40, 40, 40);
        }
        else if (textTag == "LeverText")
        {
            box.size = new Vector3(10, 10, 10);
        }
        else if (textTag == "ChariotText")
        {
            box.size = new Vector3(15, 15, 15);
        }
        else if (textTag == "PlaqueText")
        {
            box.size = new Vector3(5, 5, 5);
        }

        text = GameObject.FindGameObjectWithTag(textTag);

        if(text == null)
        {
            //Destroy(gameObject);
        }

    }


    void LateUpdate()
    {

        if (playersCount == 0)
        {
        }
        else if (playersCount == 1)
        {
            Vector3 posInScreen = mainCam.WorldToScreenPoint(transform.position);

            text.transform.position = posInScreen;

            text.GetComponent<TextMeshProUGUI>().enabled = true;
        }
        else if (playersCount == 2)
        {
            Vector3 posInScreen = mainCam.WorldToScreenPoint(transform.position);

            text.transform.position = posInScreen;

            text.GetComponent<TextMeshProUGUI>().enabled = true;
        }
        else if (playersCount > 2)
        {
            playersCount = 2;
        }else if (playersCount < 0)
        {
            playersCount = 0;
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player1" || other.tag == "Player2")
        {
            playersCount++;
        }
        else if (other.tag == "FusedPlayer")
        {
            playersCount += 2;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player1" || other.tag == "Player2")
        {
            playersCount--;

            if (playersCount == 0)
            {
                text.GetComponent<TextMeshProUGUI>().enabled = false;
            }
        }
        else if (other.tag == "FusedPlayer")
        {
            playersCount = 0;

            if (playersCount == 0)
            {
                text.GetComponent<TextMeshProUGUI>().enabled = false;
            }
        }

    }
}
