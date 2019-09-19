using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 1.0f;
    public GameObject target;

    [SerializeField]
    float projectileDemage;

    [SerializeField]
    AudioSource source;

    [SerializeField]
    ParticleSystem explosion;

    void Update()
    {
        if (target != null)
        {
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, step);

            if (Vector3.Distance(transform.position, target.transform.position) < 0.1f)
            {
                Instantiate(source, transform.position, transform.rotation);
                Instantiate(explosion, transform.position, Quaternion.identity);
                target.GetComponent<ObjectInfo>().health = target.GetComponent<ObjectInfo>().health - projectileDemage;
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
        
    }
}
