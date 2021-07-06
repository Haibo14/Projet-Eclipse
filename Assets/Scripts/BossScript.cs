using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    public AudioSource source;
    public AudioClip rage;
    public AudioClip lancerEnnemi;
    public AudioClip mort;

    public Animator animator;


    GameObject rockPrefab;
    GameObject lastRock;

    public GameObject enemyBallPrefab;

    public GameObject handRock;
    public GameObject handSbire;

    private int[] values;

    public int rageCount;

    public float timer;
    public float endTimer;

    private bool playOnce;
    private bool playOnce2;
    public bool rageMode;

    private int[] values2;


    void Start()
    {
        source = GetComponent<AudioSource>();

        rockPrefab = Resources.Load("Prefabs/FBX couleur/Pierre/BOSS_Pierre") as GameObject;

        values2 = new int[] { 0, 1, 2, 3, 4, 5 };
        values = new int[]{ 0, 0, 0, 0, 0, 0, 1, 1, 2, 2};

        playOnce = true;
        playOnce2 = true;
        rageMode = false;

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

        if(animator.GetBool("rage") == true)
        {
            if (playOnce2 == true)
            {
                source.PlayOneShot(rage, 1f);
                playOnce2 = false;
            }
        }
    }

    private IEnumerator Rock()
    {
        while (true)
        {
            int value = values[Random.Range(0, values.Length)];
            if (rageMode == false)
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
                    source.PlayOneShot(lancerEnnemi, 1f);

                    GameObject lastEnemyBall = Instantiate(enemyBallPrefab, handSbire.transform.position, Quaternion.identity);
                    lastEnemyBall.transform.parent = handSbire.transform;

                    yield return new WaitForSeconds(9.0f);

                   


                }
                else if (value == 2)
                {

                    
                    animator.SetBool("sbireGround", true);
                    source.PlayOneShot(lancerEnnemi, 1f);

                    GameObject lastEnemyBall = Instantiate(enemyBallPrefab, handSbire.transform.position, Quaternion.identity);
                    lastEnemyBall.transform.parent = handSbire.transform;

                    yield return new WaitForSeconds(7.5f);

                   

                }

            }
            else
            {
                if (rageCount == 10)
                {
                    animator.SetBool("rage", true);
                    source.PlayOneShot(rage, 1f);
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

                        GameObject lastEnemyBall = Instantiate(enemyBallPrefab, handSbire.transform.position, Quaternion.identity);
                        lastEnemyBall.transform.parent = handSbire.transform;

                        yield return new WaitForSeconds(5.5f);
                    }
                    else if (value == 2)
                    {
                        animator.SetBool("sbireGround", true);

                        GameObject lastEnemyBall = Instantiate(enemyBallPrefab, handSbire.transform.position, Quaternion.identity);
                        lastEnemyBall.transform.parent = handSbire.transform;

                        yield return new WaitForSeconds(4.75f);
                    }
                }

            }

            rageCount++;
        }
    }


}
