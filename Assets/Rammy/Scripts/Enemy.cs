using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IHealth
{
    [SerializeField] Animator animator;
    [SerializeField] NavMeshAgent agent;
    [SerializeField] Transform player;
    [SerializeField] Collider collider;
    [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;
    [SerializeField] float speed;
    [SerializeField] float detectionRadius;
    [SerializeField] float attackRadius;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] int damageAfterCooldown;
    [SerializeField] float attackCooldown;
    [SerializeField] float deathTime;
    [SerializeField] float hitEffectDuration;
    [SerializeField] Color defaultColor;
    [SerializeField] Color hitColor;

    private IHealth playerHealth;
    EnemyState enemyState = EnemyState.Idle;
    public int Health { get; private set; } = 100;

    public int HealthPoints => Health;

    float attackCooldownTimer = 0;
    bool isDead;
    float hitEffectTimer = 0;
    private void Awake()
    {
        agent.enabled = false;
        agent.speed = speed;
        agent.stoppingDistance = attackRadius;
        agent.autoBraking = true;        
    }

    private void Start()
    {
        player = GameManager.Instance.Player.transform;
        playerHealth = player.GetComponent<IHealth>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isDead) return;
        if (hitEffectTimer > 0)
        {
            hitEffectTimer -= Time.fixedDeltaTime;
            if (hitEffectTimer <= 0)
            {
                hitEffectTimer = 0;
                skinnedMeshRenderer.material.color = defaultColor;
            }
        }
        UpdateAttackCooldown();
        UpdateStateBehavior();
        EnemyState newState = GetNewState();
        if (enemyState == newState)
        {
            return;
        }
        SetState(newState);
    }

    bool IsPlayerVisible()
    {
        Vector3 diff = player.position - transform.position;
        return diff.magnitude <= detectionRadius &&
            Physics.Raycast(transform.position, diff.normalized, detectionRadius, playerLayer);
    }

    bool IsPlayerInAttackRange()
    {
        return (player.position - transform.position).magnitude <= attackRadius;
    }

    EnemyState GetNewState()
    {
        bool isPlayerVisible = IsPlayerVisible();
        bool isPlayerInAttackRange = IsPlayerInAttackRange();
        if (!isPlayerVisible)
        {
            return EnemyState.Idle;
        }
        if (!isPlayerInAttackRange)
        {
            return EnemyState.Chasing;
        }

        return EnemyState.Attacking;
    }

    void SetState(EnemyState newState)
    {
        switch (newState)
        {
            case EnemyState.Idle:
                agent.enabled = false;
                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", false);
                break;
            case EnemyState.Chasing:
                agent.enabled = true;
                animator.SetBool("isRunning", true);
                animator.SetBool("isAttacking", false);
                break;
            case EnemyState.Attacking:
                agent.enabled = false;
                animator.SetBool("isRunning", false);
                animator.SetBool("isAttacking", true);
                break;
        }

        enemyState = newState;
    }

    public void Damage(int damage)
    {
        if (isDead) return;
        skinnedMeshRenderer.material.color = hitColor;
        hitEffectTimer = hitEffectDuration;
        Health -= damage;
        if (Health <= 0)
        {
            Health = 0;
            HandleDeath();
        }
    }

    void HandleDeath()
    {
        isDead = true;
        animator.SetBool("isDead", true);
        agent.enabled = false;
        collider.enabled = false;
        Destroy(gameObject, deathTime);
    }

    void UpdateAttackCooldown()
    {
        attackCooldownTimer -= Time.fixedDeltaTime;
        if (attackCooldownTimer < 0)
        {
            attackCooldownTimer = 0;
        }
    }

    void UpdateStateBehavior()
    {
        if (enemyState == EnemyState.Chasing)
        {
            agent.destination = player.position;
        }
        if (enemyState == EnemyState.Attacking)
        {
            transform.forward = (player.position - transform.position).normalized;
            if (attackCooldownTimer <= 0)
            {
                //damage player
                //playerHealth.Damage(damageAfterCooldown);
                attackCooldownTimer = attackCooldown;
            }
        }
    }

    public void DamageCharacter()
    {
        if (enemyState != EnemyState.Attacking)
        {
            return;
        }
        playerHealth.Damage(damageAfterCooldown);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, attackRadius);
    }

    enum EnemyState
    {
        Idle,
        Chasing,
        Attacking
    }
}