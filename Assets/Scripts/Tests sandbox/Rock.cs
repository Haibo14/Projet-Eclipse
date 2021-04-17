using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    private Transform target;

    public AnimationCurve inTheAir;
    public AnimationCurve speedAcceleration;

    private Vector3 displacements;
    private Vector3 randomRotation;

    public float focusDistance = 5;
    public float rotationSpeed = 1000;
    public float speed = 15;
    public float height;
    public float randomRotationValue;
    private float maxDistance;
    private float distance;
    private float curveValue;

    public string targetTag;

    private bool isLookingAtObject = true;
    public bool throwing;

    void Start()
    {
        throwing = false;
        targetTag = "Player1";
        target = GameObject.FindGameObjectWithTag(targetTag).transform;

        randomRotation = new Vector3(Random.Range(-randomRotationValue, randomRotationValue), Random.Range(-randomRotationValue, randomRotationValue), Random.Range(-randomRotationValue, randomRotationValue));
    }

    void Update()
    {
        if(throwing == true)
        {
            distance = Vector2.Distance(new Vector2(target.position.x, target.position.z), new Vector2(transform.position.x, transform.position.z));

            curveValue = 1 - (distance / maxDistance);

            displacements = Vector3.forward + new Vector3(0, inTheAir.Evaluate(curveValue) * height, 0);

            Vector3 targetDirection = target.position - transform.position;

            Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, rotationSpeed * Time.deltaTime, 0.0F);

            transform.Translate(displacements * Time.deltaTime * speed * speedAcceleration.Evaluate(curveValue), Space.Self);

            if (Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(target.position.x, target.position.z)) < focusDistance)
            {
                isLookingAtObject = false;
            }

            if (isLookingAtObject)
            {
                transform.rotation = Quaternion.LookRotation(newDirection);
            }
            else
            {
                
                //transform.rotation = Quaternion.LookRotation(Vector3.down);
            }


            transform.GetChild(0).transform.Rotate(randomRotation * Time.deltaTime);
        }
        else
        {
            maxDistance = Vector2.Distance(new Vector2(target.position.x, target.position.z), new Vector2(transform.position.x, transform.position.z));
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player1" || other.gameObject.tag == "Player2" || other.gameObject.tag == "FusedPlayer")
        {
            //UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
