using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// https://www.youtube.com/watch?v=Ae7XJ8Ai42I

public class Fishing : MonoBehaviour
{
    public Transform[] target;
    bool isMoving = false;

    float speed = 5.0f;

    private Transform newTarget;

    public void Update()
    {
        if (isMoving == false)
        {
            newTarget = target[Random.Range(0, target.Length)];
            isMoving = true;
        }

        transform.position = Vector3.MoveTowards(transform.position, newTarget.position, speed * Time.deltaTime);

        if (transform.position == newTarget.position)
        {
            isMoving = false;
        }



    }


}
