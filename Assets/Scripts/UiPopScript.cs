using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiPopScript : MonoBehaviour
{
    Camera mainCam;

    public GameObject text;

    public int playersCount;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        BoxCollider box = this.gameObject.AddComponent<BoxCollider>();

        box.isTrigger =true;
        box.size = new Vector3(40, 40, 40);

        text = GameObject.FindGameObjectWithTag("BarricadeText");

    }


    void LateUpdate()
    {
        Vector3 posInScreen = mainCam.WorldToScreenPoint(transform.position);

        text.transform.position = posInScreen;

        if (playersCount == 0)
        {
            text.SetActive(false);
        }
        else if (playersCount == 1)
        {
            text.SetActive(true);
        }
        else if (playersCount == 2)
        {
            text.SetActive(true);
        }
        else if (playersCount > 2)
        {
            playersCount = 2;
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
        }
        else if (other.tag == "FusedPlayer")
        {
            playersCount -= 2;
        }
    }
}
