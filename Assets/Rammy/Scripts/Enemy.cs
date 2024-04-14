/* GameManager.cs
 * Author: Ramandeep Singh
 * Student Number: 301364879
 * Last modified: 02/04/2024
 * 
 * This script controls the Enemy
 * 
 */
using SlimUI.ModernMenu;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

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
    AudioSource audioSource;
    [SerializeField] string enemyHitSoundName;
    [SerializeField] string enemyDeathSoundName;

    [SerializeField] private bool _shouldMove = true;
    private IHealth playerHealth;
    EnemyState enemyState = EnemyState.Idle;
    public int Health { get; private set; } = 100;

    public UnityAction<Enemy> OnDied;

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
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        player = GameManager.Instance.Player.transform;
        playerHealth = player.GetComponentInChildren<IHealth>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!_shouldMove) return;
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
        //UpdateAttackCooldown();
        UpdateStateBehavior();
        EnemyState newState = GetNewState();
        if (enemyState == newState)
        {
            return;
        }
        SetState(newState);
    }

    //checks if player is in range and is visible
    bool IsPlayerVisible()
    {
        Vector3 diff = player.position - transform.position;
        return diff.magnitude <= detectionRadius &&
            Physics.Raycast(transform.position, diff.normalized, detectionRadius, playerLayer);
    }

    //checks if player is in attack state
    bool IsPlayerInAttackRange()
    {
        return (player.position - transform.position).magnitude <= attackRadius;
    }

    //Gets the state that the enemy should be in
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

    //sets the current state of the enemy and handles navmesgagent and animations accordingly
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

    //Enemy takes damage
    public void Damage(int damage)
    {
        if (isDead) return;
        skinnedMeshRenderer.material.color = hitColor;
        hitEffectTimer = hitEffectDuration;
        Health -= damage;
        AudioManager.Instance.PlayAsset(enemyHitSoundName, audioSource);
        audioSource.Play();
        if (Health <= 0)
        {
            Health = 0;
            HandleDeath();
        }
    }

    //Handles enemy death
    void HandleDeath()
    {
        AudioManager.Instance.PlayAsset(enemyDeathSoundName, audioSource);
        audioSource.Play();
        isDead = true;
        animator.SetBool("isDead", true);
        agent.enabled = false;
        collider.enabled = false;
        Destroy(gameObject, deathTime);
        OnDied?.Invoke(this);
    }

  /*  void UpdateAttackCooldown()
    {
        attackCooldownTimer -= Time.fixedDeltaTime;
        if (attackCooldownTimer < 0)
        {
            attackCooldownTimer = 0;
        }
    }*/

    //Performs an update logic based on current state
    void UpdateStateBehavior()
    {
        if (enemyState == EnemyState.Chasing)
        {
            agent.destination = player.position;
        }
        if (enemyState == EnemyState.Attacking)
        {
            Vector3 diff = player.position - transform.position;
            diff.y = 0;
            diff.Normalize();
            transform.forward = diff;
            /*if (attackCooldownTimer <= 0)
            {
                //damage player
                //playerHealth.Damage(damageAfterCooldown);
                attackCooldownTimer = attackCooldown;
            }*/
        }
    }

    //Damages player
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
