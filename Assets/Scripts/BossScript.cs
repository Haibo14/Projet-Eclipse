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
    public GameObject handSbire;

    private int[] values;

    public int rageCount;

    public float timer;
    public float endTimer;

    private bool playOnce;
    bool rageMode;

    void Start()
    {
        rockPrefab = Resources.Load("Prefabs/Proto-Props/Rock") as GameObject;
        enemyBallPrefab = Resources.Load("Prefabs/Proto-Props/EnemyBall") as GameObject;

        values = new int[]{ 0, 0, 0, 0, 0, 0, 1, 1, 2, 2};

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

        if(rageCount <= 9)
        {
            rageMode = false;
        }
        else if (rageCount > 9 && rageCount <= 14)
        {
            rageMode = true
;       }
        else
        {
            rageCount = 0;
        }
    }

    private IEnumerator Rock()
    {
        while (true)
        {
            int value = values[Random.Range(0, values.Length)];
            if (rageMode)
            {
                if (value == 0)
                {
                    animator.SetBool("rock", true);

                    lastRock = Instantiate(rockPrefab, handRock.transform.position, Quaternion.identity);
                    lastRock.transform.parent = handRock.transform;

                    yield return new WaitForSeconds(6.0f);
                }
                else if (value == 1)
                {
                    animator.SetBool("sbireThrow", true);

                    lastEnemyBall = Instantiate(enemyBallPrefab, handSbire.transform.position, Quaternion.identity);
                    lastEnemyBall.transform.parent = handSbire.transform;

                    yield return new WaitForSeconds(9.0f);
                }
                else if (value == 2)
                {
                    animator.SetBool("sbireGround", true);

                    lastEnemyBall = Instantiate(enemyBallPrefab, handSbire.transform.position, Quaternion.identity);
                    lastEnemyBall.transform.parent = handSbire.transform;

                    yield return new WaitForSeconds(7.5f);
                }

            }
            else
            {
                if (rageCount == 10)
                {
                    animator.SetBool("rage", true);
                    yield return new WaitForSeconds(8.0f);
                }
                else
                {
                    if (value == 0)
                    {
                        animator.SetBool("rock", true);

                        lastRock = Instantiate(rockPrefab, handRock.transform.position, Quaternion.identity);
                        lastRock.transform.parent = handRock.transform;

                        yield return new WaitForSeconds(4.0f);
                    }
                    else if (value == 1)
                    {
                        animator.SetBool("sbireThrow", true);

                        lastEnemyBall = Instantiate(enemyBallPrefab, handSbire.transform.position, Quaternion.identity);
                        lastEnemyBall.transform.parent = handSbire.transform;

                        yield return new WaitForSeconds(5.5f);
                    }
                    else if (value == 2)
                    {
                        animator.SetBool("sbireGround", true);

                        lastEnemyBall = Instantiate(enemyBallPrefab, handSbire.transform.position, Quaternion.identity);
                        lastEnemyBall.transform.parent = handSbire.transform;

                        yield return new WaitForSeconds(4.75f);
                    }
                }

            }

            rageCount++;
        }
    }


}
