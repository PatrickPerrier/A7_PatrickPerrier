using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

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
    public bool groundMove;

    [SerializeField]
    ParticleSystem deathParticle;

    [SerializeField]
    public float startHealth;

    public float health;

    public Image healthBar;

    [SerializeField]
    public float cooldownTime = 4f;

    [SerializeField]
    public Animator animator;

    [SerializeField]
    public GameObject projectile;

    [SerializeField]
    float projectileHeight;

    private bool isCooldown;

    private float dist;

    public GameObject target;

    public float speed = 5f;

    void Start()
    {
        health = startHealth;
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
        else
        {
            Color fColor = FindObjectOfType<MainMenuHandler>().tColor;
            art.GetComponent<Renderer>().material.color = fColor;
        }
    }
    private void Update()
    {
        if (target != null)
        {
            dist = Vector3.Distance(target.transform.position, transform.position);
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
        if (agent.isStopped || canMove == false)
        {            
            if (!isCooldown && target != null)
            {
                if (target.GetComponent<ObjectInfo>().health >= 0)
                {
                    if (dist < attackRange)
                    {
                        StartCoroutine(Attack());
                    }
                    else
                    {
                        StopCoroutine(Attack());
                    }                    
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

        if (isEnemy == false)
        {
            SelectionGizmo();
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
        if (dist < attackRange || canMove == false)
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
            if (hitColliders[i].gameObject.GetComponent<ObjectInfo>())
            {
                if (hitColliders[i].gameObject.GetComponent<ObjectInfo>().isEnemy != isEnemy)
                {
                    target = hitColliders[i].gameObject;
                    if (agent.remainingDistance > attackRange)
                    {
                        target = null;
                    }
                    else if (canMove)
                    {
                        agent.SetDestination(target.gameObject.transform.position);
                    }
                    else if (!isCooldown)
                    {
                        ShootProjectile();
                    }

                }
            }
            i++;
        }
    }
    
    void LookAt ()
    {
        if (groundMove == false && target != null)
        {
            Vector3 direction = target.transform.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, speed * Time.deltaTime);
        }
    }
    private void HealthCheck()
    {
        healthBar.fillAmount = health / startHealth;
        if (health <= 0)
        {
            Instantiate(deathParticle, transform.position, Quaternion.identity);
            if (canMove == false)
            {
                FindObjectOfType<InGameMenuHandler>().gameWon = true;
                print("You Win");
            }
            Destroy(gameObject);
        }
    }
    void ShootProjectile ()
    {
        GameObject proj = Instantiate(projectile, transform.position + new Vector3(0, projectileHeight, 0), Quaternion.identity) as GameObject;
        proj.GetComponent<Projectile>().target = target; 
    }
    void SelectionGizmo ()
    {
        GameObject sCircle = transform.Find("SelectionCircle").gameObject;
        if (isSelected)
        {
            sCircle.SetActive(true);
        }
        else
        {
            sCircle.SetActive(false);
        }
    }

}
