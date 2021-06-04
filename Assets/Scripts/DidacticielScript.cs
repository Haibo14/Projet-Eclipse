using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DidacticielScript : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject fusedPlayer;

    public GameObject chefPrefab;

    public Transform spawnSpot;

    public int didacticielStep;

    bool startDidacticiel;

    void Start()
    {
        startDidacticiel = true;
    }

    void Update()
    {
        if (startDidacticiel == true)
        {
            if (didacticielStep == 0)
            {
                //montrer comment faire

                if(fusedPlayer.GetComponent<Players>().merged == true)
                {
                    didacticielStep++;
                }


            }
            else if (didacticielStep  == 1)
            {
                //expliquer comment faire

                if(fusedPlayer.GetComponent<Players>().splitting == true)
                {
                    didacticielStep++;
                }

            }
            else if (didacticielStep == 2)
            {
                //demander de refusionner

                if (fusedPlayer.GetComponent<Players>().merged == true)
                {
                    didacticielStep++;
                }

            }
            else if (didacticielStep == 3)
            {
                //défusionner + joystick

                if (fusedPlayer.GetComponent<Players>().splitting == true && fusedPlayer.GetComponent<Players>().move != Vector3.zero)
                {
                    didacticielStep++;
                }

            }
            else if (didacticielStep == 4)
            {
                //casser barricade
                GameObject barricade = GameObject.FindGameObjectWithTag("Breakable_Barricade");
                if (barricade == null)
                {
                    didacticielStep++;
                }
                
            }
            else if (didacticielStep == 5)
            {
                //apparition chef + message fuite

                GameObject enemy = Instantiate(chefPrefab, spawnSpot.position, spawnSpot.rotation);


                enemy.GetComponent<EnemyScript>().Spot(1, player2.transform);
                enemy.GetComponent<EnemyScript>().Spot(0, player1.transform);

                didacticielStep++;
            }


        }
    }
}
