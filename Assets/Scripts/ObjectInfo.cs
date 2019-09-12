using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ObjectInfo : MonoBehaviour
{
    InputManager iManager;

    public bool isSelected;

    public string objectName;

    private NavMeshAgent agent;

    [SerializeField]
    public float attackDemage = 2f;

    [SerializeField]
    private float attackRange = 3f;

    [SerializeField]
    private float sightRange = 8f;

    [SerializeField]
    public bool isEnemy;

    [SerializeField]
    public bool isRanged;

    [SerializeField]
    public bool canMove;

    [SerializeField]
    public bool hasTarget;

    [SerializeField]
    public bool groundMove;

    [SerializeField]
    public float health = 20f;

    [SerializeField]
    public float cooldownTime = 4f;

    [SerializeField]
    public Animator animator;

    [SerializeField]
    public GameObject projectile;

    [SerializeField]
    float projectileHeight;

    private bool isCooldown;

    public bool isTDead;

    public GameObject target;

    public float speed = 5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        if (canMove)
        {
            animator = GetComponentInChildren<Animator>();
        }
        GameObject art = transform.Find("ybot").gameObject;
        art = art.transform.Find("Alpha_Surface").gameObject;

        if (isEnemy)
        {
            art.GetComponent<Renderer>().material.color = Color.red;
        }
    }
    private void Update()
    {
        if (canMove == false)
        {
            print(target);
        }
        //Check to see if agent is walking
        if (agent.velocity.magnitude <= 0f && canMove)
        {
            animator.SetBool("isWalking", false);
        }
        if (agent.velocity.magnitude >= 1.5f && canMove)
        {
            animator.SetBool("isWalking", true);
        }
        if (hasTarget && agent.isStopped || hasTarget && canMove == false)
        {
            
            if (!isCooldown && isTDead == false)
            {
                if (target.GetComponent<ObjectInfo>().health >= 0 && target.gameObject.activeSelf)
                {
                    StartCoroutine(Attack());
                }
                else
                {
                    target = null;
                    isTDead = true;
                }
            }
        }

        CheckAttackRange();

        if (isEnemy)
        {
            SphereDetect();
        }

        HealthCheck();

        if (agent.hasPath)
        {
            LookAt();
        }
        
    }

    public  void CheckAttackRange()
    {
        if (agent.hasPath)
        {
            if (groundMove)
            {
                if (agent.remainingDistance < 2f)
                {
                    agent.isStopped = true;
                }
            }
            else if (agent.remainingDistance < attackRange)
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
        if (agent.remainingDistance < attackRange || canMove == false)
        {
            isCooldown = true;
            if (canMove)
            {
                animator.SetBool("isAttacking", true);
            }

            if (isRanged)
            {
                ShootProjectile();
            }
            else
            {
                target.GetComponent<ObjectInfo>().health = target.GetComponent<ObjectInfo>().health - attackDemage;
            }

            yield return new WaitForSeconds(cooldownTime);

            if (canMove)
            {
                animator.SetBool("isAttacking", false);

                agent.isStopped = false;
            }

            isCooldown = false;

        }
        
    }
    void SphereDetect()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, sightRange);
        int i = 0;
        while (i < hitColliders.Length)
        {
            Delay();
            if (agent.hasPath == false)
            {
                if (hasTarget == false)
                {
                    if (hitColliders[i].gameObject.GetComponent<ObjectInfo>())
                    {
                        if (hitColliders[i].gameObject.GetComponent<ObjectInfo>().isEnemy == false)
                        {
                            if (canMove)
                            {
                                agent.SetDestination(hitColliders[i].gameObject.transform.position);
                            }
                            else
                            {
                                target = hitColliders[i].gameObject;
                                ShootProjectile();
                            }
                            hasTarget = true;
                            target = hitColliders[i].gameObject;
                        }
                    }
                }
            }
            
            i++;
        }
    }
    private IEnumerator Delay()
    {
        yield return new WaitForEndOfFrame();
        
    }
    void LookAt ()
    {
        if (groundMove == false)
        {
            Vector3 direction = target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
        }
    }
    private void HealthCheck()
    {
        if (health <= 0)
        {
            gameObject.SetActive(false);
        }
    }
    void ShootProjectile ()
    {
        GameObject proj = Instantiate(projectile, transform.position + new Vector3(0, projectileHeight, 0), Quaternion.identity) as GameObject;
        proj.GetComponent<Projectile>().target = target; 
    }

}
