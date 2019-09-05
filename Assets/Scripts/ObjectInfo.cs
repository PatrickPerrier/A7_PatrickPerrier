using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectInfo : MonoBehaviour
{
    public bool isSelected;

    public string objectName;

    private NavMeshAgent agent;

    [SerializeField]
    public float attackDemage = 2f;

    [SerializeField]
    private float attackRange = 6f;

    [SerializeField]
    public bool isEnemy;

    [SerializeField]
    public bool hasTarget;

    [SerializeField]
    private float health = 20f;

    [SerializeField]
    public float cooldownTime = 4f;

    private bool isCooldown;

    public RaycastHit target;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GameObject art = transform.GetChild(0).gameObject;
        if (isEnemy)
        {
            art.GetComponent<Renderer>().material.color = Color.red;
        }
    }
    private void Update()
    {
        if (hasTarget && agent.isStopped)
        {
            if (!isCooldown)
            {
                StartCoroutine(Attack());
            }
        }
        CheckAttackRange();

        HealthCheck();
    }

    public  void CheckAttackRange()
    {
        if (agent.hasPath)
        {
            if (agent.remainingDistance < attackRange)
            {
                agent.isStopped = true;
            }
            else
            agent.isStopped = false;
        }

    }
    private IEnumerator Attack()
    {
        new WaitForEndOfFrame();
        if (agent.remainingDistance < attackRange)
        {
            isCooldown = true;

            yield return new WaitForSeconds(cooldownTime);

            target.collider.GetComponent<ObjectInfo>().health = target.collider.GetComponent<ObjectInfo>().health - attackDemage;

            agent.isStopped = false;

            isCooldown = false;
        }
        
    }
    private void HealthCheck()
    {
        if (health <= 0)
        {
            Destroy(gameObject);
            print(objectName + " has been killed");
        }
    }
    
}
