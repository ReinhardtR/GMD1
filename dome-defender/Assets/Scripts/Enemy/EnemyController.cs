using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class EnemyController : MonoBehaviour
{
    private GameObject target;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Collider2D coll;
    private Health health;
    private Rigidbody2D rb;
    private Health targetHealth;
    private Collider2D targetColl;

    private bool isGrounded;
    private readonly float isGroundedCastDistance = 0.01f;

    private float speed;
    private float range;

    private int damage;
    private float attackRate;
    private float nextAttackTime;
    private bool isDead;


    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        health = GetComponent<Health>();
        coll = GetComponent<Collider2D>();

        target = GameObject.FindWithTag("Tower");
        targetHealth = target.GetComponent<Health>();
        targetColl = target.GetComponent<Collider2D>();

        damage = 10;
        speed = 2f;
        range = 0.1f;
        attackRate = 0.5f;
        nextAttackTime = 0f;
        isDead = false;
    }

    void OnEnable()
    {
        health.OnDamageEvent += OnDamage;
        health.OnDeathEvent += OnDeath;
    }

    void OnDisable()
    {
        health.OnDamageEvent -= OnDamage;
        health.OnDeathEvent -= OnDeath;
    }

    void Update()
    {
        AttackTarget();
    }

    void FixedUpdate()
    {
        isGrounded = IsGrounded();
        ApproachTarget();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireCube(coll.bounds.center - transform.up * isGroundedCastDistance, coll.bounds.size);
    }

    private void ApproachTarget()
    {
        if (target == null)
        {
            Debug.Log("No target");
            return;
        }

        if (isDead)
        {
            Debug.Log("Dead");
            rb.velocity = new Vector2(0, rb.velocity.y);
            return;
        }

        ColliderDistance2D targetDistance = Physics2D.Distance(targetColl, coll);
        Debug.DrawRay(targetDistance.pointB, targetDistance.normal * range, Color.red);
        Debug.Log("Is grounded: " + isGrounded);

        bool shouldWalk = targetDistance.distance >= range && isGrounded;

        rb.velocity = shouldWalk
            ? new Vector2(targetDistance.normal.x, 0) * speed
            : new Vector2(0, rb.velocity.y);

        animator.SetBool("IsWalking", shouldWalk);
        spriteRenderer.flipX = targetDistance.normal.x <= 0;
    }

    private void AttackTarget()
    {
        if (target == null || isDead)
        {
            return;
        }

        ColliderDistance2D targetDistance = Physics2D.Distance(targetColl, coll);
        if (targetDistance.distance <= range)
        {
            Attack();
        }
    }

    private void Attack()
    {
        if (targetHealth == null)
        {
            return;
        }

        if (Time.time >= nextAttackTime)
        {
            targetHealth.TakeDamage(damage);
            nextAttackTime = Time.time + 1f / attackRate;
            animator.SetTrigger("Attack");
        }
    }

    private void OnDamage(int damage)
    {
        StartCoroutine(FlashRed());
    }

    private void OnDeath()
    {
        isDead = true;
        animator.SetTrigger("Die");
        Destroy(gameObject, animator.GetCurrentAnimatorStateInfo(0).length);
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(coll.bounds.center, coll.bounds.size, 0f, Vector2.down, isGroundedCastDistance, LayerMask.GetMask("Ground"));
    }

    IEnumerator FlashRed()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}
