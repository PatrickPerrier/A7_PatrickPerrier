using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1.0f;
    public GameObject target;

    [SerializeField]
    float projectileDemage;

    void Update()
    {
        float step = speed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);

        if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
        {
            target.GetComponent<ObjectInfo>().health = target.GetComponent<ObjectInfo>().health - projectileDemage;
            Destroy(gameObject);
        }
    }
}
