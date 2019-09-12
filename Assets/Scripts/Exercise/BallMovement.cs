using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float speed = 2;
    public float distanceUpDown = 0;

    void Update()
    {
        Vector3 mov = new Vector3(transform.position.x, Mathf.Sin(speed * Time.time) * distanceUpDown, transform.position.z);
        transform.position = mov;
    }
   
}
