using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public Animator animator;


    GameObject rockPrefab;
    GameObject lastRock;

    GameObject enemyBallPrefab;
    GameObject lastEnemyBall;

    public GameObject handRock;

    private int[] values;

    public float timer;
    public float endTimer;

    private bool playOnce;

    void Start()
    {
        rockPrefab = Resources.Load("Prefabs/Proto-Props/Rock") as GameObject;
        enemyBallPrefab = Resources.Load("Prefabs/Proto-Props/EnemyBall") as GameObject;

        values = new int[]{ 0, 0, 0, 0, 0, 0, 0, 1, 1, 1};

        playOnce = true;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= endTimer && playOnce == true)
        {
            StartCoroutine("Rock");

            playOnce = false;
        }
    }

    private IEnumerator Rock()
    {
        while (true)
        {
            int value = values[Random.Range(0, values.Length)];

            if(value == 0)
            {
                animator.SetBool("rock", true);

                lastRock = Instantiate(rockPrefab, handRock.transform.position, Quaternion.identity);
                lastRock.transform.parent = handRock.transform;

                yield return new WaitForSeconds(4.0f);
            }
            else if (value == 1)
            {

                lastEnemyBall = Instantiate(enemyBallPrefab, transform.position, Quaternion.identity);

                yield return new WaitForSeconds(7.0f);
            }

        }
    }


}
