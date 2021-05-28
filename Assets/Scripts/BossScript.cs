using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    GameObject rockPrefab;
    GameObject lastRock;

    GameObject enemyBallPrefab;
    GameObject lastEnemyBall;

    private int[] values;

    public float timer;
    public float endTimer;

    private bool playOnce;

    void Start()
    {
        rockPrefab = Resources.Load("Prefabs/Proto-Props/Rock") as GameObject;
        enemyBallPrefab = Resources.Load("Prefabs/Proto-Props/EnemyBall") as GameObject;

        values = new int[]{ 0, 0, 0, 0, 0, 0, 1, 1, 1, 1};

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

                lastRock = Instantiate(rockPrefab, transform.position, Quaternion.identity);

                yield return new WaitForSeconds(2.5f);
            }
            else if (value == 1)
            {

                lastEnemyBall = Instantiate(enemyBallPrefab, transform.position, Quaternion.identity);

                yield return new WaitForSeconds(5.0f);
            }

        }
    }


}
