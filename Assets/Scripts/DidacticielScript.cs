using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DidacticielScript : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject fusedPlayer;

    public GameObject chefPrefab;

    public Transform spawnSpot;

    public TextMeshProUGUI text0;
    public TextMeshProUGUI text1;
    public TextMeshProUGUI text2;
    public TextMeshProUGUI text3;
    public TextMeshProUGUI text4;
    public TextMeshProUGUI text5;

    public int didacticielStep;

    bool startDidacticiel;

    void Start()
    {
        startDidacticiel = true;
        text0.enabled = false;
        text1.enabled = false;
        text2.enabled = false;
        text3.enabled = false;
        text4.enabled = false;
        text5.enabled = false;
    }

    void Update()
    {
        if (startDidacticiel == true)
        {
            if (didacticielStep == 0)
            {
                //montrer comment faire

                text0.enabled = true;

                if (fusedPlayer.GetComponent<Players>().merged == true)
                {
                    didacticielStep++;
                    text0.enabled = false;
                }


            }
            else if (didacticielStep  == 1)
            {
                //expliquer comment faire

                text1.enabled = true;

                if (fusedPlayer.GetComponent<Players>().splitting == true)
                {
                    didacticielStep++;
                    text1.enabled = false;
                }

            }
            else if (didacticielStep == 2)
            {
                //demander de refusionner

                text2.enabled = true;

                if (fusedPlayer.GetComponent<Players>().merged == true)
                {

                    text2.enabled = false;
                    didacticielStep++;
                }

            }
            else if (didacticielStep == 3)
            {
                //défusionner + joystick
                text3.enabled = true;

                if (fusedPlayer.GetComponent<Players>().splitting == true && fusedPlayer.GetComponent<Players>().move != Vector3.zero)
                {
                    text3.enabled = false;
                    didacticielStep++;
                }

            }
            else if (didacticielStep == 4)
            {
                //casser barricade

                text4.enabled = true;

                GameObject barricade = GameObject.FindGameObjectWithTag("Breakable_Barricade");
                if (barricade == null)
                {
                    text4.enabled = false;
                    didacticielStep++;
                }
                
            }
            else if (didacticielStep == 5)
            {
                //apparition chef + message fuite

                text5.enabled = true;

                GameObject enemy = Instantiate(chefPrefab, spawnSpot.position, spawnSpot.rotation);


                enemy.GetComponent<EnemyScript>().Spot(1, player2.transform);
                enemy.GetComponent<EnemyScript>().Spot(0, player1.transform);
                enemy.GetComponent<EnemyScript>().spotted = true;

                didacticielStep++;
            }


        }
    }
}
