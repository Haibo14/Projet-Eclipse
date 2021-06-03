using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{

    [SerializeField] Animator throwAnimator;

    GameObject rockPrefab;
    GameObject lastRock;

    void Start()
    {
        rockPrefab = Resources.Load("Prefabs/Proto-Props/Rock") as GameObject;

        StartCoroutine("Rock");
    }

    void Update()
    {
        
    }

    private IEnumerator Rock()
    {
        while (true)
        {
            lastRock = Instantiate(rockPrefab, transform.position, Quaternion.identity);
                
            throwAnimator.SetBool("throw", true);

            yield return new WaitForSeconds(10.0f);
        }
    }

    public void EndAnimation()
    {

    }

}
